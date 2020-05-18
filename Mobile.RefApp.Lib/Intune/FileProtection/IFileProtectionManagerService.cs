
namespace Mobile.RefApp.Lib.Intune.FileProtection
{
    public interface IFileProtectionManagerService
    {
        bool IsFileEncrypted(string filePath);
        bool DecryptFile(string filePath);
        bool EncryptFile(string filePath, string userIdentity);

        void ProtectFile(string filePath, string userIdentity);
        string GetFileProtectionIdentity(string filePath);
    }
}
