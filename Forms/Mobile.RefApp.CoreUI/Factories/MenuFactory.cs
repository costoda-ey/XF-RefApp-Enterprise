using System.Collections.Generic;
using System.Linq;
using Mobile.RefApp.CoreUI.Constants;
using Mobile.RefApp.CoreUI.Models;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Mobile.RefApp.CoreUI.Factories
{
    public static class MenuFactory
    {
        /// <summary>
        /// Constant string used for unit testing all features
        /// </summary>
        public const string TestingPlatform = "Testing";

        public static string RuntimePlatform => Device.RuntimePlatform;

        private static List<NavigationMenuItem> _allMenuItems;

        public static IEnumerable<NavigationMenuItem> GetMenuItems()
        {
            return GetAllMenuItems().FilterByPlatform().Where(x => x.IsVisible);
        }

        public static NavigationMenuItem GetMenuItem(MenuPageType pageView)
        {
            return GetAllMenuItems().FilterByPlatform().SingleOrDefault(x => x.PageType == pageView);
        }

        private static IEnumerable<NavigationMenuItem> FilterByPlatform(this IEnumerable<NavigationMenuItem> menuItems)
        {
            switch (RuntimePlatform)
            {
                case TestingPlatform:
                    // used for testing and unit tests
                    // show all
                    break;
                case Device.Android:
                    // remove iOS screens 
                    menuItems.FirstOrDefault(x => x.PageType == MenuPageType.InTuneLogs).IsVisible = false;
                    menuItems.FirstOrDefault(x => x.PageType == MenuPageType.iOSKeychainGroup).IsVisible = false;
                    break;
                case Device.iOS:
                    //keep all
                    break;
                case Device.UWP:
                case Device.macOS:
                default:
                    // if not supported, we'll just hide everything
                    menuItems.ForEach(x => x.IsVisible = false);
                    break;
            }
            return menuItems;
        }

        private static IEnumerable<NavigationMenuItem> GetAllMenuItems()
        {
            return _allMenuItems ?? (_allMenuItems = new List<NavigationMenuItem>
            {
                new NavigationMenuItem
                {
                    PageType = MenuPageType.AzureTokenGen5000,
                    DisplayName = "Azure Token Generator 5000",
                    MenuOrder = 0,
                    IsVisible = true,
                    AutomationId = AutomationConstants.TokenGen5000,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.token-2000-1200.png",
                    Description = "This feature is used to test getting tokens from Azure AD using the Xamarin ADAL Library.  You can setup custom end point configuration and have tokens generated."
                },
                new NavigationMenuItem
                {
                    PageType = MenuPageType.AzureTokenCache,
                    DisplayName = "Azure Token Cache",
                    MenuOrder = 1,
                    IsVisible = true,
                    AutomationId = AutomationConstants.AzureTokenCache,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.tokencache-2000-1200.png",
                    Description = "This feature allows you to view and clear the Token Cache inside of the ADAL library and is useful for debugging."
                },
               new NavigationMenuItem
                {
                    PageType = MenuPageType.iOSKeychainGroup,
                    DisplayName = "Keychain Group Cache",
                    MenuOrder = 1,
                    IsVisible = true,
                    AutomationId = AutomationConstants.iOSKeychainGroup,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.keychain-2000-1200.png",
                    Description = "This feature is iOS specific and allows you to see all the information from Keychain Access Group."
                },
                new NavigationMenuItem
                {
                    PageType = MenuPageType.InTuneEnrollment,
                    DisplayName = "InTune Enrollment",
                    MenuOrder = 2,
                    IsVisible = true,
                    AutomationId = AutomationConstants.InTuneEnrollment,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.intune-enrollment-2000-1200.png",
                    Description = "This feature allows manual enrollment into InTune using the SDK various methods"
                },
                new NavigationMenuItem
                {
                    PageType = MenuPageType.InTuneDiagnostics,
                    DisplayName = "InTune Diagnostics",
                    MenuOrder = 3,
                    IsVisible = true,
                    AutomationId = AutomationConstants.InTuneDiagnostics,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.diagnostics-2000-1200.png",
                    Description = "This feature will display information about InTune Enrollment",
                },
                new NavigationMenuItem
                {
                    PageType = MenuPageType.InTuneLogs,
                    DisplayName = "InTune Logs",
                    MenuOrder = 4,
                    IsVisible = true,
                    AutomationId = AutomationConstants.InTuneLogs,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.logs-2000-1200.png",
                    Description = "This feature is iOS Specific and allows you to gather logging information about InTune",
                },

                new NavigationMenuItem
                {
                    PageType = MenuPageType.NetworkConnectivity,
                    DisplayName = "Network Connectivity",
                    MenuOrder = 5,
                    IsVisible = true,
                    AutomationId = AutomationConstants.NetworkMenuItem,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.networkconnection-2000-1200.png",
                    Description = "This feature will display information about your network connection",
                },
                new NavigationMenuItem
                {
                    PageType = MenuPageType.Ping,
                    DisplayName = "Network Tools",
                    MenuOrder = 6,
                    IsVisible = true,
                    AutomationId = AutomationConstants.PingMenuItem,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.networktools-2000-1200.png",
                    Description = "This feature will show you how to use Ping and DNS lookup to test conductivity to network resources" 
                },
                new NavigationMenuItem
                {
                    PageType = MenuPageType.SharePointExplorer,
                    DisplayName = "SharePoint Explorer",
                    MenuOrder = 7,
                    IsVisible = true,
                    AutomationId = AutomationConstants.SharePointMenuItem,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.sharepointexplorer-2000-1200.png",
                    Description = "This feature allows you to look at SharePoint List information"
                },
                new NavigationMenuItem
                {
                    PageType = MenuPageType.SharePointProject,
                    DisplayName = "SharePoint Demo",
                    MenuOrder = 8,
                    IsVisible = true,
                    AutomationId = AutomationConstants.SharePointMenuItem,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.sharepoint-2000-1000.png",
                    Description = "This feature is not complete"
                },
               new NavigationMenuItem
                {
                    PageType = MenuPageType.Camera,
                    DisplayName = "Camera - CrossMedia",
                    MenuOrder = 9,
                    IsVisible = true,
                    AutomationId = AutomationConstants.CameraMenuItem,
                    ImageName = "Mobile.RefApp.CoreUI.Assets.Images.camera-2000-1200.png",
                    Description = "This feature allows you to use the Camera to take and manage photos along with protect them with Microsoft InTune"
                }
            }
             .OrderBy(x => x.MenuOrder)
             .ToList());
        }
    }
}
