using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.RefApp.XamarinForms
{
    public abstract class ViewModelBase 
        : NotifyPropertyChanged
    {
        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }

        public virtual async Task Initialize(Dictionary<string, object> navigationsParams = null) => await Task.CompletedTask;
        public virtual async Task PoppingTo(Dictionary<string, object> navigationsParams = null) => await Task.CompletedTask;

        private WeakReference<ContentPageBase> _page;
        public ContentPageBase Page => _page.TryGetTarget(out ContentPageBase target) ? target : null;
        public void SetWeakPage(ContentPageBase page) => _page = new WeakReference<ContentPageBase>(page);

        private WeakReference<MasterDetailPageBase> _masterDetailPage;
        public MasterDetailPageBase MasterDetailPage => _masterDetailPage.TryGetTarget(out MasterDetailPageBase target) ? target : null;
        public void SetWeakMasterDetailpage(MasterDetailPageBase page) => _masterDetailPage = new WeakReference<MasterDetailPageBase>(page);

        protected ApplicationBase CurrentApplication => Application.Current as ApplicationBase;
        private INavigation Navigation => Page?.Navigation ?? Application.Current.MainPage.Navigation;

        protected Messaging Messaging => Messaging.Instance;

        public Task DisplayAlert(string title, string message, string cancel)
            => Page?.DisplayAlert(title, message, cancel);

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
            => Page?.DisplayAlert(title, message, accept, cancel);

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
            => Page?.DisplayActionSheet(title, cancel, destruction, buttons);

        public IReadOnlyList<Page> ModalStack => Navigation.ModalStack;
        public IReadOnlyList<Page> NavigationStack => Navigation.NavigationStack;

        public Task<Page> PopAsync(Dictionary<string, object> navigationParams = null, bool animated = true) => PopAsyncInternal(navigationParams, animated);
        public Task<Page> PopModalAsync(bool animated = true) => Navigation.PopModalAsync(animated);
        public Task PopToRootAsync(bool animated = true) => Navigation.PopToRootAsync(animated);
        public void RemovePage(Page page = null) => Navigation.RemovePage(page ?? Page);

        public async Task InsertPageBefore<TPage, TViewModel>(Dictionary<string, object> navigationParams = null)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
        {
            Page page = await CurrentApplication.CreatePage<TPage, TViewModel>(navigationParams);

            Navigation.InsertPageBefore(page, Page);
        }

        public Task ReplaceMainPageAsync<TPage, TViewModel>(Dictionary<string, object> navigationParams)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
            => ReplaceMainPageAsync<TPage, TViewModel>(true, navigationParams);

        public async Task ReplaceMainPageAsync<TPage, TViewModel>(bool wrapInNavigationPage = true, Dictionary<string, object> navigationParams = null)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
        {
            Page page = await CurrentApplication.CreatePage<TPage, TViewModel>(navigationParams);

            CurrentApplication.MainPage = wrapInNavigationPage ? new NavigationPage(page) : page;
        }

        public Task ReplaceDetailPageAsync<TPage, TViewModel>(Dictionary<string, object> navigationParams)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
            => ReplaceDetailPageAsync<TPage, TViewModel>(true, navigationParams);

        public async Task ReplaceDetailPageAsync<TPage, TViewModel>(bool wrapInNavigationPage = true, Dictionary<string, object> navigationParams = null)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
        {
            Page page = await CurrentApplication.CreatePage<TPage, TViewModel>(navigationParams);

            MasterDetailPage.Detail = wrapInNavigationPage ? new NavigationPage(page) : page;

            await Task.CompletedTask;
        }

        public async Task PopToPageAsync<TPage>(bool animated = true)
            where TPage : ContentPageBase
        {
            var navList = NavigationStack.ToList();
            Page headersPage = navList.FirstOrDefault(p => p.GetType() == typeof(TPage));
            int idx = navList.IndexOf(headersPage);
            if (idx != -1)
            {
                for (int i = idx + 1; i < navList.Count - 1; i++)
                {
                    RemovePage(navList[i]);
                }
                await PopAsync(null, animated);
            }
        }

        public Task PushMasterDetailAsync<TPage>(Dictionary<string, object> navigationParams = null, bool animated = true)
            where TPage : MasterDetailPageBase
            => PushMasterDetailAsyncInternal<TPage>(navigationParams, animated, false);

        public Task PushMasterDetailModalAsync<TPage>(Dictionary<string, object> navigationParams = null, bool animated = true)
            where TPage : MasterDetailPageBase
            => PushMasterDetailAsyncInternal<TPage>(navigationParams, animated, true);

        public Task PushAsync<TPage, TViewModel>(bool animated = true)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
            => PushAsyncInternal<TPage, TViewModel>(null, animated, false);

        public Task PushAsync<TPage, TViewModel>(Dictionary<string, object> navigationParams, bool animated = true)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
            => PushAsyncInternal<TPage, TViewModel>(navigationParams, animated, false);

        public Task PushModalAsync<TPage, TViewModel>(bool animated = true)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
            => PushAsyncInternal<TPage, TViewModel>(null, animated, true);

        public Task PushModalAsync<TPage, TViewModel>(Dictionary<string, object> navigationParams, bool animated = true)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
            => PushAsyncInternal<TPage, TViewModel>(navigationParams, animated, true);

        private async Task PushMasterDetailAsyncInternal<TPage>(Dictionary<string, object> navigationParams = null, bool animated = true, bool modal = false)
            where TPage : MasterDetailPageBase
        {
            Page page = await CurrentApplication.CreateMasterDetailPage<TPage>(navigationParams);

            if (modal)
            {
                await Navigation.PushModalAsync(page, animated);
            }
            else
            {
                await Navigation.PushAsync(page, animated);
            }
        }

        private async Task PushAsyncInternal<TPage, TViewModel>(Dictionary<string, object> navigationParams = null, bool animated = true, bool modal = false)
            where TPage : ContentPageBase
            where TViewModel : ViewModelBase
        {
            Page page = await CurrentApplication.CreatePage<TPage, TViewModel>(navigationParams);

            if (modal)
            {
                await Navigation.PushModalAsync(page, animated);
            }
            else
            {
                await Navigation.PushAsync(page, animated);
            }
        }

        private async Task<Page> PopAsyncInternal(Dictionary<string, object> navigationParams = null, bool animated = true)
        {
            if (NavigationStack.Count > 1)
            {
                var vm = NavigationStack[NavigationStack.Count - 2].BindingContext as ViewModelBase;
                vm?.PoppingTo(navigationParams);
            }

            Page popped = await Navigation.PopAsync(animated);

            return popped;
        }
    }
}
