using System.Net.Http;

namespace Mobile.RefApp.Lib.Network
{
    public interface IPlatformHttpClientHandler
    {
        HttpClientHandler CreateHandler();
        HttpClient GetHttpClient();
    }
}
