using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EY.Mobile.iOS.Network;
using EY.Mobile.Lib.ADAL;
using EY.Mobile.Lib.Logging;
using EY.Mobile.Lib.Network;
using Foundation;
using UIKit;

namespace EY.Mobile.iOS.ADAL
{
	public static class AzureAuthenticatorFactory
	{
		public static AzureAuthenticatorService GetAzureAuthenticator(
			IAzurePlatformParameters parameters = null,
			IPlatformHttpClientHandler httpHandler = null,
			ILoggingService loggingService = null
		)
		{
			if (parameters == null)
				parameters = new AzurePlatformParameters();
			if (httpHandler == null)
				httpHandler = new IosHttpClientHandler();

			return new AzureAuthenticatorService(parameters,
										  httpHandler,
										  loggingService);
		}
	}
}