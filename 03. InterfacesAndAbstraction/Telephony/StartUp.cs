using Telephony.Core;
using Telephony.Core.Interfaces;
using Telephony.IO;

namespace Telephony;

public class StartUp
{
    public static void Main(string[] args)
    {
        ConsoleReader reader = new ConsoleReader();
        FileWriter writer = new FileWriter();
        
        IEngine engine = new Engine(reader, writer);
        engine.Run();
    }
}