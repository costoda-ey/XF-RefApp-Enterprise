using System;

using Android.App;

using Microsoft.Intune.Mam.Client.App;

using Plugin.CurrentActivity;

using Serilog;

namespace Mobile.RefApp.CoreUI.Droid
{
    /// <summary>
    /// The Taskr application.
    /// </summary>
#if DEBUG
    /// <remarks>
    /// Due to an issue with debugging the Xamarin bound MAM SDK the Debuggable = false attribute must be added to the Application in order to enable debugging.
    /// Without this attribute the application will crash when launched in Debug mode. Additional investigation is being performed to identify the root cause.
    /// </remarks>
    [Application(Debuggable = false)]
#else
    [Application]
#endif
    public class MainApplication 
        : MAMApplication
    {
        public MainApplication(IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
     : base(handle, transfer) { }

        public override void OnMAMCreate()
        {
            CrossCurrentActivity.Current.Init(this);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.AndroidLog()
                .Enrich.WithProperty("ErrorLog", "ErrorTag")
                .CreateLogger();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            // Delete all tasks and close the database connection.
        }
    }
}
