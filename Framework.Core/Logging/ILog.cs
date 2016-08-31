using System;
namespace Framework.Core.Logging
{
    public interface ILog
    {
        void LogDebug(string debugMessage);
        void LogError(Exception exception);
        void LogError(Exception exception, bool isNotify);
        void LogError(string message, Exception exception);
        void LogError(string message, Exception exception, bool isNotify);
        void LogInfo(string info);
        void LogInfo(string info, bool isNotify);
        void LogWarn(string warn);
    }
}
