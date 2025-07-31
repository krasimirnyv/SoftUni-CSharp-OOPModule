using Logger.Enums;

namespace Logger.Interfaces;

public interface ILayout
{
    string Format(string dateAndTime, ReportLevel reportLevel, string message);
}