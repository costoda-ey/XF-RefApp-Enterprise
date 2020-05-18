
namespace Mobile.RefApp.Lib.Intune.Enrollment
{
	public enum StatusCode : ulong
	{
		NewPoliciesReceived = 100,
		PoliciesHaveNotChanged = 101,
		WipeReceived = 102,
		NoPolicyReceived = 103,
		UnenrollmentSuccess = 104,
		AccountNotLicensed = 200,
		InternalError = 201,
		MamServiceDisabled = 202,
		AuthRequired = 203,
		LocationServiceFailure = 204,
		EnrollmentEndPointNetworkFailure = 205,
		ParsingFailure = 206,
		NullAccount = 207,
		AlreadyEnrolled = 208,
		NotEmmAccount = 209,
		MdmEnrolledDifferentUser = 210,
		NotDeviceAccount = 211,
		PolicyEndPointNetworkFailure = 212,
		AppNotEnrolled = 213,
		NotEnrolledAccount = 214,
		FailedToClearMamData = 215,
		Timeout = 216,
		ADALInternalError = 217,
		SwitchExistingAccount = 218,
		LoginCanceled = 219,
		PolicyRecordGone = 220,
		ReEnrollForUnenrolledUser = 221,
		MAMUnsupportedAPI,
		CompanyPortalRequired,
		EnrollmentSuccess,
		MdmEnrolled,
		Pending,
		RefreshPolicy,
		UnenrollmentFailed,
		StatusUnknown = 999
	}
}
