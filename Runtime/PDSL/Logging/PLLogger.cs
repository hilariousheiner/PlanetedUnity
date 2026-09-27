using UnityEngine;

namespace Planeted
{
    public class PLLogger : ILogger
    {
        public void Log(LogLevelEnum logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevelEnum.Message:
                    Debug.Log(message);
                    break;
                case LogLevelEnum.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogLevelEnum.Error:
                    Debug.LogError(message);
                    break;
                default:
                    break;
            }
        }
    }
}