using System.Threading.Tasks;

using Microsoft.AppCenter;

namespace Mobile.RefApp.Lib.AppCenter
{
    public static class Config
    {
        public static string SDKVersion => Microsoft.AppCenter.AppCenter.SdkVersion;

        public static LogLevel LoggingLevel
        {
            get => Microsoft.AppCenter.AppCenter.LogLevel;
            set => Microsoft.AppCenter.AppCenter.LogLevel = value;
        }

        public static async Task<bool> IsEnabled()
        {
            return await Microsoft.AppCenter.AppCenter.IsEnabledAsync();
        }

        public static async Task SetEnabledAsync(bool isEnabled)
        {
            await Microsoft.AppCenter.AppCenter.SetEnabledAsync(isEnabled);
        }
    }
}
