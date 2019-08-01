using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace pnsms.utility
{
    public class EncryptionDecreption
    {
        public static string EncryptToMD5(string value)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();

            byte[] hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(value));
            
            return Convert.ToBase64String(hashedBytes); 
            
            //MD5 of 123 is ICy5YqxZB1uWSwcVLSNLcA==            
        }

        public static string EncryptUserId(string userId)
        {
            return userId;
        }

        public static string DecryptUserId(string userId)
        {
            
            return userId;
        }
    }
}