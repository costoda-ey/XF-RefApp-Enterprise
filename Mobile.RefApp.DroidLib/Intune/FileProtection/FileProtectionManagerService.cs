using Mobile.RefApp.Lib.Intune.FileProtection;

using Java.IO;

namespace Mobile.RefApp.DroidLib.Intune.FileProtection
{
    public class FileProtectionManagerService
        : IFileProtectionManagerService
    {
        public string GetFileProtectionIdentity(string filePath)
        {
            string value = string.Empty;
            using (File file = new File(filePath))
            {
                value = Microsoft.Intune.Mam.Client.Identity.MAMFileProtectionManager.GetProtectionInfo(file).Identity;
            }
            return value;
        }

        public bool IsFileEncrypted(string filePath)
        {
            return !string.IsNullOrEmpty(GetFileProtectionIdentity(filePath));
        }

        public bool DecryptFile(string filePath)
        {
            //per Intune Team @Microsoft this is automatically done by the SDK - no need to implement
            return true;
        }

        public bool EncryptFile(string filePath, string userIdentity)
        {
            //per Intune Team @Microsoft this is automatically done by the SDK - no need to implement
            return true;
        }

        public void ProtectFile(string filePath, string userIdentity)
        {
            using (File file = new File(filePath))
            {
                Microsoft.Intune.Mam.Client.Identity.MAMFileProtectionManager.Protect(file, userIdentity);
            }
        }
    }
}
