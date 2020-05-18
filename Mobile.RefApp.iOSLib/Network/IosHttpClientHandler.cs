using Mobile.RefApp.Lib.Network;

using System.Net.Http;

namespace Mobile.RefApp.iOSLib.Network
{
	public class IosHttpClientHandler
	 : IPlatformHttpClientHandler
	{
        public HttpClient GetHttpClient()
        {
            return new HttpClient(CreateHandler());
        }

        public HttpClientHandler CreateHandler()
		{
			return new HttpClientHandler();
		}
	}
}