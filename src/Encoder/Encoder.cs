using System.Collections;
using System.Text;

namespace Encoder;

public static class Encoder {
    public static byte[] Encode(object toEncode) {
        if (toEncode is IEnumerable collection) {
            return EncodeCollection(collection);
        }

        return EncodeObject(toEncode);
    }

    private static byte[] EncodeCollection(IEnumerable collection) {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);

        foreach (var something in collection) {
            using var somethingStream = EncodeObjectToStream(something);
            writer.Write(somethingStream.Length);
            writer.Write(somethingStream.ToArray());
        }

        return stream.ToArray();
    }
    private static byte[] EncodeObject(object toEncode) {
        using var stream = EncodeObjectToStream(toEncode);
        return stream.ToArray();
    }

    private static MemoryStream EncodeObjectToStream<T>(T toEncode) {
        var members = GetMembersWithValues(toEncode);
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);

        foreach (var member in members) {
            WriteValue(member.Type, member.Value, writer);
        }

        return stream;
    }

    private static void WriteValue(Type type, object? value, BinaryWriter writer) {
        // switch on type
        switch (type) {
            case {IsPrimitive: true}:
                WritePrimitives(value, writer);
                return;
            case {IsEnum: true}: 
                WritePrimitives(Convert.ToInt32(value), writer);
                return;
            case {IsArray: true}:
                var ab = EncodeCollection((IEnumerable)value!);
                writer.Write(ab.Length);
                writer.Write(ab);
                return;
            case { } t when t == typeof(string):
                writer.Write((string)value!);
                return;
            case { } t when t == typeof(Decimal):
                writer.Write((Decimal)value!);
                return;
            case {IsGenericType:true}:
                WriteGenerics(type, value, writer);
                return;
            case {IsClass: true}: 
                var cb = Encode(value!);
                writer.Write(cb.Length);
                writer.Write(cb);
                return;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    private static void WriteGenerics(Type type, object? value, BinaryWriter writer) {
        var genericType = type.GetGenericTypeDefinition();

        switch (genericType) {
            case {} t when t == typeof(Nullable<>):
                if (value is null) {
                    writer.Write(false);
                    return;
                }
                
                writer.Write(true);
                var underlyingType = Nullable.GetUnderlyingType(type);
                if (underlyingType is null) throw new NullReferenceException();
                WriteValue(underlyingType, value, writer);
                return;
            case {} t when t == typeof(List<>):
                throw new NotImplementedException();
                return;
            case {} t when t == typeof(Dictionary<,>):
                throw new NotImplementedException();
                return;
            default: throw new ArgumentOutOfRangeException();
        }
        
    }

    private static void WritePrimitives(object? value, BinaryWriter writer) {
        switch (value) {
            case int i:
                writer.Write(i);
                break;
            case ushort us:
                writer.Write(us);
                break;
            case short s:
                writer.Write(s);
                break;
            case uint ui:
                writer.Write(ui);
                break;
            case long l:
                writer.Write(l);
                break;
            case ulong ul:
                writer.Write(ul);
                break;
            case byte b:
                writer.Write(b);
                break;
            case sbyte sb:
                writer.Write(sb);
                break;
            case float f:
                writer.Write(f);
                break;
            case double d:
                writer.Write(d);
                break;
            case bool bl:
                writer.Write(bl);
                break;
            case char c:
                writer.Write(c);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static IEnumerable<(Type Type, object? Value)> GetMembersWithValues(object toEncode) {
        var type = toEncode.GetType();

        var members = TypeInfoService.Instance.GetTypeInfo(type);
        return members.Select(x => (x.GetMemberType(), x.GetValue(toEncode)));
    }
}
