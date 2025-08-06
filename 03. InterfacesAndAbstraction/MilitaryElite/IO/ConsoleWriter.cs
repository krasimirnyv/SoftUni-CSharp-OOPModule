using MilitaryElite.IO.Interfaces;

namespace MilitaryElite.IO;

public class ConsoleWriter : IWriter
{
    public void WriteLine(string line)
        => Console.WriteLine(line);
}