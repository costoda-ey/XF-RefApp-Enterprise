using System;
using System.Collections.Generic;

using Mobile.RefApp.Lib.Keychain;

using Security;

namespace Mobile.RefApp.iOSLib.Keychain
{
    public class KeychainService
        : IKeychainService

    {
        public ICollection<Lib.Keychain.SecRecord> GetRecordsFromKeychain(string key)
        {
            List<Lib.Keychain.SecRecord> returnResults = new List<Lib.Keychain.SecRecord>(); 
            var queryRecord = new Security.SecRecord(SecKind.GenericPassword)
            {
                AccessGroup = key
            };

            var records = SecKeyChain.QueryAsRecord(queryRecord, Int32.MaxValue, out SecStatusCode resultCode);
            if(resultCode == SecStatusCode.Success)
            {
                foreach (var r in records)
                {
                    returnResults.Add(GetSecRecord(r));
                }
            }
            return returnResults; 
        }

        private Lib.Keychain.SecRecord GetSecRecord(Security.SecRecord secRecord)
        {
            return new Lib.Keychain.SecRecord
            {
                 AccessGroup = secRecord.AccessGroup,
                 Account = secRecord.Account,
                 ApplicationLabel= secRecord.ApplicationLabel,
                 CreationDate  = (DateTime)secRecord.CreationDate,
                 Description = secRecord.Description,
                 Label = secRecord.Label,
                 Path= secRecord.Path,
                 SecurityDomain = secRecord.SecurityDomain,
                 Subject = secRecord.Subject,
                 Service = secRecord.Service
            };
        }

        public bool ClearRecordsFromKeychain(string key)
        {
            bool results = false;

            var queryRecord = new Security.SecRecord(SecKind.GenericPassword)
            {
                AccessGroup = key
            };
            var queryResults = SecKeyChain.Remove(queryRecord);

            if (queryResults == SecStatusCode.Success)
                results = true;

            return results;
        }
    }
}
