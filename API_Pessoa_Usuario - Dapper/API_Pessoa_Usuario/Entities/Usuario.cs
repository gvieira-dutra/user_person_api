using Dapper.Contrib.Extensions;

namespace API_Pessoa_Usuario.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        public Guid Id { get; set; }
        public Guid PessoaId { get; set; }
        [Write(false)]
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
    }
}
