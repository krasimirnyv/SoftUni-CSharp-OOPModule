using Logger.Enums;
using Logger.Interfaces;

namespace Logger.Loggers;

public class Logger : ILogger
{
    private readonly IAppender[] _appenders;
    
    public Logger(params IAppender[] appenders)
    {
        _appenders = appenders ?? throw new ArgumentNullException(nameof(appenders));
    }

    public void Log(string dateAndTime, ReportLevel reportLevel, string message)
    {
        foreach (IAppender appender in _appenders)
            appender.Append(dateAndTime, reportLevel, message);
    }
}