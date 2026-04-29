using System.Security.Cryptography;
using System.Text;

namespace PapelariaMVCProjeto.Helpers
{
    public static class HashHelper
    {
        public static string GerarHash(string senha)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                byte[] hash = md5.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}