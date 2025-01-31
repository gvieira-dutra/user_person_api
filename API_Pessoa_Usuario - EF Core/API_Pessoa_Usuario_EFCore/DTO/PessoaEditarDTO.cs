namespace API_Pessoa_Usuario_EFCore.DTO
{
    public class PessoaEditarDTO
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? ApelidoFantasia { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
