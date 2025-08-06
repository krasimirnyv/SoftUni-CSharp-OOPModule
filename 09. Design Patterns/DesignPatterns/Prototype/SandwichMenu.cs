namespace Prototype;

public class SandwichMenu
{
    private readonly Dictionary<string, SandwichPrototype> _sandwiches = new();

    public SandwichPrototype this[string name]
    {
        get => _sandwiches[name];
        set => _sandwiches.Add(name, value);
    }
}