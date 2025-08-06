using Logger.Enums;
using Logger.Interfaces;

namespace Logger.Appenders;

public abstract class BaseAppender : IAppender
{
    private readonly ILayout _layout;
    private readonly Func<string, ReportLevel, string, bool>? _filter;
    
    protected BaseAppender(ILayout layout, Func<string, ReportLevel, string, bool>? filter)
    { 
        _layout = layout ?? throw new ArgumentNullException(nameof(layout));
        _filter = filter;
    }
    
    public bool Append(string dateAndTime, ReportLevel reportLevel, string message)
    {
        if(_filter is not null && !_filter(dateAndTime, reportLevel, message))
            return false;
        
        string formattedLogMessage = _layout.Format(dateAndTime, reportLevel, message);
        Append(formattedLogMessage);
        
        return true;
    }
    
    protected abstract void Append(string formattedLogMessage);
}