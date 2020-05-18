using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AppCenter.Analytics;

namespace Mobile.RefApp.Lib.AppCenter
{
    public static class AnalyticsHelper
    {
        private static string _userIdentity;
        private static string _fullName;
        private static bool _useInEvent;

        public static void SetUserIdentity(string userIdentity, string fullName, bool useInEvents)
        {
            _userIdentity = userIdentity;
            _fullName = fullName;
            _useInEvent = useInEvents;

            Microsoft.AppCenter.AppCenter.SetUserId(userIdentity);
        }

        public static Task<bool> IsEnabledAsync()
        {
            return Analytics.IsEnabledAsync();
        }

        public static Task EnableAsync(bool isEnabled)
        {
            return Analytics.SetEnabledAsync(isEnabled);
        }

        public static void TrackEvent(string name, Dictionary<string, string> eventDetails = null)
        {
            if (_useInEvent)
            {
                if (!string.IsNullOrEmpty(_userIdentity) && !string.IsNullOrEmpty(_fullName))
                    eventDetails.Add("User", $"{_userIdentity} - {_fullName}");
                else if (!string.IsNullOrEmpty(_userIdentity))
                {
                    eventDetails.Add("User", $"{_userIdentity}");
                }
            }
            Analytics.TrackEvent(name, eventDetails);
        }

    }
}
