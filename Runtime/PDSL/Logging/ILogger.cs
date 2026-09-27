namespace Planeted
{
    public interface ILogger
    {
        void Log(LogLevelEnum logLevel, string message);        
    }
}