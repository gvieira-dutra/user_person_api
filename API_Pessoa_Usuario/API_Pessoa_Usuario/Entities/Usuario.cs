using System.Security.Cryptography;
using System.Text;

namespace API_Pessoa_Usuario.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public Guid PessoaId { get; set; }
        public Pessoa? PessoaPertencente { get; set; }
        public Guid TenantId { get; set; }
        public string? Apelido { get; set; }
        public string? CodigoAlternativo { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid? DeletedBy { get; set; }
        public bool? Ativo { get; set; }

        public string Login { get; set; }
        public string Senha { get; set; } 

        public static string HashSenha(string senha)
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
