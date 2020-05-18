using System;
using System.Collections.Generic;

namespace Mobile.RefApp.Lib.Intune.Logging
{
    public interface IInTuneLoggingService
    {
        List<LoggingMessage> Messages { get; set; }
        void AddMessage(LoggingMessage message);
        List<LoggingMessage> GetMessagesByModule(string module);
        void ClearAllMessages();
    }
}
