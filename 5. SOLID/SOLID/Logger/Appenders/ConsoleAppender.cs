using Logger.Enums;
using Logger.Interfaces;

namespace Logger.Appenders;

public class ConsoleAppender : BaseAppender
{
    public ConsoleAppender(ILayout layout, Func<string, ReportLevel, string, bool>? filter = null) 
        : base(layout, filter)
    {
    }

    protected override void Append(string formattedLogMessage)
        => Console.WriteLine(formattedLogMessage);
} 