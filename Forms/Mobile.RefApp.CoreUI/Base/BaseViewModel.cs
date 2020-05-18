using System;

using Mobile.RefApp.Lib.Logging;
using Mobile.RefApp.XamarinForms;

using Xamarin.Forms;

namespace Mobile.RefApp.CoreUI.Base
{
    public class BaseViewModel : ViewModelBase
    {
        protected new RefAppApplication CurrentApplication => Application.Current as RefAppApplication;

        public ILoggingService LoggingService { get; set; }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isBusy;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                    IsNotBusy = !_isBusy;
            }
        }

        private bool _isNotBusy = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get => _isNotBusy;
            set
            {
                if (SetProperty(ref _isNotBusy, value))
                    IsBusy = !_isNotBusy;
            }
        }

        private bool _canLoadMore = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can load more.
        /// </summary>
        /// <value><c>true</c> if this instance can load more; otherwise, <c>false</c>.</value>
        public bool CanLoadMore
        {
            get => _canLoadMore;
            set => SetProperty(ref _canLoadMore, value);
        }


        public BaseViewModel(ILoggingService loggingService)
        {
            LoggingService = loggingService;
            IsNotBusy = true;
            IsBusy = false;
        }
    }
}
