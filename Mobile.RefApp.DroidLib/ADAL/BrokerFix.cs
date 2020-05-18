using System;
using Android.Accounts;
using Android.Content;
using Plugin.CurrentActivity;

namespace Mobile.RefApp.DroidLib.ADAL
{
    public static class BrokerFix
    {
        public static void AccountPicker()
        {
            string WORK_AND_SCHOOL_TYPE = "com.microsoft.workaccount";
            // See the Android docs for customizing the UI https://developers.google.com/android/reference/com/google/android/gms/common/AccountPicker
            Intent intent = AccountManager.NewChooseAccountIntent(null, null, new[] { WORK_AND_SCHOOL_TYPE }, null, null, null, null);
            // Start an activity with this intent, e.g. 
            CrossCurrentActivity.Current.Activity.StartActivity(intent);
        }
    }
}
