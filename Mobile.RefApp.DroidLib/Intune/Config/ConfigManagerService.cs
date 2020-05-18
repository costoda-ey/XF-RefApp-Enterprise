using System;
using System.Collections.Generic;

using Mobile.RefApp.Lib.Logging;

using Microsoft.Intune.Mam.Client.App;
using Microsoft.Intune.Mam.Policy.AppConfig;

namespace Mobile.RefApp.DroidLib.Intune.Config
{
    public class ConfigManagerService
    {
        private readonly ILoggingService _loggingService;
        private readonly IMAMAppConfigManager _configManager;

        public ConfigManagerService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
            _configManager = MAMComponents.Get<IMAMAppConfigManager>();
        }

        /// <summary>
        /// GetConfigItems - pull app configuration from Intune
        /// https://docs.microsoft.com/en-us/intune/app-sdk-android#enabling-mam-targeted-configuration-for-your-android-applications-optional
        /// </summary>
        /// <returns>Tuple Dictionary of string, double, and bool app configuration items</returns>
        /// <param name="identity">string value of the user Identity to pull configuration for</param>
        public Tuple<List<Dictionary<string, string>>,
            List<Dictionary<string, double>>,
            List<Dictionary<string, bool>>> GetConfigItems(string identity)
        {
            var stringValues = new List<Dictionary<string, string>>();
            var numberValues = new List<Dictionary<string, double>>();
            var boolValues = new List<Dictionary<string, bool>>();
            try
            {
                var items = _configManager.GetAppConfig(identity);
                foreach (var item in items.FullData)
                {
                    var dict = new Dictionary<string, string>();
                    foreach (var key in item.Keys)
                    {
                        var value = item[key];
                        dict.Add(key, value);
                    }
                    stringValues.Add(dict);
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(ConfigManagerService), ex, "Error Converting Configuration Data");
            }

            return new Tuple<List<Dictionary<string, string>>, List<Dictionary<string, double>>, List<Dictionary<string, bool>>>(stringValues, numberValues, boolValues);
        }
    }
}
