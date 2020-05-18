using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Mobile.RefApp.Lib.ADAL
{
    public class CacheToken
    {
        public Endpoint Endpoint { get; set; }
        public string Name => Endpoint.Name;
        public string ResourceId => Endpoint.ResourceId; 
        public string Token { get; set; }
        public string TokenType { get; set; }
        public string IdToken { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public string ExpiresOnDisplay => ExpiresOn.ToString();

        public string TenantId { get; set; }

        public UserInfo UserInfo { get; set; }
        public string UserFullname => $"{UserInfo.GivenName} {UserInfo.FamilyName}";

        public IList<KeyValuePair<string, string>> GetTokenDetails()
        {
            var data = new List<KeyValuePair<string, string>>();
            var handler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(Token) && handler.CanReadToken(Token))
            {
                var jwtToken = handler.ReadJwtToken(Token);
                if (jwtToken != null)
                {
                    data.Add(new KeyValuePair<string, string>("Issuer", jwtToken.Issuer));
                    data.Add(new KeyValuePair<string, string>("Valid From", jwtToken.ValidFrom.ToString()));
                    data.Add(new KeyValuePair<string, string>("Valid To", jwtToken.ValidTo.ToString()));
                    data.Add(new KeyValuePair<string, string>("Headers", ""));
                    foreach (var header in jwtToken.Header)
                    {
                        data.Add(new KeyValuePair<string, string>(header.Key, header.Value.ToString()));
                    }
                    data.Add(new KeyValuePair<string, string>("Claims", ""));
                    foreach (var claim in jwtToken.Claims)
                    {
                        data.Add(new KeyValuePair<string, string>(claim.Type, claim.Value));
                    }
                    data.Add(new KeyValuePair<string, string>("Payload", ""));
                    foreach (var payload in jwtToken.Payload)
                    {
                        data.Add(new KeyValuePair<string, string>(payload.Key, payload.Value.ToString()));
                    }
                }
            }
            return data;
        }

        public static CacheToken GetCacheToken(Endpoint endpoint, 
                                               AuthenticationResult result)
        {
            return new CacheToken
            {
                Endpoint = endpoint,
                TenantId = result.TenantId,
                UserInfo = result.UserInfo,
                Token = result.AccessToken,
                ExpiresOn = result.ExpiresOn,
                IdToken = result.IdToken,
                TokenType = result.AccessTokenType
            };
        }
    }
}
