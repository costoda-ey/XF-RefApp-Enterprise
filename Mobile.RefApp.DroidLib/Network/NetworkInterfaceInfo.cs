using System;
using System.Text;

using Java.Util;
using Mobile.RefApp.Lib.Logging;
using Mobile.RefApp.Lib.Network;

namespace Mobile.RefApp.DroidLib.Network
{
    public class NetworkInterfaceInfo 
        : INetworkInterfaceInfo
    {
        private readonly ILoggingService _loggingService;

        public NetworkInterfaceInfo(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public string GetIpAddresses()
        {
            StringBuilder ipAddresses = new StringBuilder();
            try
            {
                var netInterfaces = Collections.List(Java.Net.NetworkInterface.NetworkInterfaces);
                foreach (var ni in netInterfaces)
                {
                    var netInfo = ni as Java.Net.NetworkInterface;
                    if (netInfo.DisplayName.ToLower().Contains("eth")
                    || netInfo.DisplayName.ToLower().Contains("wlan"))
                    {
                        var addressInterfaces = netInfo.InterfaceAddresses;
                        foreach (var ai in addressInterfaces)
                        {
                            if (ai.Broadcast != null)
                            {
                                ipAddresses.Append($"{netInfo.Name} - {ai.Address.HostAddress}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(NetworkInterfaceInfo), ex, ex.Message);
            }

            return ipAddresses.ToString();
        }
    }
}
