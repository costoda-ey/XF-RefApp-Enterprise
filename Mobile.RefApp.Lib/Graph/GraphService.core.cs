using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Logging;
using Mobile.RefApp.Lib.Network;

using Newtonsoft.Json;
namespace Mobile.RefApp.Lib.Graph
{
    public partial class GraphService
        : IGraphService
    {
        private readonly ILoggingService _loggingService;
        private readonly IPlatformHttpClientHandler _httpClientHandler;
        private readonly IAzureAuthenticatorEndpointService _azureAuthenticatorEndpointService;
        private readonly HttpClient _httpClient;

        public GraphService(
            ILoggingService loggingService,
            IPlatformHttpClientHandler platformHttpClientHandler,
            IAzureAuthenticatorEndpointService azureAuthenticatorEndpointService)
        {
            _loggingService = loggingService;
            _httpClientHandler = platformHttpClientHandler;
            _azureAuthenticatorEndpointService = azureAuthenticatorEndpointService;

            //get httpclient and token for accessing graph api
            _httpClient = _httpClientHandler.GetHttpClient();
        }


        private async Task<T> GetItemsAsync<T>(string query, ADAL.Endpoint endpoint, bool includeODataMetadata = false)
        {
            try
            {
                var didSetupHttpClient = await SetupHttpClient(endpoint);
                if (didSetupHttpClient)
                {
                    var uri = $"{Constants.GraphAPIRootQueryV1}/{query}";
                    if (!includeODataMetadata)
                    {
                        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata.metadata=none");
                    }
                    var results = await _httpClient.GetAsync(uri);
                    if (results != null && results.IsSuccessStatusCode)
                    {
                        var content = await results.Content.ReadAsStringAsync();
                        _loggingService.LogDebug(typeof(GraphService), $"{DateTime.Now}:  Received JSON information from URI {uri} Content: {content}");
                        var item = JsonConvert.DeserializeObject<T>(content);
                        return item;
                    }
                    else
                    {
                        var content = await results.Content.ReadAsStringAsync();
                        var errorMessage = $"Error: {results.StatusCode} Content: {content}";
                        Console.WriteLine(errorMessage);
                        throw new Exception(errorMessage);
                    }

                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, $"Error trying to return GetItemsAsync");
                System.Console.WriteLine($"Error trying to return GetItemsAsync {ex.Message} {ex.StackTrace}");
            }
            return default;
        }

        private async Task<byte[]> GetFileAsync(string query, ADAL.Endpoint endpoint)
        {
            try
            {
                var uri = $"{Constants.GraphAPIRootQueryV1}/{query}";
                var didSetupHttpClient = await SetupHttpClient(endpoint);
                if (didSetupHttpClient)
                {
                    _httpClient.DefaultRequestHeaders.Remove("Accept");
                    var results = await _httpClient.GetAsync(uri);
                    if (results != null && results.IsSuccessStatusCode)
                    {
                        var content = await results.Content.ReadAsByteArrayAsync();
                        return content;
                    }
                    else
                    {
                        var content = await results.Content.ReadAsStringAsync();
                        var errorMessage = $"Error: {uri} - {results.StatusCode} Content: {content}";
                        throw new Exception(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, $"Error trying to return GetItemsAsync");
                System.Console.WriteLine($"Error trying to return GetFileAsync {ex.Message} {ex.StackTrace}");
            }
            return null;
        }

        private async Task<bool> SetupHttpClient(ADAL.Endpoint endpoint)
        {
            var token = await _azureAuthenticatorEndpointService.AcquireTokenSilentAsync(endpoint);
            if (token == null || string.IsNullOrEmpty(token.Token))
            {
                token = await _azureAuthenticatorEndpointService.AuthenticateEndpoint(endpoint);
            }
            if (token != null)
            {
                var authValue = new AuthenticationHeaderValue("Bearer", token.Token);
                _httpClient.DefaultRequestHeaders.Authorization = authValue;
                return true;
            }
            else
            {
                throw new Exception(Constants.HttpClientErrorNoToken);
            }
        }

    }
}
