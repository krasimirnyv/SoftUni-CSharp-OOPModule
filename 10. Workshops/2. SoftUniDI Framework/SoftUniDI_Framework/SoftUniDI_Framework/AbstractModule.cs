using SoftUniDI_Framework.Attributes;
using SoftUniDI_Framework.Interfaces;

namespace SoftUniDI_Framework;

public abstract class AbstractModule : IModule
{
    private IDictionary<Type, Dictionary<string, Type>> _implementations;
    private IDictionary<Type, object> _instances;

    protected AbstractModule()
    {
        _implementations = new Dictionary<Type, Dictionary<string, Type>>();
        _instances = new Dictionary<Type, object>();
    }
    
    public abstract void Configure();
    
    protected void CreateMapping<TInter, TImpl>()
    {
        if (!_implementations.ContainsKey(typeof(TInter)))
        {
            _implementations[typeof(TInter)] = new Dictionary<string, Type>();
        }
        
        _implementations[typeof(TInter)].Add(typeof(TImpl).Name, typeof(TImpl));
    }
    
    public Type GetMapping(Type currentInterface, object attribute)
    {
        Dictionary<string, Type> currentImplementation = _implementations[currentInterface];

        Type type = null;
        
        if (attribute is InjectAttribute)
        {
            if (currentImplementation.Count is 1) 
                type = currentImplementation.Values.First();
            else 
                throw new ArgumentException($"No available mapping for class: {currentInterface.Name}");
        }
        else if (attribute is NamedAttribute)
        {
            NamedAttribute qualifier = attribute as NamedAttribute;
            string dependencyName = qualifier.Name;
            type = currentImplementation[dependencyName];
        }
        
        return type;
    }

    public object GetInstance(Type implementation)
    {
        _instances.TryGetValue(implementation, out object value);
        return value;
    }
    
    public void SetInstance(Type implementation, object instance)
        => _instances.TryAdd(implementation, instance);
}