using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Mobile.RefApp.CoreUI.Base;
using Mobile.RefApp.CoreUI.Interfaces;
using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Keychain;
using Mobile.RefApp.Lib.Logging;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Mobile.RefApp.CoreUI.ViewModels
{
    public class KeychainGroupListViewModel
        : BaseViewModel
    {
        private readonly IEndpointService _endpointService;
        private readonly IKeychainService _keychainService;

        private Endpoint _endpoint;

        private string _queryText;
        public string QueryText
        {
            get => _queryText;
            set => SetProperty(ref _queryText, value);
        }

        public ObservableCollection<SecRecord> KeychainKeys { get; }

        public ICommand SearchCommand { get; private set; }

        public KeychainGroupListViewModel(
            ILoggingService loggingService,
            IKeychainService keychainService,
            IEndpointService endpointService)
            : base(loggingService)
        {
            _keychainService = keychainService;
            _endpointService = endpointService;

            Title = "Keychain Group Cache";
            KeychainKeys = new ObservableCollection<SecRecord>();
            SearchCommand = new Command(DoSearch);
            
        }

        public override async Task Initialize(Dictionary<string, object> navigationsParams = null)
        {
            try
            {
                var endpoints = await _endpointService.GetEndpointsByPlatform(App.CurrentPlatform);
                if (endpoints.Any())
                {
                    _endpoint = endpoints[0];
                    DoSearch($"{_endpoint.iOSTeamId}.{_endpoint.iOSKeychainSecurityGroup}");
                } 
            }
            catch (System.Exception ex)
            {
                LoggingService.LogError(typeof(KeychainGroupListViewModel), ex, ex.Message);
            }
        }

        private void DoSearch(object queryText = null)
        {
            try
            {
                string query = (queryText == null) ? QueryText : (string)queryText;
                IsBusy = true;
                if (!string.IsNullOrEmpty(query))
                {
                    KeychainKeys.Clear();
                    var results = _keychainService.GetRecordsFromKeychain(query);
                    if (results.Any())
                    {
                        foreach (var r in results)
                        {
                            KeychainKeys.Add(r);
                            System.Console.WriteLine($"{r.AccessGroup} {r.Account} {r.Service} {r.CreationDate}");
                        }
                    }
                }
                IsBusy = false;

                this.Page.BindingContext = this;
            }
            catch (System.Exception ex)
            {
                LoggingService.LogError(typeof(KeychainGroupListViewModel), ex, ex.Message);
            }
        }
    }
}
