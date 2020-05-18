using System.Net.Http;
using Mobile.RefApp.Lib.Network;

namespace Mobile.RefApp.Droid.Network
{
	public class AndroidHttpClientHandler
		: IPlatformHttpClientHandler
	{
        public HttpClient GetHttpClient()
        {
            return new HttpClient(CreateHandler());
        }

        public HttpClientHandler CreateHandler()
		{
			return new Xamarin.Android.Net.AndroidClientHandler();
		}
	}
}