using System;

namespace Mobile.RefApp.Lib.Network
{
    public class Endpoint
    {
        public Environment Environment { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class UriEndPoint 
        : Endpoint
    {
        public Uri UriPath { get; set; }
        public string Token { get; set; }
        public bool UseToken { get; set; }
    }
}
