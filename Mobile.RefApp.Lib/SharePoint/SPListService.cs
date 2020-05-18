using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Logging;
using Mobile.RefApp.Lib.Network;

using Newtonsoft.Json;

namespace Mobile.RefApp.Lib.SharePoint
{
    public class SPListService
        : ISPListService
    {
        private readonly ILoggingService _loggingService;
        private readonly IPlatformHttpClientHandler _httpClientHandler;
        private readonly IAzureAuthenticatorEndpointService _azureAuthenticatorEndpointService;
        private readonly HttpClient _httpClient;
        private readonly string _endpointSharePointFilePath = "_api/web/GetFileByServerRelativeUrl";

        public ADAL.Endpoint Endpoint { get; set; }
        public SharePointEndpoint SharePointEndpoint { get; set; }

        public string SharePointApiRootQuery => $"{SharePointEndpoint.SiteUrl}/{SharePointEndpoint.ApiVersion}/sites/{SharePointEndpoint.RootSiteUrl},{SharePointEndpoint.RootSiteId}";

        public SPListService(
            ILoggingService loggingService,
            IPlatformHttpClientHandler platformHttpClientHandler,
            IAzureAuthenticatorEndpointService azureAuthenticatorEndpointService)
        {
            _loggingService = loggingService;
            _httpClientHandler = platformHttpClientHandler;
            _azureAuthenticatorEndpointService = azureAuthenticatorEndpointService;

            _httpClient = _httpClientHandler.GetHttpClient();
        }

        public Task<byte[]> GetFileAsync(
            ADAL.Endpoint endPoint,
            string siteUrl,
            string apiVersion,
            string rootSiteUrl,
            string rootSiteId,
            string siteName,
            string fileUrl)
        {
            Endpoint = endPoint;
            SharePointEndpoint = new SharePointEndpoint
            {
                SiteUrl = siteUrl,
                ApiVersion = apiVersion,
                RootSiteUrl = rootSiteUrl,
                RootSiteId = new Guid(rootSiteId)
            };

            return GetFileAsync(siteName, fileUrl);
        }

        public Task<byte[]> GetFileAsync(
            ADAL.Endpoint endPoint,
            SharePointEndpoint sharePointEndpoint,
            string siteName,
            string fileUrl)
        {
            Endpoint = endPoint;
            SharePointEndpoint = sharePointEndpoint;
            return GetFileAsync(siteName, fileUrl);
        }

        public async Task<byte[]> GetFileAsync(
            string siteName,
            string url)
        {
            try
            {
                if (Endpoint == null || SharePointEndpoint == null)
                {
                    throw new Exception("EndPoint is null, please set endpoint before trying again.");
                }

                var didSetupHttpClient = await SetupHttpClient();
                if (didSetupHttpClient)
                {
                    var fullUrl = $"{SharePointEndpoint.SiteUrl}/{SharePointEndpoint.RootSiteUrl}/{siteName}/{_endpointSharePointFilePath}('{url}')/$value";
                    _httpClient.DefaultRequestHeaders.Remove("Accept");

                    var results = await _httpClient.GetAsync(fullUrl);
                    if (results != null && results.IsSuccessStatusCode)
                    {
                        var content = await results.Content.ReadAsByteArrayAsync();
                        return content;
                    }
                    else
                    {
                        var content = await results.Content.ReadAsStringAsync();
                        var errorMessage = $"Error: {fullUrl} - {results.StatusCode} Content: {content}";
                        throw new Exception(errorMessage);
                    }
                }
                else
                {
                    throw new Exception("Couldn't setup httpClient, this usually means couldn't get a token from ADAL.");
                }

            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(SPListService), ex, $"Error trying to return GetAsync");
            }
            return null;
        }

        public Task<T> GetListItemsAsync<T>(
            ADAL.Endpoint endPoint,
            string siteUrl,
            string apiVersion,
            string rootSiteUrl,
            string rootSiteId,
            string list,
            string query)
        {
            Endpoint = endPoint;
            SharePointEndpoint = new SharePointEndpoint
            {
                SiteUrl = siteUrl,
                ApiVersion = apiVersion,
                RootSiteUrl = rootSiteUrl,
                RootSiteId = new Guid(rootSiteId)
            };

            return GetListItemsAsync<T>(list, query);
        }

        public Task<T> GetListItemsAsync<T>(
            ADAL.Endpoint endPoint,
            SharePointEndpoint sharePointEndPoint,
            string list,
            string query)
        {
            Endpoint = endPoint;
            SharePointEndpoint = sharePointEndPoint;

            return GetListItemsAsync<T>(list, query);
        }

        public async Task<T> GetListItemsAsync<T>(string list, string query)
        {
            try
            {
                if (Endpoint == null || SharePointEndpoint == null)
                {
                    throw new Exception("EndPoint is null, please set endpoint before trying again.");
                }

                var didSetupHttpClient = await SetupHttpClient();
                if (didSetupHttpClient)
                {
                    var uri = $"{SharePointApiRootQuery}/lists/{list}/items?{query}";
                    _httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata.metadata=none");
                    var results = await _httpClient.GetAsync(uri);
                    if (results != null && results.IsSuccessStatusCode)
                    {
                        var content = await results.Content.ReadAsStringAsync();
                        var item = JsonConvert.DeserializeObject<T>(content);
                        return item;
                    }
                    else
                    {
                        var content = await results.Content.ReadAsStringAsync();
                        var errorMessage = $"Error: {results.StatusCode} Content: {content}";
                        throw new Exception(errorMessage);
                    }
                }
                else
                {
                    throw new Exception("Couldn't setup httpClient, this usually means couldn't get a token from ADAL.");
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(SPListService), ex, $"Error trying to return GetItemsAsync");
            }
            return default;
        }

        private async Task<bool> SetupHttpClient()
        {
            CacheToken token = null;

            token = await _azureAuthenticatorEndpointService.AcquireTokenSilentAsync(Endpoint);
            if (token == null || string.IsNullOrEmpty(token.Token))
            {
                token = await _azureAuthenticatorEndpointService.AuthenticateEndpoint(Endpoint);
            }
            if (token != null)
            {
                var authValue = new AuthenticationHeaderValue("Bearer", token.Token);
                _httpClient.DefaultRequestHeaders.Authorization = authValue;
                return true;
            }
            return false;
        }
    }
}
