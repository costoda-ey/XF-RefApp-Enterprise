using System;
namespace Mobile.RefApp.CoreUI.Constants
{
    public static class AutomationConstants
    {
        //Pages
        public static string PictureMasterPageView => nameof(PictureMasterPageView);
        public static string MenuPageView => nameof(MenuPageView);

        //Menu Items
        public static string CameraMenuItem = nameof(CameraMenuItem);
        public static string InTuneSignOnMenuItem = nameof(InTuneSignOnMenuItem);
        public static string SQLiteMenuItem = nameof(SQLiteMenuItem);
        public static string ScannerMenuItem = nameof(ScannerMenuItem);
        public static string AudioRecordMenuItem = nameof(AudioRecordMenuItem);
        public static string FontMenuItem = nameof(FontMenuItem);
        public static string FileMenuItem = nameof(FileMenuItem);
        public static string NetworkMenuItem = nameof(NetworkMenuItem);
        public static string GeolocationMenuItem = nameof(GeolocationMenuItem);
        public static string BeaconMenuItem = nameof(BeaconMenuItem);
        public static string SharePointMenuItem = nameof(SharePointMenuItem);
        public static string DeepLinkMenuItem = nameof(DeepLinkMenuItem);
        public static string LinkedInMenuItem = nameof(LinkedInMenuItem);
        public static string BiometricsAuthMenuItem = nameof(BiometricsAuthMenuItem);
        public static string ColorThemeMenuItem = nameof(ColorThemeMenuItem);
        public static string PingMenuItem = nameof(PingMenuItem);
        public static string StickyHeaderMenuItem = nameof(StickyHeaderMenuItem);
        public static string TokenGen5000 = nameof(TokenGen5000);
        public static string AzureTokenCache = nameof(AzureTokenCache);
        public static string iOSKeychainGroup = nameof(iOSKeychainGroup);
        public static string InTuneDiagnostics = nameof(InTuneDiagnostics);
        public static string InTuneLogs = nameof(InTuneLogs);
        public static string InTuneEnrollment = nameof(InTuneEnrollment);
        //Buttons
        public static string CameraButton = nameof(CameraButton);
        public static string PicturesListView = nameof(PicturesListView);

        //HACK: limitation on Android Xamarin.UITest for toolbaritems, must match id and string name
        //https://bugzilla.xamarin.com/show_bug.cgi?id=32718
        public static string MenuButton => "Menu";
    }
}
