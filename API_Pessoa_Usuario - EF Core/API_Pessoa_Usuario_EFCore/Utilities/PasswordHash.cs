using System.Security.Cryptography;
using System.Text;

namespace API_Pessoa_Usuario_EFCore.Utilities;

public class PasswordHash
{
    public static string PwHash(string senha)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha));

        StringBuilder builder = new StringBuilder();

        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}
