using System;
namespace Mobile.RefApp.Lib.Graph
{
    public static class Constants
    {
        public const string GraphAPIRootQueryV1 = @"https://graph.microsoft.com/v1.0";

        public const string HttpClientErrorNoToken = "Error:  Can't get http client because couldn't get token for authentication.  This usually means the Endpoint that was set is null or incorrect.";
    }
}
