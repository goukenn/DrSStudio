using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    public static class GSExtensions
    {

        /// <summary>
        /// get if the type implement interface
        /// </summary>
        /// <param name="t"></param>
        /// <param name="interface"></param>
        /// <returns></returns>
        internal static bool ImplementInterface(this Type t, Type @interface)
        {
            if (t == null)
                return false;
            Type r = t.GetInterface(@interface.FullName);
            return (r != null);
        }
        public static string MD5(this string input)
        {
            if (input == null)
                return string.Empty;

            // step 1, calculate MD5 hash from input
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
    }
}
