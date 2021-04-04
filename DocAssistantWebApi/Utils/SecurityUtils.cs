using System;
using System.Security.Cryptography;
using System.Text;

namespace DocAssistantWebApi.Utils
{
    public abstract class SecurityUtils
    {
        private const int SaltSize = 10;
        
        private static byte[] GenerateRandomSalt()
        {
            byte [] salt = new byte[SaltSize];
            
            RNGCryptoServiceProvider serviceProvider = new RNGCryptoServiceProvider();
            serviceProvider.GetBytes(salt);

            return salt;
        }

        private static string CreatePasswordHashUtil(byte[] salt, byte[] password)
        {
            var hashedPassword = HashStringHex(password,salt);

            return $"${SaltSize}${Convert.ToHexString(salt)}${hashedPassword}";
        }

        private static bool TimingSafeEquals(byte[] a, byte[] b)
        {
            // Nincs built in (source) => https://social.msdn.microsoft.com/Forums/en-US/44498a4f-4649-4f1a-8f91-d7c8ab65d132/securestring-comparing-and-timing-attacks?forum=csharpgeneral
            
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            
            return diff == 0;
        }
        
        public static string HashStringHex(byte [] str,byte[] key) => Convert.ToHexString(new HMACSHA256(key).ComputeHash(str));

        public static string CreatePasswordHash(string password, byte[] salt) =>
            CreatePasswordHashUtil(salt, Encoding.Default.GetBytes(password));

        public static bool VerifyPassword(string plain, string stored)
        {
            var splitted = stored.Split("$");
            
            byte[] salt = Convert.FromHexString(splitted[2]);
                
            string storedPassword = splitted[3];
            var plainHashed = HashStringHex(Encoding.Default.GetBytes(plain),salt);

            return TimingSafeEquals(Encoding.Default.GetBytes(storedPassword), Encoding.Default.GetBytes(plainHashed));
        }
    }
}