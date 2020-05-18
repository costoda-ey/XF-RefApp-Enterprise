using System;
using Mobile.RefApp.XamarinForms;

namespace Mobile.RefApp.CoreUI.Base
{
    public abstract class BaseContentPage 
        : ContentPageBase
    {
        protected new BaseViewModel ViewModel => BindingContext as BaseViewModel;
    }
}
