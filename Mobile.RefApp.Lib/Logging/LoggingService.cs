using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AppCenter.Crashes;
using Serilog;
using Serilog.Events;

namespace Mobile.RefApp.Lib.Logging
{
	public class LoggingService
		: ILoggingService
	{

		private readonly bool IsDebug = Log.IsEnabled(LogEventLevel.Debug);

		public void LogDebug(Type context,
							string message,
							[CallerMemberName] string memberName = "",
							[CallerLineNumber] int lineNumber = 0,
							params object[] propertyValues)
		{
			var log = Log.ForContext(context);
            if (IsDebug)
            {
                log.Debug($"{{CallingMember:}} {message}", ComposePropertyValues(memberName, lineNumber, propertyValues));
            }
		}

		public void LogError(Type context,
							Exception ex,
							string message,
							[CallerMemberName] string memberName = "",
							[CallerLineNumber] int lineNumber = 0,
							params object[] propertyValues)
		{
			var log = Log.ForContext(context);
			log.Error(ex, $"{{CallingMember:}} {message}", ComposePropertyValues(memberName, lineNumber, propertyValues));
            Crashes.TrackError(ex, ComposeProperty(propertyValues)); 
		}

		public void LogInformation(Type context,
								string message,
								[CallerMemberName] string memberName = "",
								[CallerLineNumber] int lineNumber = 0,
								params object[] propertyValues)
		{
			var log = Log.ForContext(context);
			log.Information($"{{CallingMember}} {message}", ComposePropertyValues(memberName, lineNumber, propertyValues));
		}

        private Dictionary<string, string> ComposeProperty(object[] propertyValues)
        {
            var col = new Dictionary<string, string>();
            if (propertyValues != null)
            {
                foreach (var o in propertyValues)
                {
                    col.Add(o.GetType().ToString(), o.ToString());
                }
            }
            return col;
        }

        private object[] ComposePropertyValues(string memberName,
									   int lineNumber,
									   object[] propertyValues)
		{
			if (propertyValues == null || propertyValues.Length == 0)
			{
				return new object[] { memberName, $"line number: {lineNumber}" };
			}
			else
			{
				var l = new List<object>(propertyValues);
				l.Insert(0, memberName);
				l.Insert(1, $"line number: {lineNumber}");
				return l.ToArray();
			}
		}
	}
}
