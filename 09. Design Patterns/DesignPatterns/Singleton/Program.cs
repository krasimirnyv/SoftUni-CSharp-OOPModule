namespace Singleton;

class Program
{
    static void Main(string[] args)
    {
        var db1 = SingletonDataContainer.Instance;
        Console.WriteLine(db1.GetPopulation("Washington, D.C."));
        
        var db2 = SingletonDataContainer.Instance;
        Console.WriteLine(db2.GetPopulation("London"));
    }
}