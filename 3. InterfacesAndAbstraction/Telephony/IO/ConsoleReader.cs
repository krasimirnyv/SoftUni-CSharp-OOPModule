using Telephony.IO.Interfaces;

namespace Telephony.IO;

public class ConsoleReader : IReader
{
    public string ReadLine()
    {
        return Console.ReadLine();
    }
}