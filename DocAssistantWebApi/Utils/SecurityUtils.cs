using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DocAssistantWebApi.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DocAssistantWebApi.Utils
{
    public abstract class SecurityUtils
    {
        private const int SaltSize = 10;

        public static byte[] GenerateRandomSalt(int size = SaltSize)
        {
            byte [] salt = new byte[size];
            
            RNGCryptoServiceProvider serviceProvider = new RNGCryptoServiceProvider();
            serviceProvider.GetBytes(salt);

            return salt;
        }

        private static string CreatePasswordHashUtil(byte[] salt, byte[] password)
        {
            var hashedPassword = HashStringHex(password,salt);

            return $"${SaltSize}${Convert.ToHexString(salt)}${hashedPassword}";
        }

        public static string HashStringHex(byte [] str,byte[] key) => Convert.ToHexString(new HMACSHA256(key).ComputeHash(str));

        public static string CreatePasswordHash(string password, byte[] salt) =>
            CreatePasswordHashUtil(salt, Encoding.Default.GetBytes(password));

        public static string CreatePasswordHash(string password) =>
            CreatePasswordHashUtil(GenerateRandomSalt(), Encoding.Default.GetBytes(password));

        public static bool VerifyPassword(string plain, string stored)
        {
            var splitted = stored.Split("$");
            
            byte[] salt = Convert.FromHexString(splitted[2]);
                
            string storedPassword = splitted[3];
            var plainHashed = HashStringHex(Encoding.Default.GetBytes(plain),salt);

            return storedPassword.Equals(plainHashed);
        }
    }
}