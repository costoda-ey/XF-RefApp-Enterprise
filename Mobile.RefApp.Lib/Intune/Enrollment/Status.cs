using System;

namespace Mobile.RefApp.Lib.Intune.Enrollment
{
	public class Status
	{
        public DateTimeOffset EventDate { get; set; }
		public bool DidSucceed { get; set; }
		public string Error { get; set; }
		public string Identity { get; set; }
		public StatusCode StatusCode { get; set; }
	}
}
