
namespace Mobile.RefApp.Lib.ADAL
{
    public class Endpoint
        : Network.Endpoint
    {
        public bool UseBroker { get; set; }
        public Core.Platform Platform { get; set; }

        //
        //Required by ADAL
        //
        public bool EnableLogging { get; set; }
        public string iOSTeamId { get; set; }
        public string iOSKeychainSecurityGroup { get; set; }
        public string ApplicationId { get; set; }
        public string Authority { get; set; }
        public string RedirectUri { get; set; }
        public string ResourceBaseUri { get; set; }
        public string ResourceId  { get; set; }   
        public string ExtraParameters { get; set; }
    }
}
