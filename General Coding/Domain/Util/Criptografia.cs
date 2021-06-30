using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.Domain.Utils
{
    public class Criptografia
    {
        public static string SHA1Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }

        public static string MD5Encode(string value)
        {
            var hash = System.Security.Cryptography.MD5.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }

        public static string AuthTokenGenerate(string nome, DateTime dt_cadastro)
        {
            return MD5Encode(nome + dt_cadastro.ToString("s"));
        }
    }
}