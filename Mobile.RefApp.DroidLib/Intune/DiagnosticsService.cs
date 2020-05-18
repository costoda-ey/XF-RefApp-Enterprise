using Mobile.RefApp.Lib.Intune;

namespace Mobile.RefApp.DroidLib.Intune
{
    public class DiagnosticsService 
        : IDiagnosticService
    {
        public void DisplayDiagnostics(bool isDarkMode = true)
        {
            throw new System.Exception("Error - not available in Android");
        }
    }
}
