using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Core;

namespace Mobile.RefApp.CoreUI.Interfaces
{
    public interface IEndpointService
    {
        Task<IList<Endpoint>> GetEndpoints();
        Task<IList<Endpoint>> GetEndpointsByPlatform(Platform platform);
    }
}
