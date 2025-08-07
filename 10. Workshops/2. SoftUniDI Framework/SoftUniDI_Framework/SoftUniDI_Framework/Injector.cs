using System.Reflection;
using SoftUniDI_Framework.Attributes;
using SoftUniDI_Framework.Interfaces;

namespace SoftUniDI_Framework;

public class Injector
{
    private readonly IModule _module;

    public Injector(IModule module)
    {
        _module = module;
    }
    
    public TClass Inject<TClass>()
    {
        bool hasConstructorAttribute = CheckForConstructorInjection<TClass>();
        bool hasFieldAttribute = CheckForFieldInjection<TClass>();

        if (hasConstructorAttribute && hasFieldAttribute)
            throw new ArgumentException("There must be only field or constructor annotated with Inject attribute");

        if (hasConstructorAttribute)
            return CreateConstructorInjection<TClass>();
        else if (hasFieldAttribute)
            return CreateFieldsInjection<TClass>();
        
        return default(TClass);
    }

    private TClass CreateConstructorInjection<TClass>()
    {
        Type desireClass = typeof(TClass);

        if (desireClass is null)
            return default(TClass);

        ConstructorInfo[] constructors = desireClass.GetConstructors();
        foreach (ConstructorInfo constructor in constructors)
        {
            if (!CheckForConstructorInjection<TClass>())
                continue;

            InjectAttribute inject = (InjectAttribute)constructor
                .GetCustomAttributes(typeof(InjectAttribute), true)
                .FirstOrDefault();

            ParameterInfo[] parameterTypes = constructor.GetParameters();
            object[] constructorParams = new object[parameterTypes.Length];

            int i = 0;
            foreach (ParameterInfo parameterType in parameterTypes)
            {
                Attribute qualifier = parameterType.GetCustomAttribute(typeof(NamedAttribute));
                Type dependency = null;
                
                if (qualifier is null)
                    dependency = _module.GetMapping(parameterType.ParameterType, inject);
                else
                    dependency = _module.GetMapping(parameterType.ParameterType, qualifier);

                object nestedInstance = typeof(Injector)
                    .GetMethod(nameof(Inject))
                    .MakeGenericMethod(dependency)
                    .Invoke(this, null);
                
                _module.SetInstance(dependency, nestedInstance);
                
                if (parameterType.ParameterType.IsAssignableFrom(dependency))
                {
                    object instance = _module.GetInstance(dependency);

                    if (instance is not null)
                        constructorParams[i++] = instance;
                    else
                    {
                        instance = Activator.CreateInstance(dependency);
                        constructorParams[i++] = instance;
                        _module.SetInstance(parameterType.ParameterType, instance);
                    }
                }
            }

            return (TClass)Activator.CreateInstance(desireClass, constructorParams);
        }
        
        return default(TClass);
    }

    private TClass CreateFieldsInjection<TClass>()
    {
        Type desireClass = typeof(TClass);
        object desireClassInstance = _module.GetInstance(desireClass);

        if (desireClassInstance is null)
        {
            desireClassInstance = Activator.CreateInstance(desireClass);
            _module.SetInstance(desireClass, desireClassInstance);
        }

        FieldInfo[] fields = desireClass.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            if (field.GetCustomAttributes(typeof(InjectAttribute), true).Any())
            {
                InjectAttribute injection = (InjectAttribute)field
                    .GetCustomAttributes(typeof(InjectAttribute), true)
                    .FirstOrDefault();
                
                Type dependency = null;

                object[] qualifier = field.GetCustomAttributes(typeof(NamedAttribute), true);
                
                Type type = field.FieldType;
                if (qualifier is null)
                    dependency = _module.GetMapping(type, injection);
                else
                    dependency = _module.GetMapping(type, qualifier);

                if (type.IsAssignableFrom(dependency))
                {
                    object instance = _module.GetInstance(dependency);
                    if (instance is null)
                    {
                        instance = Activator.CreateInstance(dependency);
                        _module.SetInstance(dependency, instance);
                    }
                    
                    field.SetValue(desireClassInstance, instance);
                }
            }
        }
        
        return (TClass)desireClassInstance;
    }

    private bool CheckForConstructorInjection<TClass>()
        => typeof(TClass)
            .GetConstructors()
            .Any(c => c.GetCustomAttributes(typeof(InjectAttribute), true).Any());
    
    private bool CheckForFieldInjection<TClass>()
        => typeof(TClass)
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Any(f => f.GetCustomAttributes(typeof(InjectAttribute), true).Any());
}