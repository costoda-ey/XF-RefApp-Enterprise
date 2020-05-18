using System.Threading.Tasks;

using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Graph.Groups;
using Mobile.RefApp.Lib.Graph.User;

namespace Mobile.RefApp.Lib.Graph
{
    public interface IGraphService
    {
        //me api's
        Task<UserAccount> GetMyUserAccountFull(Endpoint endpoint);
        Task<UserAccount> GetMyUserAccountFullDemographics(Endpoint endpoint);
        Task<UserAccount> GetMyUserAccount(Endpoint endpoint);
        Task<UserAccount> GetMyManagerUserAccount(Endpoint endpoint);
        Task<byte[]> GetMyPhoto(Endpoint endpoint);

        //group api's
        Task<Group[]> GetMyGroups(Endpoint endpoint);
        Task<Group[]> GetMyGroupsFull(Endpoint endpoint);
    }
}
