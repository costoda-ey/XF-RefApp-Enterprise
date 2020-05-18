using System;
using Mobile.RefApp.Lib.Intune.Policies;
using Mobile.RefApp.Lib.Logging;

namespace Mobile.RefApp.DroidLib.Intune.Policies
{
    public class PolicyService
        : IPolicyService
    {
        private readonly ILoggingService _loggingService;

        public string IdentityForCurrentActivity => throw new NotImplementedException();

        public bool IsContactSyncAllowed => throw new NotImplementedException();

        public bool IsManagedBrowserRequired => throw new NotImplementedException();

        public bool IsPINRequired => throw new NotImplementedException();

        public bool IsScreenCaptureAllowed => throw new NotImplementedException();

        public bool IsSaveToPersonalAllowed => throw new NotImplementedException();

        public bool IsFileEncrytionUsed => throw new NotImplementedException();

        public PolicyService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public bool IsIdentityManaged(string identity)
        {
            throw new NotImplementedException();
        }

        public bool IsSavedToAllowedForLocation(SaveLocation location, string accountName)
        {
            throw new NotImplementedException();
        }

        public bool IsUrlAllowed(string url)
        {
            throw new NotImplementedException();
        }

        public string[] GetAadAuthorityUriForIdentity(string identity)
        {
            throw new NotImplementedException();
        }
    }
}
