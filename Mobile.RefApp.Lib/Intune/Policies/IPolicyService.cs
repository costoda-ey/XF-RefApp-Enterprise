
namespace Mobile.RefApp.Lib.Intune.Policies
{
	public interface IPolicyService
	{
		string IdentityForCurrentActivity { get; }

		//from policy model/interface
		bool IsContactSyncAllowed { get; }
		bool IsManagedBrowserRequired { get; }
		bool IsPINRequired { get; }

		bool IsSavedToAllowedForLocation(SaveLocation location,
										 string accountName);
		bool IsUrlAllowed(string url);

        bool IsIdentityManaged(string identity);

        string[] GetAadAuthorityUriForIdentity(string identity);

        //only iOS properities
#if iOS
        Source PolicySource { get; }
        string[] IntuneLogPaths { get; }
        bool ShouldFileProviderEncryptFiles { get; }
        bool IsAppSharingAllowed { get; }
        bool SpotlightIndexingAllowed { get; }
        bool IsUniversalLinkAllowed(string url);
        bool IsDocumentPickerAllowed(DocumentPickerMode documentPickerMode);
        bool TelemetryEnabled { get; }
        string PrimaryUser { get; }
        string ProcessIdentity { get; }
        string UIPolicyIdentity { get; }
#endif

#if Android
        bool IsScreenCaptureAllowed { get; }
        bool IsSaveToPersonalAllowed { get; }
        bool IsFileEncrytionUsed { get; }
#endif

    }
}
