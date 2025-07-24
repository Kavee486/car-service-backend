
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace biZTrack.Static
{
    public static class StaticClass
    {
        public static string EncryptStringAES(string plainText)
        {
            var key = Encoding.UTF8.GetBytes("7412589631234567");
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                byte[] iv = aes.IV;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }

                    byte[] cipherText = memoryStream.ToArray();

                    byte[] combined = new byte[iv.Length + cipherText.Length];
                    Array.Copy(iv, 0, combined, 0, iv.Length);
                    Array.Copy(cipherText, 0, combined, iv.Length, cipherText.Length);

                    return Convert.ToBase64String(combined);
                }
            }
        }

        public static string DecryptStringAES(string encryptedValue)
        {
            var key = Encoding.UTF8.GetBytes("7412589631234567");
            byte[] combined = Convert.FromBase64String(encryptedValue);

            using (Aes aes = Aes.Create())
            {
                byte[] iv = new byte[aes.BlockSize / 8];
                byte[] cipherText = new byte[combined.Length - iv.Length];

                Array.Copy(combined, iv, iv.Length);
                Array.Copy(combined, iv.Length, cipherText, 0, cipherText.Length);

                aes.Key = key;
                aes.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(cryptoStream))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
