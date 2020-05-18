using System.Collections.Generic;

namespace Mobile.RefApp.Lib.Keychain
{
    public interface IKeychainService
    {
        ICollection<Lib.Keychain.SecRecord> GetRecordsFromKeychain(string key);
        bool ClearRecordsFromKeychain(string key);
    }
}
