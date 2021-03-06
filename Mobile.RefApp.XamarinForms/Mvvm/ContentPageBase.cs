﻿using Xamarin.Forms;

namespace Mobile.RefApp.XamarinForms
{
    public abstract class ContentPageBase 
        : ContentPage
    {
        protected ViewModelBase ViewModel => BindingContext as ViewModelBase;

        protected override void OnAppearing() => ViewModel?.OnAppearing();

        protected override void OnDisappearing() => ViewModel?.OnDisappearing();

        public virtual void Initialize() { }
    }
}
