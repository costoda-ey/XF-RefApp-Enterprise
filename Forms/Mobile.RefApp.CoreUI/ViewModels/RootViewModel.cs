using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Mobile.RefApp.CoreUI.Base;
using Mobile.RefApp.CoreUI.Factories;
using Mobile.RefApp.CoreUI.Models;
using Mobile.RefApp.CoreUI.Views;
using Mobile.RefApp.Lib.Intune;
using Mobile.RefApp.Lib.Logging;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mobile.RefApp.CoreUI.ViewModels
{
    public class RootViewModel
        : BaseViewModel
    {
        private readonly Mobile.RefApp.Lib.Intune.IDiagnosticService _diagnosticService;
        public string _version;
        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        private string _deviceInformation;
        public string DeviceInformation
        {
            get => _deviceInformation;
            set => SetProperty(ref _deviceInformation, value);
        }

        public ICommand NavigateItemSelectedCommand { get; private set; }

        public ObservableCollection<NavigationMenuItem> MenuItems { get; private set; }

        public RootViewModel(
            ILoggingService loggingService,
            Mobile.RefApp.Lib.Intune.IDiagnosticService diagnosticService) 
            : base (loggingService)
        {
            Title = "Reference App";

            _diagnosticService = diagnosticService;

            MenuItems = new ObservableCollection<NavigationMenuItem>(MenuFactory.GetMenuItems());
        }
        public override void OnAppearing()
        {
            Version = $"Version: {AppInfo.VersionString}, Build: {AppInfo.BuildString}";

            DeviceInformation = $"OS Ver: {DeviceInfo.VersionString}, Model: {DeviceInfo.Model}, Manufacturer: {DeviceInfo.Manufacturer}, Name: {DeviceInfo.Name}";

            NavigateItemSelectedCommand = new Command(async (obj) => await NavigateItemSelected(obj));
        }

        public async Task NavigateItemSelected(object selectedItem)
        {
            if (selectedItem is NavigationMenuItem item)
            {
                switch (item.PageType)
                {
                    case MenuPageType.AzureTokenGen5000:
                        await PushAsync<AzureTokenGeneratorView, AzureTokenGeneratorViewModel>();
                        break;
                    case MenuPageType.iOSKeychainGroup:
                        await PushAsync<KeychainGroupListView, KeychainGroupListViewModel>();
                        break;
                    case MenuPageType.InTuneEnrollment:
                        await PushAsync<InTuneEnrollmentView, InTuneEnrollmentViewModel>();
                        break;
                    case MenuPageType.InTuneDiagnostics:
                        _diagnosticService.DisplayDiagnostics(true);
                        break;
                    case MenuPageType.InTuneLogs:
                        await PushAsync<InTuneLogsViewerView, InTuneLogsViewerViewModel>();
                        break;
                    case MenuPageType.NetworkConnectivity:
                        break;
                    case MenuPageType.Ping:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
