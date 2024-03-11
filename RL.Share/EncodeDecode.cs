using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Share
{
    public static class EncodeDecode
    {
        public static string Base64Encode(this string str)
        {
            try
            {
                var textBytes = System.Text.Encoding.UTF8.GetBytes(str);
                return System.Convert.ToBase64String(textBytes);
            }
            catch (Exception)
            {
                return str;
            }
        }

        public static string Base64Decode(this string str)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(str);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception)
            {
                return str;
            }
        }
    }
}
