using System.Threading.Tasks;

namespace Mobile.RefApp.Lib.SharePoint
{
    public interface ISPListService
    {
        ADAL.Endpoint Endpoint { get; set; }
        SharePointEndpoint SharePointEndpoint { get; set; }

        Task<byte[]> GetFileAsync(
            ADAL.Endpoint endPoint,
            string siteUrl,
            string apiVersion,
            string rootSiteUrl,
            string rootSiteId,
            string siteName,
            string fileUrl);

        Task<byte[]> GetFileAsync(
            ADAL.Endpoint endPoint,
            SharePointEndpoint sharePointEndpoint,
            string siteName,
            string fileUrl);

        Task<byte[]> GetFileAsync(
            string siteName,
            string fileUrl);

        Task<T> GetListItemsAsync<T>(
            string list,
            string query);

        Task<T> GetListItemsAsync<T>(
            ADAL.Endpoint endPoint,
            SharePointEndpoint sharePointEndPoint,
            string list,
            string query);

        Task<T> GetListItemsAsync<T>(
            ADAL.Endpoint endPoint,
            string siteUrl,
            string apiVersion,
            string rootSiteUrl,
            string rootSiteId,
            string list,
            string query);
    }
}
