using System;
namespace Mobile.RefApp.Lib.SharePoint
{
    public class SharePointEndpoint : Network.Endpoint
    {
        public string RootSiteUrl { get; set; }
        public Guid RootSiteId { get; set; }
        public string ApiVersion { get; set; }

        public string SiteUrl { get; set; }
        public Guid SiteId { get; set; }
    }
}
