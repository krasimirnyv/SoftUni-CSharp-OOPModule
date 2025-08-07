namespace SoftUniDI_Framework.Attributes;

[AttributeUsage((AttributeTargets.Parameter | AttributeTargets.Field))]
public class NamedAttribute : Attribute
{
    public NamedAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}