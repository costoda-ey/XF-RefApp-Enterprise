using System.Threading.Tasks;

using Mobile.RefApp.CoreUI.Base;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Unity;
using Mobile.RefApp.CoreUI.ViewModels;
using Mobile.RefApp.CoreUI.Views;
using Mobile.RefApp.CoreUI.Interfaces;
using Mobile.RefApp.CoreUI.Services;
using Mobile.RefApp.Lib.Core;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Mobile.RefApp.CoreUI
{
    public partial class App 
        : RefAppApplication
    {
        public static Platform CurrentPlatform
        {
            get
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        return Lib.Core.Platform.IOS;
                    case Xamarin.Forms.Device.Android:
                        return Lib.Core.Platform.Android;
                    case Xamarin.Forms.Device.UWP:
                        return Lib.Core.Platform.UWP;
                    case Xamarin.Forms.Device.macOS:
                        return Lib.Core.Platform.Mac;
                    case Xamarin.Forms.Device.GTK:
                        return Lib.Core.Platform.Linux;
                    default:
                        return Lib.Core.Platform.IOS;
                }
            }
        }

        public App()
        {
            InitializeComponent();
            SetPlatform();
        }

        protected override bool UseRootNavigationPage => true;

        protected override Task<Page> CreateMainPage() => CreatePage<RootView, RootViewModel>();


        protected override void InitializeContainer()
        {
#if DEBUG
            Container.EnableDebugDiagnostic();
#endif
            Container.RegisterType<IEndpointService, EndpointService>();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        private void SetPlatform()
        {

        }
    }
}