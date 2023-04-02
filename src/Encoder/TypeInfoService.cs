using System.Reflection;

namespace Encoder; 

internal sealed class TypeInfoService {
    // create singleton instance
    private TypeInfoService() { }
    
    public static TypeInfoService Instance { get; } = new TypeInfoService();
    
    // create dictionary to store type info
    private readonly Dictionary<Type, List<InfoSwitch>> _typeInfo = new Dictionary<Type, List<InfoSwitch>>();
    
    // create method to get type info
    public List<InfoSwitch> GetTypeInfo(Type type) {
        if (_typeInfo.ContainsKey(type)) {
            return _typeInfo[type];
        }
        
        var infoSwitches = new List<InfoSwitch>();
        var fields = type.GetFields().Where(x => Attribute.IsDefined(x, typeof(RnaPiece)));
        var properties = type.GetProperties().Where(x => Attribute.IsDefined(x, typeof(RnaPiece)));
        
        foreach (var field in fields) {
            infoSwitches.Add(new InfoSwitch(type, field));
        }
        
        foreach (var property in properties) {
            infoSwitches.Add(new InfoSwitch(type, property));
        }
        
        // sort by order then by name
        infoSwitches = infoSwitches
            .OrderBy(x => x.Order)
            .ThenBy(x => x.Name)
            .ToList();
        
        _typeInfo.Add(type, infoSwitches);
        return infoSwitches;
    }
    
    // create method to get type info
    public List<InfoSwitch> GetTypeInfo<T>() {
        return GetTypeInfo(typeof(T));
    }
}
