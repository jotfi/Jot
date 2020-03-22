using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace jotfi.Jot.Base.Utils
{
    public class HashUtils
    {
        public static string GetSHA256Hash(string value)
        {
            byte[] valueBytes = Encoding.Default.GetBytes(value);
            using var SH256Password = SHA256.Create();
            byte[] hashValue = SH256Password.ComputeHash(valueBytes);
            return Convert.ToBase64String(hashValue);
        }
    }
}
