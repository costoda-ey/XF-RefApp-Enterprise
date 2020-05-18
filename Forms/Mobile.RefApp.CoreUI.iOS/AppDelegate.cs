using Foundation;
using UIKit;

using Unity;

using Xamarin.Forms;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Mobile.RefApp.Lib.Logging;
using Mobile.RefApp.iOSLib.ADAL;
using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Intune;
using Mobile.RefApp.iOSLib.Intune;
using Mobile.RefApp.Lib.Network;
using Mobile.RefApp.iOSLib.Network;
using Mobile.RefApp.Lib.Intune.Enrollment;
using Mobile.RefApp.iOSLib.Intune.Enrollment;
using Mobile.RefApp.Lib.Intune.Policies;
using Mobile.RefApp.iOSLib.Intune.Policies;
using Mobile.RefApp.Lib.Keychain;
using Mobile.RefApp.iOSLib.Keychain;
using Mobile.RefApp.Lib.Intune.Logging;

using Serilog;

namespace Mobile.RefApp.CoreUI.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate 
         : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(
            UIApplication uiApplication, 
            NSDictionary launchOptions)
        {
            //Xamarin.Calabash.Start();
            global::Xamarin.Forms.Forms.Init();
            FormsMaterial.Init();

            SetupLogging();

            //initialize the app
            var refApp = new App();
            refApp.Init(PlatformInitializeContainer);
            LoadApplication(refApp);

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        public override bool OpenUrl(
            UIApplication application, 
            NSUrl url, 
            string sourceApplication, 
            NSObject annotation)
        {
            if (AuthenticationContinuationHelper.IsBrokerResponse(sourceApplication))
                AuthenticationContinuationHelper.SetBrokerContinuationEventArgs(url);

            return true;
        }

        [Export("applicationWillTerminate:")]
        public override void WillTerminate(UIApplication uiApplication)
        {
            Log.CloseAndFlush();
        }

        private void SetupLogging()
        {
            var log = new LoggerConfiguration()
                .WriteTo
                .NSLog()
                .CreateLogger();
        }

        public void PlatformInitializeContainer(UnityContainer container)
        {
            container.RegisterType<ILoggingService, LoggingService>();
            container.RegisterType<IPlatformHttpClientHandler, IosHttpClientHandler>();
            container.RegisterType<IAzurePlatformParameters, AzurePlatformParameters>();
            container.RegisterType<IIntuneService, IntuneService>();
            container.RegisterType<INetworkInterfaceInfo, NetworkInterfaceInfo>();
            container.RegisterType<IAzureAuthenticatorEndpointService, AzureAuthenticatorService>();

            container.RegisterType<IKeychainService, KeychainService>();
            container.RegisterType<IEnrollmentService, EnrollmentService>();
            container.RegisterType<IPolicyService, PolicyService>();
            container.RegisterType<Lib.Intune.IDiagnosticService, iOSLib.Intune.DiagnosticsService>();
        }
    }
}
