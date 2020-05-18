using Mobile.RefApp.Lib.Intune.FileProtection;

namespace EY.Mobile.iOS.Intune.DataProtection
{
    public class FileProtectionManagerService 
        : IFileProtectionManagerService
    {
        public string GetFileProtectionIdentity(string filePath)
        {
            return Microsoft.Intune.MAM.IntuneMAMFileProtectionManager.Instance.ProtectionInfo(filePath).Identity;
        }

        public bool IsFileEncrypted(string filePath)
        {
            return Microsoft.Intune.MAM.IntuneMAMFileProtectionManager.Instance.IsFileEncrypted(filePath);
        }

        public bool DecryptFile(string filePath)
        {
            return Microsoft.Intune.MAM.IntuneMAMFileProtectionManager.Instance.DecryptFile(filePath);
        }

        public bool EncryptFile(string filePath, string userIdentity)
        {
            return Microsoft.Intune.MAM.IntuneMAMFileProtectionManager.Instance.EncryptFile(filePath, userIdentity);
        }

        public void ProtectFile(string filePath, string userIdentity)
        {
            Microsoft.Intune.MAM.IntuneMAMFileProtectionManager.Instance.Protect(filePath, userIdentity);
        }
    }
}
