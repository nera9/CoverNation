using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CoverNation.Helper
{
    public class Security
    {
        //Password hashing
        public static string PasswordHash(string password)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

    }
}