using Telephony.IO.Interfaces;

namespace Telephony.IO;

public class FileWriter : IWriter
{
    public void WriteLine(string line)
    {
        string filePath = Path.Combine("..", "..", "..", "text.txt");
        
        using StreamWriter writer = new StreamWriter(filePath, true);
        
        writer.WriteLine(line);
    }
}