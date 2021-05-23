using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DocAssistant_Common.Models;

namespace DocAssistant_Common.Utils
{
    public static class Security
    {
        public static byte[] ComputeHash(string str)
        {
            using var sha = new SHA1Managed();

            return sha.ComputeHash(Encoding.ASCII.GetBytes(str));
            
        }
    }
}