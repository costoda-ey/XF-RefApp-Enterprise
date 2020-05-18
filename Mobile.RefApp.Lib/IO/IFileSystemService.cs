using System.Collections.Generic;
using System.IO;

namespace Mobile.RefApp.Lib.IO
{
    public interface IFileSystemService
    {
        string LocalStoragePath { get; }
        string EvidenceStoragePath { get; }

        void CreateDirectory(string path);
        bool DirectoryExists(string path);
        string GetSpecialFolderPath(SpecialFolder folder);
        List<string> GetFilePathsInDirectory(string path);
        List<string> DirectoryContents(string path);
        void DeleteDirectory(string path);

        void DeleteFile(string path);
        void WriteFile(string path, string userIdentity, byte[] contents);
        void WriteFile(string path, string userIdentity, string contents);
        string ReadFile(string path, string userIdentity);
        bool FileExists(string path);
        long FileSize(string path);
        Stream GetInputStream(string path, string userIdentity);
        Stream GetOutputStream(string path, bool overwrite);
    }
}
