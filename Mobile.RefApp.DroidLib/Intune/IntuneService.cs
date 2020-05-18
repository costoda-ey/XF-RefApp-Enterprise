using Mobile.RefApp.Lib.Intune;
using Microsoft.Intune.Mam.Client;

namespace Mobile.RefApp.DroidLib.Intune
{
    public class IntuneService
        : IIntuneService
    {
        public string SDKVersion
                 => $"{MAMSDKVersion.VerMajor}.{MAMSDKVersion.VerMinor}.{MAMSDKVersion.VerPatch}";
    }
}
