namespace Managers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    
    public static class EncryptionManager
    {
        private static readonly string DefaultKey = "kankangames-default-key";
    
        public static string Encrypt(string plainText, string key = null)
        {
            key ??= DefaultKey;
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV();
    
            using var encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    
            byte[] result = aes.IV.Concat(encrypted).ToArray();
            return Convert.ToBase64String(result);
        }
    
        public static string Decrypt(string encryptedText, string key = null)
        {
            key ??= DefaultKey;
            byte[] fullBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
    
            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = fullBytes.Take(16).ToArray();
    
            using var decryptor = aes.CreateDecryptor();
            byte[] decrypted = decryptor.TransformFinalBlock(fullBytes, 16, fullBytes.Length - 16);
    
            return Encoding.UTF8.GetString(decrypted);
        }
        
        public static string GetSHA256(string input)
        {
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

}