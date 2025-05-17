using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Helpers.RegFlow
{
    public class LoginRegHelper
    {
        public static string HashGen(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(password + "twutm2025");
            var ecodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(ecodedBytes).Replace("-", "").ToLower();
        }
    }
}
