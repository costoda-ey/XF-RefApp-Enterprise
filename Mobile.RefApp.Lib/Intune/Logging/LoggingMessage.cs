using System;

namespace Mobile.RefApp.Lib.Intune.Logging
{
    public static class SDKModule
    {
        public static string Enrollment = "Enrollment";
        public static string Policies = "Policies";
        public static string Configuration = "Configuration";
        public static string Unenrollment = "Unenrollment";
    }

    public class LoggingMessage
    {
        public string LogDateDisplay => LogDate.ToLocalTime().ToString();
        public DateTimeOffset LogDate { get; set; }
        public string Message { get; set; }
        public string Module { get; set; }
    }
}
