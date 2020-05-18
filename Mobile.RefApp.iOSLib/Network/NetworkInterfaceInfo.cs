using System;
using System.Text;

using System.Net.NetworkInformation;
using System.Net.Sockets;

using Mobile.RefApp.Lib.Network;
using Mobile.RefApp.Lib.Logging;

namespace Mobile.RefApp.iOSLib.Network
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
                foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                        netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                        netInterface.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                    {
                        foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                                {
                                    ipAddresses.Append($"Wireless: {addrInfo.Address} ");
                                }
                                else if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                                {
                                    ipAddresses.Append($"Ethernet: {addrInfo.Address} ");
                                }
                                else if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                                {
                                    ipAddresses.Append($"Tunnel: {addrInfo.Address} ");
                                }

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
