
namespace Mobile.RefApp.Lib.Logging
{
    public class DiagnosticService
        : IDiagnosticService
    {
        public void SimulateCrash()
        {
            Microsoft.AppCenter.Crashes.Crashes.GenerateTestCrash();
        }
    }
}
