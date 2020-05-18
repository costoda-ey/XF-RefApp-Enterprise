using System;
using System.Threading.Tasks;

using Mobile.RefApp.Lib.ADAL;
using Mobile.RefApp.Lib.Graph.Groups;
using Mobile.RefApp.Lib.Graph.User;


namespace Mobile.RefApp.Lib.Graph
{
    public partial class GraphService
    {
        //logged in user profile information
        //uses UserAccount for serialization
        private const string MeApiUri = "/me";
        private const string MeFullDemographicsApiUri = "/me?$select=AboutMe,Birthday,BusinessPhones,City,CompanyName,Country,Department,DisplayName,FaxNumber,GivenName,HireDate,Id,IMAddresses,Interests,JobTitle,Mail,MailNickname,mobilePhone,MySite,OfficeLocation,OnPremisesDistinguishedName,OnPremisesDomainName,OnPremisesLastSyncDateTime,OnPremisesSamAccountName,OnPremisesSyncEnabled,otherMails,PastProjects,PostalCode,PreferredLanguage,PreferredName,Responsibilities,Schools,Skills,State,StreetAddress,Surname,UsageLocation,UserPrincipalName,UserType";
        private const string MeFullApiUri = "/me?$select=AboutMe,assignedLicenses,assignedPlans,Birthday,BusinessPhones,City,CompanyName,Country,Department,DisplayName,FaxNumber,GivenName,HireDate,Id,IMAddresses,Interests,JobTitle,Mail,MailNickname,mobilePhone,MySite,OfficeLocation,OnPremisesDistinguishedName,OnPremisesDomainName,OnPremisesLastSyncDateTime,OnPremisesSamAccountName,OnPremisesSyncEnabled,otherMails,PastProjects,PostalCode,PreferredLanguage,PreferredName,Responsibilities,Schools,Skills,State,StreetAddress,Surname,UsageLocation,UserPrincipalName,UserType";
        private const string MyManagerApiUri = "/me/manager";

        //binary photo
        private const string MePhotoApiUri = "/me/photo/$value";

        //logged in user onedrive information
        private const string OneDriveMyItemsApiUri = "/me/drive/root/children";
        private const string OneDriveMyItemsRecentApiUri = "/me/drive/recent";
        private const string OneDriveSearchApiUri = "/me/drive/root/search";

        //logged in user people information
        private const string MePeopleWorkWithApiUri = "/me/people";
        private const string MePeopleWorkWithSearchApiUri = "/me/people/?$search={0}";

        //groups
        private const string MeGroupsBelongToApiUri = "/me/memberOf";
        private const string MeGroupsBelongToFullApiUri = "/me/memberOf?$select=classification,createdDateTime,description,displayName,groupTypes,id,licenseProcessingState,mail,mailEnabled,mailNickname,onPremisesLastSyncDateTime,onPremisesSecurityIdentifier,onPremisesSyncEnabled,preferredDataLocation,proxyAddresses,securityEnabled,visibility,assignedLicenses,licenseProcessingState";
        //teams
        private const string MeJoinedTeamsApiUri = "/me/joinedTeams";

        /// <summary>
        /// GetMyUserAccountFull - get account information for the current logged in user from Graph API
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<UserAccount> GetMyUserAccountFull(Endpoint endpoint)
        {
            try
            {
                var results = await GetItemsAsync<UserAccount>(MeFullApiUri, endpoint, false);
                if (results != null)
                    return results;

            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, ex.Message);
            }
            return null;
        }

        /// <summary>
        /// GetMyUserAccountFull - get account information for the current logged in user from Graph API
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<UserAccount> GetMyUserAccountFullDemographics(Endpoint endpoint)
        {
            try
            {
                var results = await GetItemsAsync<UserAccount>(MeFullDemographicsApiUri, endpoint, false);
                if (results != null)
                    return results;

            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, ex.Message);
            }
            return null;
        }

        /// <summary>
        /// GetMyUserAccount - get account information for the current logged in user from Graph API
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<UserAccount> GetMyUserAccount(Endpoint endpoint)
        {
            try
            {
                var results = await GetItemsAsync<UserAccount>(MeApiUri, endpoint, false);
                if (results != null)
                    return results;

            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, ex.Message);
            }
            return null;
        }

        /// <summary>
        /// GetMyGroups - get groups membership the current logged in user from Graph API
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<Group[]> GetMyGroups(Endpoint endpoint)
        {
            try
            {
                var results = await GetItemsAsync<GroupResults>(MeGroupsBelongToApiUri, endpoint, false);
                if (results != null)
                    return results.Groups;

            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, ex.Message);
            }
            return null;
        }

        /// <summary>
        /// GetMyGroups - get groups membership the current logged in user from Graph API
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<Group[]> GetMyGroupsFull(Endpoint endpoint)
        {
            try
            {
                var results = await GetItemsAsync<GroupResults>(MeGroupsBelongToFullApiUri, endpoint, false);
                if (results != null)
                    return results.Groups;

            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, ex.Message);
            }
            return null;
        }

        /// <summary>
        /// GetMyManagerUserAccount - get account information for the current logged in users assigned Manager from Graph API
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<UserAccount> GetMyManagerUserAccount(Endpoint endpoint)
        {
            try
            {
                var results = await GetItemsAsync<UserAccount>(MyManagerApiUri, endpoint, false);
                if (results != null)
                    return results;

            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, ex.Message);
            }
            return null;
        }

        /// <summary>
        /// GetMyManagerUserAccount - get account information for the current logged in users assigned Manager from Graph API
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<byte[]> GetMyPhoto(Endpoint endpoint)
        {
            try
            {
                var results = await GetFileAsync(MePhotoApiUri, endpoint);
                if (results != null)
                    return results;

            }
            catch (Exception ex)
            {
                _loggingService.LogError(typeof(GraphService), ex, ex.Message);
            }
            return null;
        }

    }
}
