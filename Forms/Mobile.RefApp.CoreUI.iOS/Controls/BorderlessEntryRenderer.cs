using System.ComponentModel;

using Mobile.RefApp.CoreUI.Controls;
using Mobile.RefApp.CoreUI.iOS.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using UIKit;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace Mobile.RefApp.CoreUI.iOS.Controls
{
    public class BorderlessEntryRenderer 
        : EntryRenderer
    {
        protected override void OnElementPropertyChanged(
            object sender, 
            PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}
