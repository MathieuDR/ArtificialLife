using System.Reflection;

namespace Encoder; 


internal sealed class InfoSwitch {
    private readonly PropertyInfo? _propertyInfo;
    private readonly Type _type;
    private readonly FieldInfo? _fieldInfo;
    private readonly RnaPiece _rnaPieceAttribute; 
    

    public InfoSwitch(Type type, FieldInfo fieldInfo) {
        _type = type;
        _fieldInfo = fieldInfo;
        _rnaPieceAttribute = SetRnaAttribute(fieldInfo);

    }

    public InfoSwitch(Type type, PropertyInfo propertyInfo) {
        _type = type;
        _propertyInfo = propertyInfo;
        _rnaPieceAttribute = SetRnaAttribute(propertyInfo);
    }
    
    private RnaPiece SetRnaAttribute(MemberInfo memberInfo) {
        return memberInfo.GetCustomAttribute<RnaPiece>(false) ?? throw new NullReferenceException("RnaPiece attribute is null");
    }
    
    public object? GetValue(object obj) {
        if (obj.GetType() != _type)
            throw new ArgumentException($"Object is of type {_type} but should be of type {obj.GetType()}");
                
        return _propertyInfo?.GetValue(obj) ?? _fieldInfo?.GetValue(obj);
    }

    public string Name => _propertyInfo?.Name ?? _fieldInfo!.Name;
    public Type GetMemberType() => _propertyInfo?.PropertyType ?? _fieldInfo!.FieldType;
    public int Order => _rnaPieceAttribute.Order;
    public bool IsConstructorParam => _rnaPieceAttribute.IsConstructorParam;
    public int? ConstructorArgument => _rnaPieceAttribute.ConstructorArgument;

    public void SetValue<T>(T obj, object? value) {
        if(obj is null)
            throw new ArgumentNullException(nameof(obj));
        
        // set the value from an object
        if (obj.GetType() != _type)
            throw new ArgumentException($"Object is of type {_type} but should be of type {obj.GetType()}");
        
    
        
        if (_propertyInfo != null) {
            _propertyInfo.SetValue(obj, value);
        } else {
            _fieldInfo!.SetValue(obj, value);
        }
    }
}
