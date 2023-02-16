using System.Security.Cryptography;
using System.Text;

namespace Analisystem.Helper
{
    public static class Encrypting
    {
        public static string GenerateHash(this string value)
        {
            var SHA1hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var bytesArray = encoding.GetBytes(value);
            bytesArray = SHA1hash.ComputeHash(bytesArray);

            var builder = new StringBuilder();
            foreach (var item in bytesArray)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
