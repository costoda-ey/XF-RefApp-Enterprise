using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Mobile.RefApp.CoreUI.Interfaces;
using Mobile.RefApp.CoreUI.Base;
using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Intune;
using Mobile.RefApp.Lib.Intune.Enrollment;
using Mobile.RefApp.Lib.Logging;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Mobile.RefApp.CoreUI.ViewModels
{
    public class InTuneEnrollmentViewModel
        : BaseViewModel
    {
        private readonly IEndpointService _endpointService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IIntuneService _intuneService;
       
        public string SDKVersion => _intuneService.SDKVersion;

        private Endpoint _selectedEndpoint;
        public Endpoint SelectedEndpoint
        {
            get => _selectedEndpoint;
            set 
            { 
                SetProperty(ref _selectedEndpoint, value);
            }
        }

        public ObservableCollection<Endpoint> EndPoints { get; set; }

        private bool _isIdenityManaged;
        public bool IsIdentityManaged
        {
            get => _isIdenityManaged;
            set { SetProperty(ref _isIdenityManaged, value); }
        }

        private string _enrolledAccount;
        public string EnrolledAccount
        {
            get => _enrolledAccount;
            set { SetProperty(ref _enrolledAccount, value); }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private Status _enrollmentRequestStatus;
        public Status EnrollmentRequestStatus
        {
            get => _enrollmentRequestStatus;
            set => SetProperty(ref _enrollmentRequestStatus, value);
        }

        private Status _unenrollmentRequestStatus;
        public Status UnenrollmentRequestStatus
        {
            get => _unenrollmentRequestStatus;
            set => SetProperty(ref _unenrollmentRequestStatus, value);
        }

        private Status _policyRequestStatus;
        public Status PolicyRequestStatus
        {
            get => _policyRequestStatus;
            set { SetProperty(ref _policyRequestStatus, value); }
        }

        public ICommand IntuneEnrollAccountCommand { get; }
        public ICommand LoginEnrollAccountCommand { get; }
        public ICommand DeregisterUnenrollCommand { get; }

        public Action<string> DisplayAlertAction { get; set; }

        public InTuneEnrollmentViewModel(
            ILoggingService loggingService,
            IEnrollmentService enrollmentService,
            IIntuneService intuneService,
            IEndpointService endpointService)
            : base (loggingService)
        {
            Title = "InTune Enrollment";

            _enrollmentService = enrollmentService;
            _intuneService = intuneService;
            _endpointService = endpointService;

            EndPoints = new ObservableCollection<Endpoint>();

            //setup commands
            IntuneEnrollAccountCommand = new Command(() => DoIntuneEnrollAccount());

            LoginEnrollAccountCommand = new Command(() => DoLoginEnrollAccount(),
                                                () => IsNotBusy);

            DeregisterUnenrollCommand = new Command(() => DoDeregisterUnenroll());

            //setup intune action events
            _enrollmentService.EnrollmentRequestStatus = OnEnrollmentRequestStatus;
            _enrollmentService.UnenrollmentRequestStatus = OnUnenrollmentRequestStatus;
            _enrollmentService.PolicyRequestStatus = OnPolicyRequestStatus;
        }

        public async override Task Initialize(
            Dictionary<string, object> navigationsParams = null)
        {
            var endpoints = await _endpointService.GetEndpointsByPlatform(App.CurrentPlatform);

            if(endpoints.Any())
                endpoints.ForEach(x => EndPoints.Add(x));
        }

        private void DoLoginEnrollAccount()
        {
            Status = string.Empty;
            Task.Run(async () => await _enrollmentService.LoginAndEnrollAccountAsync(SelectedEndpoint));
        }

        private void DoIntuneEnrollAccount()
        {
            Status = string.Empty;
            Task.Run(async () => await _enrollmentService.RegisterAndEnrollAccountAsync(SelectedEndpoint));
        }

        private void DoDeregisterUnenroll()
        {
            Status = string.Empty;
            Task.Run(async () => await _enrollmentService.DeRegisterAndUnenrollAccountAsync(false));
        }

        private void OnPolicyRequestStatus(Status status)
        {
            Status = status.DidSucceed ? $"Policy Request - Status: {status.StatusCode}" : $"Error - {status.Error} {status.StatusCode}";

            PolicyRequestStatus = status;
            UpdateLoginStatus();
        }

        private void OnUnenrollmentRequestStatus(Status status)
        {
            if (status.DidSucceed)
                Status = $"Unenrollment Succeeded - Status: {status.StatusCode}";
            else
                Status = $"Error - {status.Error} {status.StatusCode}";

            UnenrollmentRequestStatus = status;

            EnrolledAccount = string.Empty;
            EnrollmentRequestStatus = null;

            UpdateLoginStatus();
        }

        private void OnEnrollmentRequestStatus(Status status)
        {
            if (!status.DidSucceed)
                Status = status.Error;
            
            EnrollmentRequestStatus = status;
            UnenrollmentRequestStatus = null;
            UpdateLoginStatus();
        }

        private void UpdateLoginStatus()
        {
            IsIdentityManaged = _enrollmentService.IsIdentityManaged;
            EnrolledAccount = _enrollmentService.EnrolledAccount;
            IsBusy = false;
        }

    }
}
