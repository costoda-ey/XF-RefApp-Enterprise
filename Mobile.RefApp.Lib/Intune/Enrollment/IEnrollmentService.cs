using Mobile.RefApp.Lib.ADAL;

using System;

using System.Threading.Tasks;

namespace Mobile.RefApp.Lib.Intune.Enrollment
{
	public interface IEnrollmentService
	{
		Endpoint Endpoint { get; set; }
		string EnrolledAccount { get; }
        string[] RegisteredAccounts { get; }
        bool IsIdentityManaged { get; }

        Action<Status> EnrollmentRequestStatus { get; set; }
		Action<Status> UnenrollmentRequestStatus { get; set; }
		Action<Status> PolicyRequestStatus { get; set; }

        Task RegisterAndEnrollAccountAsync(Endpoint endPoint);
        Task LoginAndEnrollAccountAsync(Endpoint endpoint, string identity = null);
        Task DeRegisterAndUnenrollAccountAsync(bool withWipe = false);


    }
}

