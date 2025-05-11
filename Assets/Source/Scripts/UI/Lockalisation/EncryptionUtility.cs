using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Common
{
    public static class EncryptionUtility
    {
        private static readonly string _key = "1qJkdkYtDVI4aAb3Vp/qCVJnW9kUC6i6nkfWulq3qNE="; // Base64-encoded key
        private static readonly string _iv = "ppdk0gqd1ICosZHtb3JQOA=="; // Base64-encoded IV

        public static void GenerateKeyAndIV(int keySizeBits = 256)
        {
            if (keySizeBits != 128 && keySizeBits != 192 && keySizeBits != 256)
            {
                throw new ArgumentException("Key size must be 128, 192, or 256 bits.");
            }

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySizeBits;
                aes.GenerateKey();
                aes.GenerateIV();

                string key = Convert.ToBase64String(aes.Key);
                string iv = Convert.ToBase64String(aes.IV);

                Debug.Log($"Generated Key ({keySizeBits} bits, Base64): {key}\nGenerated IV (Base64): {iv}");
            }
        }

        public static string EncryptString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                Debug.LogError("Plain text cannot be null or empty.");
                return null;
            }

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Convert.FromBase64String(_key);
                    aes.IV = Convert.FromBase64String(_iv);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Encryption error: " + e.Message);
                return null;
            }
        }

        public static bool IsBase64String(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return false;

            base64 = base64.Trim();
            return (base64.Length % 4 == 0) && Regex.IsMatch(base64, "^[a-zA-Z0-9+/]*={0,2}$", RegexOptions.None);
        }

        public static string DecryptString(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                Debug.LogError("Encrypted text cannot be null or empty.");
                return null;
            }

            if (!IsBase64String(encryptedText))
            {
                Debug.LogError("The input string is not a valid Base64 string.");
                return null;
            }

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Convert.FromBase64String(_key);
                    aes.IV = Convert.FromBase64String(_iv);

                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (CryptographicException e)
            {
                Debug.LogError("Decryption error: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                Debug.LogError("Unexpected error during decryption: " + e.Message);
                return null;
            }
        }
    }
}