using WildFarm.IO.Interfaces;

namespace WildFarm.IO;

public class ConsoleWriter : IWriter
{
    public void WriteLine(object value) 
        => Console.WriteLine(value);
}