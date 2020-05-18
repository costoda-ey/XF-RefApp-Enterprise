using Xamarin.Forms;

namespace Mobile.RefApp.CoreUI.Models
{
    public enum MenuPageType
    {
        None,
        Camera,
        Canvas,
        SQLite,
        Scanner,
        Microphone,
        RadListView,
        FontViewer,
        FileViewer,
        NetworkConnectivity,
        Geolocation,
        Beacon,
        SharePointExplorer,
        SharePointProject,
        DeepLink,
        LinkedIn,
        BiometricsAuth,
        ColorTheme,
        Ping,
        StickyHeader,
        Scanbot,
        InTuneDiagnostics,
        InTuneEnrollment,
        InTuneLogs,
        AzureTokenGen5000,
        AzureTokenCache,
        iOSKeychainGroup,
        WebView
    }

    public class NavigationMenuItem
    {
        public MenuPageType PageType { get; set; }
        public string DisplayName { get; set; }
        public int MenuOrder { get; set; }
        public bool IsVisible { get; set; }
        public string AutomationId { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public ImageSource LogoSource => ImageSource.FromResource(ImageName);
    }
}
