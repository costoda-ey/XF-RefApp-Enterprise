using Microsoft.Intune.MAM;

using Mobile.RefApp.Lib.Intune;

namespace Mobile.RefApp.iOSLib.Intune
{
    public class DiagnosticsService 
        : IDiagnosticService
    {
        public void DisplayDiagnostics(bool isDarkMode = true)
        {
            IntuneMAMDiagnosticConsole.DisplayDiagnosticConsole(isDarkMode);
        }
    }
}
