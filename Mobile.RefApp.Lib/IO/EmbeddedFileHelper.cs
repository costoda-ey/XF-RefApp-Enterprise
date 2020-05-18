using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Mobile.RefApp.Lib.IO
{
    public static class EmbeddedFileHelper
    {
        public static async Task<string> GetResourceString(
            Assembly assembly,
            string resourcePath)
        {
            string results = string.Empty;
            Stream stream = null;
            StreamReader sr = null;
            try
            {
                stream = assembly.GetManifestResourceStream(resourcePath);
                if (stream != null && stream.Length > 0)
                {
                    sr = new StreamReader(stream);
                    results = await sr.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                sr?.Dispose();
                sr = null;

                stream?.Dispose();
                stream = null;
            }
            return results;
        }

        public static async Task<byte[]> GetResourceBytes(
            Assembly assembly, 
            string resourcePath)
        {
            byte[] results = null;
            Stream stream = null;
            MemoryStream mr = new MemoryStream();

            try
            {
                stream = assembly.GetManifestResourceStream(resourcePath);
                if (stream != null && stream.Length > 0)
                {
                    await stream.CopyToAsync(mr);
                    results = mr.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                mr?.Dispose();
                mr = null;

                stream?.Dispose();
                stream = null;
            }
            return results;
        }
    }
}
