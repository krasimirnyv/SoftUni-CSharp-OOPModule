namespace Singleton;

public class SingletonDataContainer : ISingletonContainer
{
    private readonly Dictionary<string, int> _capitals = new();
    
    private SingletonDataContainer()
    {
        Console.WriteLine("Initializing singleton object");
        
        string[] elements = File.ReadAllLines("capitals.txt");
        for (int i = 0; i < elements.Length; i += 2)
        {
            _capitals.Add(elements[i], int.Parse(elements[i + 1]));
        }
    }

    public static SingletonDataContainer Instance { get; } = new SingletonDataContainer();

    public int GetPopulation(string name)
    {
        return _capitals[name];
    }
}