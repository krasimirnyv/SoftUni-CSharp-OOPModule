using System.Text;
using Logger.Enums;
using Logger.Interfaces;

namespace Logger.Layouts;

public class XmlLayout : ILayout
{
    public string Format(string dateAndTime, ReportLevel reportLevel, string message)
    {
        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine("<log>");
        sb.AppendLine($"\t<date>{dateAndTime}</date>");
        sb.AppendLine($"\t<level>{reportLevel}</level>");
        sb.AppendLine($"\t<message>{message}</message>");
        sb.Append("</log>");

        return sb.ToString();
    }
}