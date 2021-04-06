using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DocAssistantWebApi.Utils
{
    public abstract class SecurityUtils
    {
        private const int SaltSize = 10;
        
        private static byte[] GenerateRandomSalt(int size = SaltSize)
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

        public static string GenerateAccessToken(long doctorId)
        {
            var salt = GenerateRandomSalt(32);

            var idBytes = BitConverter.GetBytes(doctorId);

            return Convert.ToHexString(idBytes)+"."+HashStringHex(salt, idBytes);
        }

        public static (bool, long) VerifyAccessToken(Dictionary<long, string> accessTokens,string accessToken)
        {

            var splitted = accessToken.Split('.');
            
            if (splitted.Length != 2) return (false,-1);

            var docIdBytes = Convert.FromHexString(splitted[0]);
            
            long docId = BitConverter.ToInt64(docIdBytes);

            if (!accessTokens.ContainsKey(docId)) return (false,-1);

            return (accessTokens[docId].Equals(splitted[1]),docId);
        }
    }
}