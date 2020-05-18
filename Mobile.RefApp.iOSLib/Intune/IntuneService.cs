using System;
using Mobile.RefApp.Lib.Intune;

namespace Mobile.RefApp.iOSLib.Intune
{
    public partial class IntuneService
        : IIntuneService
    {
        public string SDKVersion
            => Microsoft.Intune.MAM.IntuneMAMVersionInfo.SdkVersion;
    }
}
