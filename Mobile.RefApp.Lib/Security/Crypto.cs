using System;
using System.IO;
using System.Security.Cryptography;

namespace Mobile.RefApp.Lib.Security
{
    public static class Crypto
    {
        public static string HashSHA512(this string value)
        {
            using (var sha = SHA512.Create())
            {
                return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
            }
        }

        public static string Encrypt(this string clearValue, string encryptionKey)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = CreateKey(encryptionKey);

                byte[] encrypted = EncryptStringToBytes(clearValue, aes.Key, aes.IV);
                return $"{Convert.ToBase64String(encrypted)};{Convert.ToBase64String(aes.IV)}";
            }
        }

        private static byte[] EncryptStringToBytes(string value, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException($"{nameof(value)}");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException($"{nameof(key)}");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException($"{nameof(iv)}");

            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(value);
                    }
                    encrypted = memoryStream.ToArray();
                }
            }
            return encrypted;
        }

        public static string Decrypt(this string encryptedValue, string encryptionKey)
        {
            string iv = encryptedValue.Substring(encryptedValue.IndexOf(';') + 1, encryptedValue.Length - encryptedValue.IndexOf(';') - 1);
            encryptedValue = encryptedValue.Substring(0, encryptedValue.IndexOf(';'));

            return DecryptStringFromBytes(Convert.FromBase64String(encryptedValue), CreateKey(encryptionKey), Convert.FromBase64String(iv));
        }

        private static string DecryptStringFromBytes(byte[] value, byte[] key, byte[] iv)
        {
            if (value == null || value.Length <= 0)
                throw new ArgumentNullException($"{nameof(value)}");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException($"{nameof(key)}");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException($"{nameof(iv)}");

            string result = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream(value))
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (StreamReader streamReader = new StreamReader(cryptoStream))
                    result = streamReader.ReadToEnd();

            }
            return result;
        }

        private static byte[] CreateKey(string initializer, int keyBytes = 32)
        {
            byte[] salt = { 92, 82, 62, 52, 42, 32, 22, 14 };
            int iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(initializer, salt, iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
    }
}
