using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mobile.RefApp.CoreUI.Base;
using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Logging;

namespace Mobile.RefApp.CoreUI.ViewModels
{
    public class AzureTokenDetailViewerViewModel
        : BaseViewModel
    {
        private CacheToken _cacheToken;
        public CacheToken CacheToken
        {
            get => _cacheToken;
            set => SetProperty(ref _cacheToken, value);
        }

        public ObservableCollection<KeyValuePair<string, string>> TokenDetails { get; private set; }

        public AzureTokenDetailViewerViewModel(ILoggingService loggingService) : base (loggingService)
        {
            Title = "Detail Token Viewer";
        }

        public override async Task Initialize(Dictionary<string, object> navigationsParams = null)
        {
            await Task.Run(() =>
            {
                if (navigationsParams != null
                && navigationsParams.ContainsKey("token"))
                {
                    CacheToken = navigationsParams["token"] as CacheToken;
                    TokenDetails = new ObservableCollection<KeyValuePair<string, string>>(CacheToken.GetTokenDetails());
                }
            }).ConfigureAwait(false);
        }
    }
}
