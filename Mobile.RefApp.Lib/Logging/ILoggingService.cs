using System;
using System.Runtime.CompilerServices;

namespace Mobile.RefApp.Lib.Logging
{
    public interface ILoggingService
    {
        void LogDebug(Type context,
                        string message,
                        [CallerMemberName] string memberName = "",
                        [CallerLineNumber] int lineNumber = 0,
                        params object[] propertyValues);

        void LogInformation(Type context,
                        string message,
                        [CallerMemberName] string memberName = "",
                        [CallerLineNumber] int lineNumber = 0,
                        params object[] propertyValues);

        void LogError(Type context,
                        Exception ex,
                        string message,
                        [CallerMemberName] string memberName = "",
                        [CallerLineNumber] int lineNumber = 0,
                        params object[] propertyValues);
    }
}
