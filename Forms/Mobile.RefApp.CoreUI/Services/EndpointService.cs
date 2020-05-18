using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

using Mobile.RefApp.CoreUI.Interfaces;
using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Core;
using Mobile.RefApp.Lib.IO;
using Mobile.RefApp.Lib.Logging;

using Newtonsoft.Json;

namespace Mobile.RefApp.CoreUI.Services
{
    public class EndpointService
        : IEndpointService
    {
        private readonly ILoggingService _loggingService;

        public EndpointService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public async Task<IList<Endpoint>> GetEndpoints()
        {
            List<Endpoint> endpoints = null; 
            try
            {
                var resourcePath = "Mobile.RefApp.CoreUI.Assets.Data.Endpoints.json";
                var assembly = typeof(EndpointService).GetTypeInfo().Assembly;
                var jsonData = await EmbeddedFileHelper.GetResourceString(assembly, resourcePath);

                if (!string.IsNullOrEmpty(jsonData))
                {
                    endpoints = JsonConvert.DeserializeObject<List<Endpoint>>(jsonData); 
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(EndpointService), ex, ex.Message);
            }
            return endpoints;
        }

        public async Task<IList<Endpoint>> GetEndpointsByPlatform(Platform platform)
        {
            List<Endpoint> endpoints = null;
            try 
            {
                endpoints = (await GetEndpoints()).Where(x => x.Platform == platform).ToList();
            }
            catch(Exception ex)
            {
                _loggingService.LogError(typeof(EndpointService), ex, ex.Message);
            }

            return endpoints;
        }
    }
}
