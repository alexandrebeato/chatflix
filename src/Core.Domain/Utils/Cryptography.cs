using System.Security.Cryptography;
using System.Text;

namespace Core.Domain.Utils
{
    public class Cryptography
    {
        public static string EncodeWithMD5(string valor)
        {
            using (var md5Hash = MD5.Create())
            {
                string hash = GetHashMd5(md5Hash, valor);
                return hash;
            }
        }

        private static string GetHashMd5(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string EncodeWithBase64(string value)
        {
            var decodedValue = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var bytes = Encoding.UTF8.GetBytes(value);
                    decodedValue = Convert.ToBase64String(bytes);
                }
            }
            catch { }

            return decodedValue;
        }

        public static string DecodeWithBase64(string value)
        {
            var decodedValue = string.Empty;

            try
            {
                var bytes = Convert.FromBase64String(value);
                decodedValue = Encoding.UTF8.GetString(bytes);
            }
            catch { }

            return decodedValue;
        }
    }
}