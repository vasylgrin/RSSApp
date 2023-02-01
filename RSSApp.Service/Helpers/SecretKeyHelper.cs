using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.Service.Helpers
{
    public static class SecretKeyHelper
    {
        public static SymmetricSecurityKey GetSecretKey()
        {
            byte[] secretKey = Encoding.UTF8.GetBytes("Super_puper_secret_Keyssss");

            return new SymmetricSecurityKey(secretKey);
        }
    }
}
