using Mobile.RefApp.Lib.Intune.Policies;
using Mobile.RefApp.Lib.Logging;

using Microsoft.Intune.MAM;

using Foundation;
using UIKit;

namespace Mobile.RefApp.iOSLib.Intune.Policies
{
    public class PolicyService 
        : IntuneMAMPolicyDelegate, 
            IPolicyService
    {
        private readonly ILoggingService _loggingService;

        public string IdentityForCurrentActivity => IntuneMAMPolicyManager.Instance.IdentityForCurrentActivity;

        public string[] IntuneLogPaths => IntuneMAMPolicyManager.Instance.IntuneLogPaths;

        public Source PolicySource => GetPolicySource(IntuneMAMPolicyManager.Instance.MamPolicySource);

        public bool TelemetryEnabled => IntuneMAMPolicyManager.Instance.TelemetryEnabled;

        public string PrimaryUser => IntuneMAMPolicyManager.Instance.PrimaryUser;

        public string ProcessIdentity => IntuneMAMPolicyManager.Instance.ProcessIdentity;

        public string UIPolicyIdentity => IntuneMAMPolicyManager.Instance.UIPolicyIdentity;

        #region policies

        public bool IsContactSyncAllowed => IntuneMAMPolicyManager.Instance.Policy.IsContactSyncAllowed;

        public bool IsManagedBrowserRequired => IntuneMAMPolicyManager.Instance.Policy.IsManagedBrowserRequired;

        public bool IsPINRequired => IntuneMAMPolicyManager.Instance.Policy.IsPINRequired;

        public bool ShouldFileProviderEncryptFiles => IntuneMAMPolicyManager.Instance.Policy.ShouldFileProviderEncryptFiles;

        public bool IsAppSharingAllowed => IntuneMAMPolicyManager.Instance.Policy.IsAppSharingAllowed;

        public bool SpotlightIndexingAllowed => IntuneMAMPolicyManager.Instance.Policy.IsSpotlightIndexingAllowed;

     
        #endregion

        public PolicyService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
            IntuneMAMPolicyManager.Instance.Delegate = this;
        }

        public string[] GetAadAuthorityUriForIdentity(string identity)
        {
            return IntuneMAMPolicyManager.Instance.AadAuthorityUrisForIdentity(identity);
        }

        public bool IsIdentityManaged(string identity)
        {
            return IntuneMAMPolicyManager.Instance.IsIdentityManaged(identity);
        }

        public bool IsDocumentPickerAllowed(DocumentPickerMode documentPickerMode)
        {
            return IntuneMAMPolicyManager.Instance.Policy.IsDocumentPickerAllowed(GetDocumentPickerMode(documentPickerMode));
        }

        public bool IsSavedToAllowedForLocation(SaveLocation location, string accountName)
        {
            return IntuneMAMPolicyManager.Instance.Policy.IsSaveToAllowedForLocation(GetSavedLocation(location), accountName);
        }

        public bool IsUniversalLinkAllowed(string url)
        {
            return IntuneMAMPolicyManager.Instance.Policy.IsUniversalLinkAllowed(new NSUrl(url));
        }

        public bool IsUrlAllowed(string url)
        {
            return IntuneMAMPolicyManager.Instance.Policy.IsURLAllowed(new NSUrl(url));
        }

        private IntuneMAMSaveLocation GetSavedLocation(SaveLocation location)
        {
            switch (location)
            {
                case SaveLocation.LocalDrive:
                    return IntuneMAMSaveLocation.LocalDrive;
                case SaveLocation.OneDriveForBusiness:
                    return IntuneMAMSaveLocation.OneDriveForBusiness;
                case SaveLocation.SharePoint:
                    return IntuneMAMSaveLocation.SharePoint;
                case SaveLocation.LocationOther:
                default:
                    return IntuneMAMSaveLocation.Other;
            }
        }

        private UIDocumentPickerMode GetDocumentPickerMode(DocumentPickerMode documentPickerMode)
        {
            switch (documentPickerMode)
            {
                case DocumentPickerMode.ExportToService:
                    return UIDocumentPickerMode.ExportToService;
                case DocumentPickerMode.Import:
                    return UIDocumentPickerMode.Import;
                case DocumentPickerMode.MoveToService:
                    return UIDocumentPickerMode.MoveToService;
                case DocumentPickerMode.Open:
                default:
                    return UIDocumentPickerMode.Open;
            }
        }

        private Source GetPolicySource(IntuneMAMPolicySource mamPolicySource)
        {
            switch (mamPolicySource)
            {
                case IntuneMAMPolicySource.MAM:
                    return Source.MAM;
                case IntuneMAMPolicySource.MDM:
                    return Source.MDM;
                case IntuneMAMPolicySource.Other:
                default:
                    return Source.Other;
            }
        }

    }
}
