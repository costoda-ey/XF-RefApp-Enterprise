using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.RefApp.Lib.Logging
{
    public interface IAnalyticsServices
    {
        Task SetEnabledAsync(bool value);
        Task<bool> IsEnabledAsync();

        void TrackEvent(string eventName, 
                        Dictionary<string, string> eventProperties = null);
    }
}
