using Logger.Enums;

namespace Logger.Interfaces;

public interface IAppender
{
    bool Append(string dateAndTime, ReportLevel reportLevel, string message);
}