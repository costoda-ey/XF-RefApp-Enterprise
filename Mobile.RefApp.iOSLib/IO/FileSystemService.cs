using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Mobile.RefApp.Lib.Intune.FileProtection;
using Mobile.RefApp.Lib.IO;
using Mobile.RefApp.Lib.Logging;

using Foundation;
using UIKit;

namespace Mobile.RefApp.iOSLib.IO
{
    public class FileSystemService
        : IFileSystemService
    {
        private readonly ILoggingService _loggingService;
        private readonly IFileProtectionManagerService _fileProtectionManagerService;

        public FileSystemService(
            ILoggingService loggingService,
            IFileProtectionManagerService fileProtectionManagerService)
        {
            _loggingService = loggingService;
            _fileProtectionManagerService = fileProtectionManagerService;
        }

        public string LocalStoragePath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string EvidenceStoragePath => Path.Combine(LocalStoragePath, "Evidence");

        public void CreateDirectory(string path)
            => UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                Directory.CreateDirectory(path);
            });

        public void DeleteDirectory(string path)
            => UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                NSError error = null;
                NSFileManager.DefaultManager.Remove(path, out error);
                if (error != null)
                {
                    _loggingService.LogError(typeof(FileSystemService), new Exception(error.DebugDescription), error.Description);
                    throw new NSErrorException(error);
                }
            });

        public void DeleteFile(string path)
        {
            if (FileExists(path))
            {
                NSError error = null;

                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    NSFileManager.DefaultManager.Remove(NSUrl.CreateFileUrl(new[] { path }), out error);
                });

                if (error != null)
                {
                    _loggingService.LogError(typeof(FileSystemService), new Exception(error.DebugDescription), error.Description);
                    throw new NSErrorException(error);
                }
            }
        }

        public List<string> DirectoryContents(string path)
        {
            var results = new List<string>();

            var files = NSFileManager.DefaultManager.GetEnumerator(path);
            var file = files?.NextObject();

            while (file != null)
            {
                var url = file as NSString;
                if (url != null)
                {
                    results.Add(url);
                }

                file = files?.NextObject();
            }

            return results;
        }

        public bool DirectoryExists(string path)
        {
            bool doesExist = false;

            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                doesExist = Directory.Exists(path);
            });

            return doesExist;
        }

        public bool FileExists(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var result = false;
                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    result = NSFileManager.DefaultManager.FileExists(path);
                });

                return result;
            }

            return false;
        }

        public long FileSize(string path)
        {
            if (FileExists(path))
            {
                NSError error = null;
                NSFileAttributes fileAttributes = null;
                ulong? size = null;

                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    fileAttributes = NSFileManager.DefaultManager.GetAttributes(path, out error);
                });

                if (error != null)
                {
                    _loggingService.LogError(typeof(FileSystemService), new Exception(error.DebugDescription), error.Description);
                    throw new NSErrorException(error);
                }

                size = fileAttributes.Size;

                return size.HasValue ? (long)size.Value : 0;
            }

            return 0;
        }

        public List<string> GetFilePathsInDirectory(string path)
        {
            var files = new List<string>();

            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                files = Directory.GetFiles(path).ToList();
            });

            return files;
        }

        public Stream GetInputStream(string path, string userIdentity)
        {
            NSFileHandle fileHandle = null;
            NSError error = null;
            NSData result = null;
            Exception exception = null;

            try
            {
                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    _fileProtectionManagerService.DecryptFile(path);
                    fileHandle = NSFileHandle.OpenReadUrl(NSUrl.CreateFileUrl(new[] { path }), out error);
                    result = fileHandle.ReadDataToEndOfFile();
                    _fileProtectionManagerService.EncryptFile(path, userIdentity);
                });
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                if (fileHandle != null)
                {
                    fileHandle.CloseFile();
                }
            }

            if (error != null)
            {
                throw new NSErrorException(error);
            }
            else if (exception != null)
            {
                throw exception;
            }

            return result?.AsStream() ?? null;
        }

        public Stream GetOutputStream(string path, bool overwrite)
        {
            return new FileStream(path, overwrite ? FileMode.Create : FileMode.CreateNew);
        }

        public string GetSpecialFolderPath(SpecialFolder folder)
        {
            switch (folder)
            {
                case SpecialFolder.MyDocuments:
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                case SpecialFolder.Personal:
                    return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                default:
                    return string.Empty;
            }
        }

        public string ReadFile(string path, string userIdentity)
        {
            string result = String.Empty;

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                if (FileExists(path))
                {
                    NSFileHandle fileHandle = null;
                    NSError error = null;
                    Exception exception = null;

                    try
                    {
                        UIApplication.SharedApplication.InvokeOnMainThread(() =>
                        {
                            _fileProtectionManagerService.DecryptFile(path);
                            fileHandle = NSFileHandle.OpenReadUrl(NSUrl.CreateFileUrl(new[] { path }), out error);
                            if (fileHandle != null)
                            {
                                var nsStringResult = NSString.FromData(fileHandle.ReadDataToEndOfFile(), NSStringEncoding.UTF8);
                                if (nsStringResult != null)
                                {
                                    result = nsStringResult.ToString();
                                }
                            }
                            _fileProtectionManagerService.EncryptFile(path, userIdentity);
                        });
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                    finally
                    {
                        if (fileHandle != null)
                        {
                            fileHandle.CloseFile();
                        }
                    }

                    if (error != null)
                    {
                        _loggingService.LogError(typeof(FileSystemService), new Exception(error.DebugDescription), error.Description);
                        throw new NSErrorException(error);
                    }
                    else if (exception != null)
                    {
                        _loggingService.LogError(typeof(FileSystemService), exception, exception.Message);
                        throw exception;
                    }
                }
            }

            return result;
        }

        public void WriteFile(string path, string userIdentity, byte[] contents)
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(userIdentity))
            {
                if (NSFileManager.DefaultManager.FileExists(path))
                {
                    DeleteFile(path);
                }

                var dict = new NSMutableDictionary();

                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    NSFileManager.DefaultManager.CreateFile(path, NSData.FromArray(contents), dict);
                });
                _fileProtectionManagerService.ProtectFile(path, userIdentity);
                _fileProtectionManagerService.EncryptFile(path, userIdentity);
            }
        }

        public void WriteFile(string path, string userIdentity, string contents)
        {
            if (!string.IsNullOrEmpty(path) || !string.IsNullOrEmpty(contents))
            {
                if (!NSFileManager.DefaultManager.FileExists(path))
                {
                    var dict = new NSMutableDictionary();
                    UIApplication.SharedApplication.InvokeOnMainThread(() =>
                    {
                        NSFileManager.DefaultManager.CreateFile(path, NSData.FromString(contents), dict);
                    });
                    _fileProtectionManagerService.ProtectFile(path, userIdentity);
                    _fileProtectionManagerService.EncryptFile(path, userIdentity);
                }
                else
                {
                    NSFileHandle fileHandle = null;
                    NSError error = null;
                    Exception exception = null;

                    try
                    {
                        UIApplication.SharedApplication.InvokeOnMainThread(() =>
                        {
                            _fileProtectionManagerService.DecryptFile(path);
                            fileHandle = NSFileHandle.OpenUpdateUrl(NSUrl.CreateFileUrl(new[] { path }), out error);
                            fileHandle.SeekToEndOfFile();
                            fileHandle.WriteData(NSData.FromString(contents));

                            _fileProtectionManagerService.ProtectFile(path, userIdentity);
                            _fileProtectionManagerService.EncryptFile(path, userIdentity);

                        });
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                    finally
                    {
                        if (fileHandle != null)
                        {
                            UIApplication.SharedApplication.InvokeOnMainThread(() =>
                            {
                                fileHandle.CloseFile();
                            });
                        }
                    }

                    if (error != null)
                    {
                        _loggingService.LogError(typeof(FileSystemService), new Exception(error.DebugDescription), error.Description);
                        throw new NSErrorException(error);
                    }
                    else if (exception != null)
                    {
                        _loggingService.LogError(typeof(FileSystemService), exception, exception.Message);
                        throw exception;
                    }
                }
            }
        }
    }
}
