using System;
using System.Threading.Tasks;

using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Intune.Enrollment;
using Mobile.RefApp.Lib.Logging;
using Mobile.RefApp.Lib.Intune.Logging;

using Microsoft.Intune.MAM;

namespace Mobile.RefApp.iOSLib.Intune.Enrollment
{
	public class EnrollmentService
	  : IntuneMAMEnrollmentDelegate,
		IEnrollmentService
	{
        private readonly ILoggingService _loggingService;
        private readonly IAzureAuthenticatorEndpointService _authenticatorEndpointService;

        public Action<Status> EnrollmentRequestStatus { get; set; }

        public Action<Status> UnenrollmentRequestStatus { get; set; }

        public Action<Status> PolicyRequestStatus { get; set; }

        public bool IsIdentityManaged
            => IntuneMAMPolicyManager.Instance.IsIdentityManaged(IntuneMAMEnrollmentManager.Instance.EnrolledAccount);

        public string EnrolledAccount
            => IntuneMAMEnrollmentManager.Instance.EnrolledAccount;

        public string[] RegisteredAccounts
            => IntuneMAMEnrollmentManager.Instance.RegisteredAccounts;

        public Endpoint Endpoint { get; set; }

        public EnrollmentService(
            ILoggingService loggingService,
            IAzureAuthenticatorEndpointService authenticatorEndpointService)
        {
            _loggingService = loggingService;
            _authenticatorEndpointService = authenticatorEndpointService;

            IntuneMAMEnrollmentManager.Instance.Delegate = this;
        }

        public Task LoginAndEnrollAccountAsync(
            Endpoint endPoint,
            string identity = null)
        {
            try
            {
                if (endPoint != null)
                    Endpoint = endPoint;

                if (Endpoint != null)
                    SetAdalInformation(endPoint);

                InvokeOnMainThread(() =>
                {
                    InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = "Starting Login and Enrollment", Module = SDKModule.Enrollment });
                    IntuneMAMEnrollmentManager.Instance.LoginAndEnrollAccount(identity);
                });
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(EnrollmentService), ex, ex.Message);
            }
            return Task.CompletedTask;
        }

        public Task RegisterAndEnrollAccountAsync(Endpoint endPoint)
        {
            try
            {
                if (endPoint != null)
                {
                    Endpoint = endPoint;
                    SetAdalInformation(endPoint);
                    InvokeOnMainThread(async () =>
                    {
                        //get cache token from ADAL
                        var token = await _authenticatorEndpointService.AcquireTokenSilentAsync(Endpoint);
                        if (token != null)
                        {
                            InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = "Starting Register and Enrollment", Module = SDKModule.Enrollment });

                            IntuneMAMEnrollmentManager.Instance.RegisterAndEnrollAccount(
                            token.UserInfo.DisplayableId);
                        }
                    });
                }
                else
                    throw new Exception(Lib.Intune.Constants.Enrollment.ERRORNULL);

            }
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch (Exception ex)
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
#pragma warning restore CS0168 // Variable is declared but never used
            {
#if DEBUG
                _loggingService.LogError(typeof(EnrollmentService), ex, ex.Message);
#endif
            }
            return Task.CompletedTask;
        }

        public Task DeRegisterAndUnenrollAccountAsync(bool withWipe = false)
        {
            try
            {
                InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = "Starting Deregister and Unenrollment", Module = SDKModule.Unenrollment });
                IntuneMAMEnrollmentManager.Instance.DeRegisterAndUnenrollAccount(IntuneMAMEnrollmentManager.Instance.EnrolledAccount, withWipe);
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(EnrollmentService), ex, ex.Message);
            }
            return Task.CompletedTask;
        }

        //set ADAL information via c# instead of info.plist file
        private void SetAdalInformation(Endpoint endpoint)
        {
            //IntuneMAMPolicyManager.Instance.AadClientIdOverride = adalClientId;
            //IntuneMAMPolicyManager.Instance.AadAuthorityUriOverride = adalAuthority;
            //IntuneMAMPolicyManager.Instance.AadRedirectUriOverride = adalRedirect;
        }

        public override void EnrollmentRequestWithStatus(IntuneMAMEnrollmentStatus status)
        {
            if (EnrollmentRequestStatus != null)
            {
                var es = new Status
                {
                    DidSucceed = status.DidSucceed,
                    Error = status.ErrorString,
                    StatusCode = MapStatusCode(status.StatusCode)
                };
                InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = $"Enrollment Update Status: {es.StatusCode}, Error: {es.Error}", Module = SDKModule.Enrollment });
                EnrollmentRequestStatus(es);
            }
        }

        public override void PolicyRequestWithStatus(IntuneMAMEnrollmentStatus status)
        {
            if (PolicyRequestStatus != null)
            {
                var es = new Status
                {
                    DidSucceed = status.DidSucceed,
                    Error = status.ErrorString,
                    StatusCode = MapStatusCode(status.StatusCode)
                };
                InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = $"Policy Update Status: {es.StatusCode}, Error: {es.Error}", Module = SDKModule.Policies });
                PolicyRequestStatus(es);
            }
        }

        public override void UnenrollRequestWithStatus(IntuneMAMEnrollmentStatus status)
        {
            if (UnenrollmentRequestStatus != null)
            {
                var ues = new Status
                {
                    DidSucceed = status.DidSucceed,
                    Error = status.ErrorString,
                    StatusCode = MapStatusCode(status.StatusCode)
                };
                InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = $"Unenrollment Update Status: {ues.StatusCode}, Error: {ues.Error}", Module = SDKModule.Unenrollment });
                UnenrollmentRequestStatus(ues);
            }
        }

        private StatusCode MapStatusCode(IntuneMAMEnrollmentStatusCode status)
        {
            switch (status)
            {
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusAccountNotLicensed:
                    return StatusCode.AccountNotLicensed;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusADALInternalError:
                    return StatusCode.ADALInternalError;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusAlreadyEnrolled:
                    return StatusCode.AlreadyEnrolled;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusAppNotEnrolled:
                    return StatusCode.AppNotEnrolled;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusAuthRequired:
                    return StatusCode.AuthRequired;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusEnrollmentEndPointNetworkFailure:
                    return StatusCode.EnrollmentEndPointNetworkFailure;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusFailedToClearMamData:
                    return StatusCode.FailedToClearMamData;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusInternalError:
                    return StatusCode.InternalError;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusLocationServiceFailure:
                    return StatusCode.LocationServiceFailure;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusLoginCanceled:
                    return StatusCode.LoginCanceled;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusMamServiceDisabled:
                    return StatusCode.MamServiceDisabled;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusMdmEnrolledDifferentUser:
                    return StatusCode.MdmEnrolledDifferentUser;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusNewPoliciesReceived:
                    return StatusCode.NewPoliciesReceived;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusNilAccount:
                    return StatusCode.NullAccount;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusNoPolicyReceived:
                    return StatusCode.NoPolicyReceived;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusNotDeviceAccount:
                    return StatusCode.NotDeviceAccount;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusNotEmmAccount:
                    return StatusCode.NotEmmAccount;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusNotEnrolledAccount:
                    return StatusCode.AppNotEnrolled;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusParsingFailure:
                    return StatusCode.ParsingFailure;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusPoliciesHaveNotChanged:
                    return StatusCode.PoliciesHaveNotChanged;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusPolicyEndPointNetworkFailure:
                    return StatusCode.PolicyEndPointNetworkFailure;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusPolicyRecordGone:
                    return StatusCode.PolicyRecordGone;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusReEnrollForUnenrolledUser:
                    return StatusCode.ReEnrollForUnenrolledUser;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusSwitchExistingAccount:
                    return StatusCode.SwitchExistingAccount;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusTimeout:
                    return StatusCode.Timeout;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusUnenrollmentSuccess:
                    return StatusCode.UnenrollmentSuccess;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusUnsupportedAPI:
                    return StatusCode.MAMUnsupportedAPI;
                case IntuneMAMEnrollmentStatusCode.MAMEnrollmentStatusWipeReceived:
                    return StatusCode.WipeReceived;
            }
            return StatusCode.StatusUnknown;
        }
    }
}