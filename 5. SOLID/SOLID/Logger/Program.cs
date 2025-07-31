using Logger.Appenders;
using Logger.Enums;
using Logger.Interfaces;
using Logger.Layouts;

namespace Logger;

using Logger.Loggers;

class Program
{
    private static readonly Dictionary<string, Func<ILayout>> LayoutFactories = new()
    {
        [nameof(SimpleLayout)] = () => new SimpleLayout(),
        [nameof(XmlLayout)] = () => new XmlLayout()
    };
    
    private static readonly Dictionary<string, Func<ILayout, Func<string, ReportLevel, string, bool>?, IAppender>> AppenderFactories = new()
    {
        [nameof(ConsoleAppender)] = (layout, filter) => new ConsoleAppender(layout, filter),
        [nameof(FileAppender)] = (layout, filter) => new FileAppender(
            Path.Combine("..", "..", "..", "log.txt"),
            layout,
            filter
        )
    };
    
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        IAppender[] appenders = new IAppender[n];

        for (int i = 0; i < n; i++) 
            appenders[i] = ReadAppender();
        
        ILogger logger = new Logger(appenders);
        
        string command = default;
        while ((command = Console.ReadLine()) != "END")
            Log(logger, command);
    }
    
    private static IAppender ReadAppender()
    {
        string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        string appenderType = data[0];
        if (!AppenderFactories.ContainsKey(appenderType))
            throw new InvalidOperationException($"Appender type \"{appenderType}\" is not found.");
        
        string layoutType = data[1];
        if (!LayoutFactories.ContainsKey(layoutType))
            throw new InvalidOperationException($"Layout type \"{layoutType}\" is not found.");
            
        Func<ILayout> currentLayoutFactory = LayoutFactories[layoutType];
        ILayout layout = currentLayoutFactory();
            
        Func<string, ReportLevel, string, bool>? filter = null;
        if (data.Length == 3) 
        {
            ReportLevel minimumReportLevel = Enum.Parse<ReportLevel>(data[2], ignoreCase: true);
            filter = (_, rl, _) => rl >= minimumReportLevel;
        }
            
        Func<ILayout, Func<string, ReportLevel, string, bool>?, IAppender> currentAppenderFactory = AppenderFactories[appenderType];
        
        return currentAppenderFactory(layout, filter);
    }

    private static void Log(ILogger logger, string command)
    {
        string[] data = command.Split('|', StringSplitOptions.RemoveEmptyEntries);
            
        ReportLevel reportLevel = Enum.Parse<ReportLevel>(data[0], ignoreCase: true);
        string dateAndTime = data[1];
        string message = data[2];
            
        logger.Log(dateAndTime, reportLevel, message);
    }
}