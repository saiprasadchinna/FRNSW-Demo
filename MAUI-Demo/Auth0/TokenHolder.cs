using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo.Auth0
{
    public static class TokenHolder
    {
        public static string AccessToken { get; set; }
        public static string IdentityToken { get; set; }
        public static string ReferenceToken { get; set; }
    }
}
