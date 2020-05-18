using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Mobile.RefApp.XamarinForms
{
    public class MasterDetailPageBase 
        : MasterDetailPage
    {
        protected ApplicationBase CurrentApplication => Application.Current as ApplicationBase;

        public virtual async Task Initialize(Dictionary<string, object> navigationParams = null) => await Task.CompletedTask;
        public virtual async Task<Page> CreateMasterPage() => await Task.FromResult<Page>(new ContentPage());
        public virtual async Task<Page> CreateDetailPage() => await Task.FromResult<Page>(new ContentPage());
    }
}
