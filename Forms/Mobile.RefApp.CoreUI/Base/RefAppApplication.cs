/*
 * ApplicationBase - base application abstract class 
 * Idea and flow by Jacob Maristany:  https://twitter.com/jacobmaristany 
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Mobile.RefApp.Lib.Logging;
using Mobile.RefApp.XamarinForms;

using Unity;

using Xamarin.Forms;

namespace Mobile.RefApp.CoreUI.Base
{
    public abstract class RefAppApplication : ApplicationBase
    {
        protected virtual bool UseRootNavigationPage { get; }

        public UserInfo LoggedInUserInfo { get; internal set; }

        public new void Init(Action<UnityContainer> platformInitializeContainer = null)
        {
            platformInitializeContainer?.Invoke(Container);
            InitializeContainer();

            Page mainPage = null;
            Task.Run(async () => mainPage = await CreateMainPage()).Wait();

            MainPage = UseRootNavigationPage ? new NavigationPage(mainPage) : mainPage;
        }

        public async Task<Page> CreateTab<T1, T2>(
           Dictionary<string, object> navigationParams = null,
           bool isNavigable = false)
           where T1 : ContentPageBase
           where T2 : ViewModelBase
        {
            Page page = null;
            try
            {
                page = Container.Resolve<T1>();
                var vm = Container.Resolve<T2>();
                await vm.Initialize(navigationParams);

                vm.SetWeakPage((ContentPageBase)page);
                page.BindingContext = vm;
                ((ContentPageBase)page).Initialize();

                if (isNavigable)
                {
                    return new NavigationPage(page)
                    {
                        Title = page.Title,
                        IconImageSource = page.IconImageSource,
                        Style = (Style)Application.Current.Resources["NavigationPageStyle"]
                    };
                }
                else
                    return page;
            }
            catch (Exception ex)
            {
                var loggingService = Container.Resolve<ILoggingService>();
                loggingService.LogError(typeof(ApplicationBase), ex, ex.Message);
            }
            return page;
        }

        /// <summary>
        /// CreateTabForMasterDetail - return a Master Detail Page object when passed in which Master View/ViewModel and Detail View/ViewModel.  Used to embed a Master Detail into a tab.
        /// </summary>
        /// <typeparam name="MV">Master View</typeparam>
        /// <typeparam name="MVM">Master ViewModel</typeparam>
        /// <typeparam name="DV">Detail View</typeparam>
        /// <typeparam name="DVM">Detail ViewModel</typeparam>
        /// <param name="masterNavigationParams">navigation parameters for Master</param>
        /// <param name="detailNavigationParams">navigation parameters for Detail</param>
        /// <returns>Master Detail Page with properties set for tab</returns>
        public async Task<Page> CreateTabForMasterDetail<MV, MVM, DV, DVM>(
            Dictionary<string, object> masterNavigationParams = null,
            Dictionary<string, object> detailNavigationParams = null)
            where MV : ContentPageBase
            where DV : ContentPageBase
            where MVM : ViewModelBase
            where DVM : ViewModelBase
        {
            MasterDetailPage mdPage = null;
            try
            {
                var masterPage = await CreatePage<MV, MVM>(masterNavigationParams);
                var detailPage = await CreatePage<DV, DVM>(detailNavigationParams);
                var navDetailpage = new NavigationPage(detailPage)
                {
                    Title = detailPage.Title,
                    Style = (Style)Application.Current.Resources["NavigationPageStyle"]
                };

                mdPage = new MasterDetailPage
                {
                    Master = masterPage,
                    Detail = navDetailpage,
                    Title = masterPage.Title,
                    IconImageSource = masterPage.IconImageSource
                };
            }
            catch (Exception ex)
            {
                var loggingService = Container.Resolve<ILoggingService>();
                loggingService.LogError(typeof(ApplicationBase), ex, ex.Message);
            }
            return mdPage;
        }

    }
}
