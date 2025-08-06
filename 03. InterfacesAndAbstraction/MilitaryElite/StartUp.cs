using MilitaryElite.Core.Interfaces;
using MilitaryElite.IO;

namespace MilitaryElite;

using MilitaryElite.Core;

public class StartUp
{
    public static void Main(string[] args)
    {
        ConsoleReader reader = new ConsoleReader();
        ConsoleWriter writer = new ConsoleWriter();
        
        IEngine engine = new Engine(reader, writer);
        engine.Run();
    }
}