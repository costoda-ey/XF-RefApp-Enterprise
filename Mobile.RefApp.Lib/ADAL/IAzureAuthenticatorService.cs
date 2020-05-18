
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Mobile.RefApp.Lib.ADAL
{
    public interface IAzureAuthenticatorService
    {
        Task<CacheToken> Authenticate(string applicationId,
                                                string authority,
                                                string returnUri,
                                                string resourceId,
                                                bool useBroker = true,
                                                [CallerMemberName] string memberName = "",
                                                [CallerLineNumber] int lineNumber = 0);
    }
}
