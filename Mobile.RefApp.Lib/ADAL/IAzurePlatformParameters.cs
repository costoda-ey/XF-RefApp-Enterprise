using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Mobile.RefApp.Lib.ADAL
{
    public interface IAzurePlatformParameters
    {
        IPlatformParameters GetPlatformParameters(bool useBroker);
    }
}
