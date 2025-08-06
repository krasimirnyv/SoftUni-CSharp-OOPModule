using Logger.Enums;
using Logger.Interfaces;

namespace Logger.Appenders;

public class FileAppender : BaseAppender
{
    private readonly string _pathToFile;

    public FileAppender(string pathToFile, ILayout layout, Func<string, ReportLevel, string, bool>? filter = null) 
        : base(layout, filter)
    {
        _pathToFile = string.IsNullOrWhiteSpace(pathToFile) ? throw new ArgumentNullException(nameof(pathToFile)) : pathToFile;
    }

    protected override void Append(string formattedLogMessage)
        => File.AppendAllLines(_pathToFile, [formattedLogMessage]);
}