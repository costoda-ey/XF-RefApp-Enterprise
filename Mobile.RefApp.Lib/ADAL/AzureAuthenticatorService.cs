using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Mobile.RefApp.Lib.Logging;
using Mobile.RefApp.Lib.Network;

namespace Mobile.RefApp.Lib.ADAL
{
    public class AzureAuthenticatorService
		: IAzureAuthenticatorEndpointService
    {
        private readonly ILoggingService _loggingService;
        private readonly IAzurePlatformParameters _azurePlatformParameters;
        private readonly HttpClientHandler _httpClient;
        private readonly StringBuilder _logBuilder = new StringBuilder();

        public AzureAuthenticatorService(IAzurePlatformParameters azurePlatformParameters,
                                  IPlatformHttpClientHandler httpClientHandler,
                                  ILoggingService loggingService)
        {
            _azurePlatformParameters = azurePlatformParameters;
            _httpClient = httpClientHandler.CreateHandler();
            _loggingService = loggingService;
        }

        public async Task<CacheToken> Authenticate(string applicationId,
															 string authority,
															 string returnUri,
															 string resourceId,
															 bool useBroker = true,
															 [CallerMemberName] string memberName = "",
															 [CallerLineNumber] int lineNumber = 0)
        { 
			return await AuthenticateEndpoint(new Endpoint()
			{
				Authority = authority,
				ApplicationId = applicationId,
				Environment = Network.Environment.Production,
				IsActive = true,
				Name = string.Empty,
				RedirectUri = returnUri,
				ResourceId = resourceId,
				UseBroker = useBroker
			},
			memberName,
			lineNumber);
        }

        public async Task<CacheToken> AuthenticateEndpoint(Endpoint endpoint,
		  [CallerMemberName] string memberName = "",
		  [CallerLineNumber] int lineNumber = 0)


        {
            if (endpoint.IsActive)
            { 
                AuthenticationResult results = null;
                IPlatformParameters platformParameters = _azurePlatformParameters.GetPlatformParameters(endpoint.UseBroker);
                try
                {
                    var authContext = new AuthenticationContext(endpoint.Authority);
                    //fixes for security groups in iOS per
                    //https://aka.ms/adal-net-ios-keychain-access

#if iOS
                    authContext.iOSKeychainSecurityGroup = endpoint.iOSKeychainSecurityGroup;
#endif

                    _logBuilder.Clear();

                    if (endpoint.EnableLogging)
                    {
                        LoggerCallbackHandler.LogCallback = AdalLog;
                        LoggerCallbackHandler.PiiLoggingEnabled = endpoint.EnableLogging;
                    }

                    if (string.IsNullOrEmpty(endpoint.ExtraParameters))
                    {
                        results = await authContext.AcquireTokenAsync(endpoint.ResourceId,
                            endpoint.ApplicationId,
                            new Uri(endpoint.RedirectUri),
                            platformParameters).WithTimeout(5000);
                    }
                    else
                    {
                        results = await authContext.AcquireTokenAsync(endpoint.ResourceId,
                            endpoint.ApplicationId,
                            new Uri(endpoint.RedirectUri),
                            platformParameters,
                            UserIdentifier.AnyUser,
                            endpoint.ExtraParameters).WithTimeout(5000);
                    }
                }
                catch (AdalUserMismatchException aume)
                {
                    if (endpoint.EnableLogging)
                    {
                        _loggingService.LogError(typeof(AzureAuthenticatorService),
                                             (System.Exception)aume,
                                             $"{Constants.AzureAuthenticator.AZURELOGERRORTAG} :AdalUserMismatchException",
                                             memberName,
                                             lineNumber,
                                             $"Returned User: {aume?.ReturnedUser} Requested User: {aume?.RequestedUser}",
                                             $"Error Code:: {aume?.ErrorCode}");
                    }
                }
                catch (AdalSilentTokenAcquisitionException astae)
                {
                    if (endpoint.EnableLogging)
                    {
                        _loggingService.LogError(typeof(AzureAuthenticatorService),
                                             (System.Exception)astae,
                                             $"AdalSilientTokenAquisitionException {astae?.ErrorCode}",
                                             memberName,
                                             lineNumber,
                                             astae.Data);
                    }
                }
                catch (AdalClaimChallengeException acce)
                {
                    if (endpoint.EnableLogging)
                    {
                        _loggingService.LogError(typeof(AzureAuthenticatorService),
                                            (System.Exception)acce,
                                            $"AdalClaimsChallengeException:: Claims: {acce.Claims}",
                                             memberName,
                                             lineNumber,
                                             acce.Data,
                                             acce.Headers,
                                             acce.ServiceErrorCodes,
                                             acce.StatusCode);
                    }
                    throw;
                }
                catch (AdalServiceException ase)
                {
                    if (endpoint.EnableLogging)
                    {
                        _loggingService.LogError(typeof(AzureAuthenticatorService),
                                            (System.Exception)ase,
                                            $"{Constants.AzureAuthenticator.AZURELOGERRORTAG}: AdalServiceException::",
                                            memberName,
                                            lineNumber,
                                            ase.Data,
                                            ase.Headers,
                                            ase.ServiceErrorCodes,
                                            ase.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    if (endpoint.EnableLogging)
                    {
                        _loggingService.LogError(typeof(AzureAuthenticatorService),
                                            e,
                                            e.Message,
                                            memberName,
                                            lineNumber,
                                            null);
                    }
                }

                return CacheToken.GetCacheToken(endpoint, results);
            }
            else
                throw new Exception("ERROR:  Endpoint not Active, please make Endpoint Active and try again.");
        }


        public async Task<CacheToken> AcquireTokenSilentAsync(Endpoint endpoint, 
                                                        [CallerMemberName] string memberName = "",
                                                        [CallerLineNumber] int lineNumber = 0)

        {
            AuthenticationResult results = null;
            try
            {
                var authContext = new AuthenticationContext(endpoint.Authority);
                //fixes for security groups in iOS per
                //https://aka.ms/adal-net-ios-keychain-access

#if iOS
                    authContext.iOSKeychainSecurityGroup = endpoint.iOSKeychainSecurityGroup;
#endif

                _logBuilder.Clear();
                if (endpoint.EnableLogging)
                {
                    LoggerCallbackHandler.LogCallback = AdalLog;
                    LoggerCallbackHandler.PiiLoggingEnabled = true;
                }

                results = await authContext.AcquireTokenSilentAsync(endpoint.ResourceId, endpoint.ApplicationId);
            }
            catch (AdalUserMismatchException aume)
            {
                if (endpoint.EnableLogging)
                {
                    _loggingService.LogError(typeof(AzureAuthenticatorService),
                                         (System.Exception)aume,
                                         $"{Constants.AzureAuthenticator.AZURELOGERRORTAG} :AdalUserMismatchException",
                                         memberName,
                                         lineNumber,
                                         $"Returned User: {aume?.ReturnedUser} Requested User: {aume?.RequestedUser}",
                                         $"Error Code:: {aume?.ErrorCode}");
                }
            }
            catch (AdalSilentTokenAcquisitionException astae)
            {
                if (endpoint.EnableLogging)
                {
                    _loggingService.LogError(typeof(AzureAuthenticatorService),
                                         (System.Exception)astae,
                                         $"AdalSilientTokenAquisitionException {astae?.ErrorCode}",
                                         memberName,
                                         lineNumber,
                                         astae.Data);
                }
                //get the token regularly since it's not in cache
                //per documentation:  https://github.com/AzureAD/azure-activedirectory-library-for-dotnet/wiki/AcquireTokenSilentAsync-using-a-cached-token
                return await AuthenticateEndpoint(endpoint);
            }
            catch (AdalClaimChallengeException acce)
            {
                if (endpoint.EnableLogging)
                {
                    _loggingService.LogError(typeof(AzureAuthenticatorService),
                                        (System.Exception)acce,
                                        $"AdalClaimsChallengeException:: Claims: {acce.Claims}",
                                         memberName,
                                         lineNumber,
                                         acce.Data,
                                         acce.Headers,
                                         acce.ServiceErrorCodes,
                                         acce.StatusCode);
                }
            }
            catch (AdalServiceException ase)
            {
                if (endpoint.EnableLogging)
                {
                    _loggingService.LogError(typeof(AzureAuthenticatorService),
                                        (System.Exception)ase,
                                        $"{Constants.AzureAuthenticator.AZURELOGERRORTAG}: AdalServiceException::",
                                        memberName,
                                        lineNumber,
                                        ase.Data,
                                        ase.Headers,
                                        ase.ServiceErrorCodes,
                                        ase.StatusCode);
                }
            }
            catch (Exception e)
            {
                if (endpoint.EnableLogging)
                {
                    _loggingService.LogError(typeof(AzureAuthenticatorService),
                                        e,
                                        e.Message,
                                        memberName,
                                        lineNumber,
                                        null);
                }
            }

            return CacheToken.GetCacheToken(endpoint, results);
        }

        public IEnumerable<TokenCacheItem> GetCachedTokens(
            Endpoint endpoint,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            IEnumerable<TokenCacheItem> tokenCache = null;
            try
            {
                var authContext = new AuthenticationContext(endpoint.Authority);
                tokenCache = authContext.TokenCache.ReadItems();
            }
            catch (Exception ex)
            {
                if (endpoint.EnableLogging)
                {
                    _loggingService.LogError(typeof(AzureAuthenticatorService), ex, ex.Message);
                }
            }

            return tokenCache;
        }

        public IEnumerable<TokenCacheItem> GetCachedTokens(
            string authority,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            IEnumerable<TokenCacheItem> tokenCache = null;
            try
            {
                var authContext = new AuthenticationContext(authority);
                tokenCache = authContext.TokenCache.ReadItems();
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(AzureAuthenticatorService), ex, ex.Message);
            }

            return tokenCache;
        }

        public bool ClearCachedTokens(           
            Endpoint endpoint,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            bool results = false;
            try
            {
                var authContext = new AuthenticationContext(endpoint.Authority);
                authContext.TokenCache.Clear();
                results |= authContext.TokenCache.Count == 0;
            }
            catch (Exception ex)
            {
                if (endpoint.EnableLogging)
                {
                    _loggingService.LogError(typeof(AzureAuthenticatorService), ex, ex.Message);
                }
            }
            return results;
        }

        private void AdalLog(LogLevel level,
                             string message,
                             bool containsPii)
        {
            string pii = containsPii ? ":pii" : "";
            _logBuilder.AppendLine($"[{level}{pii}] {message}");
        }

        private object[] ComposePropertyValues(Endpoint endpoint)
        {
            return new List<object>
            {
                endpoint.ApplicationId,
                endpoint.Authority,
                endpoint.Environment,
                endpoint.IsActive,
                endpoint.Name,
                endpoint.ResourceId,
                endpoint.RedirectUri,
                endpoint.UseBroker,
            }.ToArray();
        }
    }
}
