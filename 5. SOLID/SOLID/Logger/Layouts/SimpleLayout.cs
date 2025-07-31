using Logger.Enums;
using Logger.Interfaces;

namespace Logger.Layouts;

public class SimpleLayout : ILayout
{
    public string Format(string dateAndTime, ReportLevel reportLevel, string message)
        => $"{dateAndTime} - {reportLevel} - {message}";
}