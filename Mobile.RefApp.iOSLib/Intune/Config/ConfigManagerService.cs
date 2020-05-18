using System;
using System.Collections.Generic;

using Mobile.RefApp.Lib.Logging;

using Foundation;

namespace Mobile.RefApp.iOSLib.Intune.Config
{
    public class ConfigManagerService
    {
        private readonly ILoggingService _loggingService;

        public ConfigManagerService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// GetConfigItems - pull app configuration from Intune
        /// https://docs.microsoft.com/en-us/intune/app-sdk-ios#enable-targeted-configuration-appmam-app-config-for-your-ios-applications   
        /// </summary>
        /// <returns>Tuple Dictionary of string, double, and bool app configuration items</returns>
        /// <param name="identity">string value of the user Identity to pull configuration for</param>
        public Tuple<Dictionary<string, string>,
            Dictionary<string, double>,
            Dictionary<string, bool>> GetConfigItems(string identity)
        {
            var stringValues = new Dictionary<string, string>();
            var numberValues = new Dictionary<string, double>();
            var boolValues = new Dictionary<string, bool>();

            try
            {
                var items = Microsoft.Intune.MAM.IntuneMAMAppConfigManager.Instance.AppConfigForIdentity(identity);
                var fullData = items.FullData;
                foreach (var i in fullData)
                {
                    foreach (NSString key in i.Keys)
                    {
                        var value = i.ValueForKey(key);
                        if (value is NSString)
                            stringValues.Add(key, (string)(value as NSString));
                        else if (value is NSNumber)
                            numberValues.Add(key, (double)(value as NSNumber));
                        else
                            boolValues.Add(key, (bool)(value as NSNumber));
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(ConfigManagerService), ex, "Error Converting Configuration Data");
            }

            return new Tuple<Dictionary<string, string>, Dictionary<string, double>, Dictionary<string, bool>>(stringValues, numberValues, boolValues);
        }
    }
}
