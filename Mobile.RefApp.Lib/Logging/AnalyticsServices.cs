using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AppCenter.Analytics;

namespace Mobile.RefApp.Lib.Logging
{
    public class AnalyticsServices
        : IAnalyticsServices
    {
        public Task<bool> IsEnabledAsync()
        {
            return Analytics.IsEnabledAsync();
        }

        public async Task SetEnabledAsync(bool value)
        {
            await Analytics.SetEnabledAsync(value);
        }

        public void TrackEvent(string eventName, Dictionary<string, string> eventProperties = null)
        {
            if (eventProperties != null)
            {
                if (eventProperties.Count <= 20 && eventName.Length <= 256)
                    Analytics.TrackEvent(eventName, eventProperties);
                else
                    throw new Exception("Error: event name or event properties do not meet App Center requirements");
            }
            else
                Analytics.TrackEvent(eventName);
        }
    }
}
