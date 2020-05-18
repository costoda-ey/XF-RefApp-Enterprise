using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Mobile.RefApp.Lib.ADAL
{
    public interface IAzureAuthenticatorEndpointService
        : IAzureAuthenticatorService
    {
        Task<CacheToken> AuthenticateEndpoint(
            Endpoint endpoint,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0);

        Task<CacheToken> AcquireTokenSilentAsync(
            Endpoint endpoint,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0);

        IEnumerable<TokenCacheItem> GetCachedTokens(
            string authority,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0);

        IEnumerable<TokenCacheItem> GetCachedTokens(
            Endpoint endpoint,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0);

        bool ClearCachedTokens(
            Endpoint endpoint,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0);
    }
}
