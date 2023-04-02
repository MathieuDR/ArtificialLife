
using System.Collections;

namespace Encoder; 

public static class Decoder {
    public static T Decode<T>(byte[] bytes) {
        return (T) Decode(bytes, typeof(T));
    }
    
    public static object Decode(byte[] bytes, Type type) {
        if (type.GetInterface(nameof(IEnumerable)) != null) {
            return DecodeCollection(bytes, type);
        }

        return DecodeObject(type, bytes);
    }

    private static T DecodeCollection<T>(byte[] bytes) {
        throw new NotImplementedException();
    }
    
    private static IEnumerable DecodeCollection(byte[] bytes, Type type) {
        throw new NotImplementedException();
    }

    private static object DecodeObject(Type type, byte[] bytes) {
        using var binaryReader = new BinaryReader(new MemoryStream(bytes));
        var members = GetMembers(type);
        var values = new List<(InfoSwitch info, object? value)>();
        
        foreach (var member in members) {
            var memberType = member.GetMemberType();
            var getValue = ReadValue(memberType, binaryReader);
            values.Add((member, getValue));
        }

        var constructorArguments = values.Where(x => x.info.ConstructorArgument is not null).OrderBy(x => x.info.ConstructorArgument).Select(x => x.value).ToArray();
        var obj = Activator.CreateInstance(type, constructorArguments);
        
        foreach (var value in values.Where(x => x.info.ConstructorArgument is null)) {
            value.info.SetValue(obj, value.value);
        }

        return obj!;
    }

    private static object? ReadValue(Type type, BinaryReader binaryReader) =>
        type switch {
            { IsEnum: true } => binaryReader.ReadInt32(),
            { IsPrimitive: true } t => ReadPrimitives(t, binaryReader),
            { } t when t == typeof(string) => binaryReader.ReadString(),
            { } t when t == typeof(decimal) => binaryReader.ReadDecimal(),
            {IsGenericType: true} => ReadGeneric(type, binaryReader),
            { IsClass: true } t => DecodeObject(t, binaryReader.ReadBytes(binaryReader.ReadInt32())),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} is not supported")
        };

    private static object? ReadGeneric(Type type, BinaryReader binaryReader) {
        var genericType = type.GetGenericTypeDefinition();
        
        return genericType switch {
            { } t when t == typeof(Nullable<>) => ReadNullable(type.GenericTypeArguments.First(), binaryReader),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} is not supported")
        };
    }

    private static object? ReadNullable(Type type, BinaryReader binaryReader) {
        var hasValue = binaryReader.ReadBoolean();
        return hasValue ? ReadValue(type, binaryReader) : null;
    }


    private static T DecodeObject<T>(byte[] bytes) {
       return (T) DecodeObject(typeof(T), bytes); 
    }

    private static object ReadPrimitives(Type t, BinaryReader binaryReader) =>
        t switch {
            { } type when type == typeof(int) => binaryReader.ReadInt32(),
            { } type when type == typeof(ushort) => binaryReader.ReadUInt16(),
            { } type when type == typeof(float) => binaryReader.ReadSingle(),
            { } type when type == typeof(double) => binaryReader.ReadDouble(),
            { } type when type == typeof(long) => binaryReader.ReadInt64(),
            { } type when type == typeof(byte) => binaryReader.ReadByte(),
            { } type when type == typeof(bool) => binaryReader.ReadBoolean(),
            { } type when type == typeof(char) => binaryReader.ReadChar(),
            { } type when type == typeof(sbyte) => binaryReader.ReadSByte(),
            { } type when type == typeof(short) => binaryReader.ReadInt16(),
            { } type when type == typeof(uint) => binaryReader.ReadUInt32(),
            { } type when type == typeof(ulong) => binaryReader.ReadUInt64(),
            _ => throw new ArgumentOutOfRangeException(nameof(t), t, $"Type {t} is not supported")
        };

    private static List<InfoSwitch> GetMembers<T>() => GetMembers(typeof(T));
    private static List<InfoSwitch> GetMembers(Type type) => TypeInfoService.Instance.GetTypeInfo(type);
}

