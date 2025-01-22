using System.Security.Cryptography;
using System.Text;

namespace API_Pessoa_Usuario_Dapper.Utilities
{
    public class PasswordHash
    {
        public static string PwHash(string senha)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
