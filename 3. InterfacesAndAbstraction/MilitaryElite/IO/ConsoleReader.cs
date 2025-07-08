using MilitaryElite.IO.Interfaces;

namespace MilitaryElite.IO;

public class ConsoleReader : IReader
{
    public string ReadLine()
        => Console.ReadLine();
}