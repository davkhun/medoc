using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medoc.Infrastructure
{
    public static class PasswordCypher
    {
        private static string Salt = "EDecenuQav306ED";
        public static string GetPassword(string password)
        {
            var newPass = password + Salt;
            return MD5.Create(newPass);
        }
    }
}
