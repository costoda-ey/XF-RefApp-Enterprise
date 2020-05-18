using Mobile.RefApp.CoreUI.Base;
using Mobile.RefApp.CoreUI.Models;
using Mobile.RefApp.CoreUI.ViewModels;
using Xamarin.Forms;

namespace Mobile.RefApp.CoreUI.Views
{
    public partial class RootView 
        : BaseContentPage
    {
        public RootView()
        {
            InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is NavigationMenuItem item)
                ((RootViewModel)ViewModel).NavigateItemSelectedCommand.Execute(item);
        }
    }
}
