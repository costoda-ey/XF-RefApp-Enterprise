using System;
using System.Collections.Generic;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Intune.Mam.Client.App;
using Microsoft.Intune.Mam.Policy;
using Microsoft.Intune.Mam.Client.Notification;
using Microsoft.Intune.Mam.Policy.Notification;

using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Intune.Enrollment;
using Mobile.RefApp.Lib.Logging;

using Android.Runtime;
using Mobile.RefApp.Lib.Intune.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.RefApp.DroidLib.Intune.Enrollment
{
    public class EnrollmentService : Java.Lang.Object,
        IEnrollmentService,
        IMAMNotificationReceiver 
    {
        private readonly IAzureAuthenticatorEndpointService _authenticatorEndpointService;
        private readonly ILoggingService _loggingService;
        private readonly IMAMEnrollmentManager _enrollmentManager;
        private readonly IMAMNotificationReceiverRegistry _notificationRegistery;
        private readonly MAMWEAuthCallback mAMWEAuthCallback;

        private IMAMUserInfo _userInfo => MAMComponents.Get<IMAMUserInfo>();
        private Exception _registerError;

        public Endpoint Endpoint { get; set; }

        public string[] RegisteredAccounts { get; private set; }

        public string EnrolledAccount => _userInfo?.PrimaryUser;
        public bool IsIdentityManaged => (_userInfo == null);

        public Action<Status> EnrollmentRequestStatus { get; set; }
        public Action<Status> UnenrollmentRequestStatus { get; set; }
        public Action<Status> PolicyRequestStatus { get; set; }

        public EnrollmentService(
            ILoggingService loggingService,
            IAzureAuthenticatorEndpointService authenticatorEndpointService)
        {
            _loggingService = loggingService;
            _enrollmentManager = MAMComponents.Get<IMAMEnrollmentManager>();
            _authenticatorEndpointService = authenticatorEndpointService;
            _notificationRegistery = MAMComponents.Get<IMAMNotificationReceiverRegistry>();

            _registerError = null;
            Endpoint = null;
            RegisteredAccounts = new string[0];

            _notificationRegistery.RegisterReceiver(this, MAMNotificationType.MamEnrollmentResult);
            _notificationRegistery.RegisterReceiver(this, MAMNotificationType.RefreshPolicy);
            mAMWEAuthCallback = new MAMWEAuthCallback(_authenticatorEndpointService);
            _enrollmentManager.RegisterAuthenticationCallback(mAMWEAuthCallback);
        }

        public async Task RegisterAndEnrollAccountAsync(Endpoint endPoint)
        {
            try
            {
                if (endPoint != null)
                {
                    Endpoint = endPoint;
                    mAMWEAuthCallback.CurrentEndpoint = endPoint;

                    var token = await _authenticatorEndpointService.AcquireTokenSilentAsync(endPoint);
                    if (token != null)
                    {
                        _loggingService.LogInformation(typeof(EnrollmentService), $"{Lib.Intune.Constants.Enrollment.ENROLLMENTLOGTAG} UPN {token.UserInfo.DisplayableId}\n TenantId: {token.UserInfo.UniqueId}\n AadId: {token.TenantId} \n");
                        InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = "Starting Register and Enrollment", Module = SDKModule.Enrollment });
                        _enrollmentManager.RegisterAccountForMAM(token.UserInfo.DisplayableId, token.UserInfo.UniqueId, token.TenantId);
                    }
                    else
                        throw new Exception(Lib.Intune.Constants.Enrollment.ERRORNULL);
                }
                else
                    throw new Exception(Lib.Intune.Constants.Enrollment.ERRORENDPOINTNULL);
            }
            catch (Exception ex)
            {
                var status = new Status
                {
                    Error = ex.Message,
                    DidSucceed = false,
                    StatusCode = StatusCode.InternalError
                };

                _loggingService.LogError(typeof(EnrollmentService), ex, ex.Message);
                EnrollmentRequestStatus(status);
            }
        }

        public Task LoginAndEnrollAccountAsync(Endpoint endPoint, string identity = null)
        {
            return RegisterAndEnrollAccountAsync(endPoint);
        }

        public async Task DeRegisterAndUnenrollAccountAsync(bool withWipe = false)
        {
            try
            {
                var token = await _authenticatorEndpointService.AcquireTokenSilentAsync(Endpoint);
                if (token != null)
                {
                    InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = "Starting Deregister and Unenrollment", Module = SDKModule.Unenrollment });
                    _enrollmentManager.UnregisterAccountForMAM(token.UserInfo.UniqueId);
                }
                else
                    throw new Exception(Lib.Intune.Constants.Enrollment.ERRORNULL);
            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(EnrollmentService), ex, ex.Message);
            }
        }

        public new void Dispose()
        {
            if (_enrollmentManager != null)
                _enrollmentManager.Dispose();
        }

        public bool OnReceive(IMAMNotification notification)
        {
            var status = new Status();

            if (notification.Type == MAMNotificationType.MamEnrollmentResult)
            {
                var en = notification.JavaCast<IMAMEnrollmentNotification>();
                var result = en.EnrollmentResult;

                if (EnrollmentRequestStatus != null)
                {
                    if (result == MAMEnrollmentManagerResult.AuthorizationNeeded)
                    {
                        status.StatusCode = StatusCode.AuthRequired;
                        status.DidSucceed = false;
                        status.Error = _registerError?.ToString();
                        _registerError = null;
                    }
                    else if (result == MAMEnrollmentManagerResult.CompanyPortalRequired)
                    {
                        status.StatusCode = StatusCode.CompanyPortalRequired;
                        status.DidSucceed = false;
                        status.Error = _registerError?.ToString();
                        _registerError = null;
                    }
                    else if (result == MAMEnrollmentManagerResult.EnrollmentFailed)
                    {
                        status.StatusCode = StatusCode.AppNotEnrolled;
                        status.DidSucceed = false;
                        status.Error = _registerError?.ToString();
                        _registerError = null;
                    }
                    else if (result == MAMEnrollmentManagerResult.EnrollmentSucceeded)
                    {
                        status.StatusCode = StatusCode.EnrollmentSuccess;
                        status.DidSucceed = true;
                        status.Error = _registerError?.ToString();
                        _registerError = null;
                    }
                    else if (result == MAMEnrollmentManagerResult.MdmEnrolled)
                    {
                        status.StatusCode = StatusCode.MdmEnrolled;
                        status.DidSucceed = true;
                        status.Error = null;
                        _registerError = null;
                    }
                    else if (result == MAMEnrollmentManagerResult.NotLicensed)
                    {
                        status.StatusCode = StatusCode.AccountNotLicensed;
                        status.DidSucceed = false;
                        status.Error = _registerError?.ToString();
                        _registerError = null;
                    }
                    else if (result == MAMEnrollmentManagerResult.WrongUser)
                    {
                        status.StatusCode = StatusCode.MdmEnrolledDifferentUser;
                        status.DidSucceed = false;
                        status.Error = _registerError?.ToString();
                        _registerError = null;
                    }

                    InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = $"Did Succeed: {status.DidSucceed} Status Code: { status.StatusCode}, Error: {status.Error}", Module = SDKModule.Enrollment });

                    EnrollmentRequestStatus(status);
                }
                else if (UnenrollmentRequestStatus != null)
                {

                    if (result == MAMEnrollmentManagerResult.UnenrollmentFailed)
                    {
                        status.StatusCode = StatusCode.UnenrollmentFailed;
                        status.DidSucceed = false;
                        status.Error = _registerError?.ToString();
                        _registerError = null;
                    }
                    else if (result == MAMEnrollmentManagerResult.UnenrollmentSucceeded)
                    {
                        status.StatusCode = StatusCode.UnenrollmentSuccess;
                        status.DidSucceed = true;
                        status.Error = _registerError?.ToString();
                        _registerError = null;
                    }

                    InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = $"Did Succeed: {status.DidSucceed} Status Code: { status.StatusCode}, Error: {status.Error}", Module = SDKModule.Unenrollment });
                    UnenrollmentRequestStatus(status);
                }
            }
            else if (notification.Type == MAMNotificationType.RefreshPolicy)
            {
                status.StatusCode = StatusCode.RefreshPolicy;
                status.DidSucceed = true;
                status.Error = null;

                InTuneLoggingService.Instance.AddMessage(new LoggingMessage { LogDate = DateTime.Now, Message = "Refresh Policy", Module = SDKModule.Policies });

                PolicyRequestStatus(status);
            }
            return true;
        }

    }

    public class MAMWEAuthCallback
        : Java.Lang.Object,
          IMAMServiceAuthenticationCallback
    {
        private readonly IAzureAuthenticatorEndpointService _authenticatorEndpointService;
        public Endpoint CurrentEndpoint { get; set; }


        public MAMWEAuthCallback(IAzureAuthenticatorEndpointService authenticatorEndpointService)
        {
            _authenticatorEndpointService = authenticatorEndpointService;
        }

        public string AcquireToken(string upn, string aadId, string resourceId)
        {
            var result = string.Empty;
            var tokens = _authenticatorEndpointService.GetCachedTokens(CurrentEndpoint).Where(x => x.Resource == CurrentEndpoint.ResourceId).ToList();
            if (tokens.Any())
            {
                var token = tokens[0];
                result = token.AccessToken;
            }
            return result;
        }
    }
}
