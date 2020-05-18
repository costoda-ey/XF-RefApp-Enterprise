using System;
using System.Linq;
using System.Collections.Generic;

namespace Mobile.RefApp.Lib.Intune.Logging
{
    public sealed class InTuneLoggingService
    {
        private static readonly Lazy<InTuneLoggingService> lazy = new Lazy<InTuneLoggingService>(() => new InTuneLoggingService());

        public static InTuneLoggingService Instance { get => lazy.Value; }

        public List<LoggingMessage> Messages { get; set; }

        private InTuneLoggingService()
        {
            Messages = new List<LoggingMessage>();
        }

        public void AddMessage(LoggingMessage message)
        {
            Messages.Add(message);
        }

        public List<LoggingMessage> GetMessagesByModule(string module)
        {
            return Messages.Where(x => x.Module == module).ToList();
        }

        public void ClearAllMessages()
        {
            Messages.Clear();
        }
    }
}
