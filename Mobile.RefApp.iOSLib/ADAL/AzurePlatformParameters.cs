using Mobile.RefApp.Lib.ADAL;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using UIKit;

namespace Mobile.RefApp.iOSLib.ADAL
{
	public class AzurePlatformParameters
			: IAzurePlatformParameters
	{
        private UIViewController _viewController;
        public IPlatformParameters GetPlatformParameters(bool useBroker)
        {

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(GetViewController);
            return new PlatformParameters(_viewController, useBroker);
        }

        private void GetViewController()
        {
            _viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        }
    }
}