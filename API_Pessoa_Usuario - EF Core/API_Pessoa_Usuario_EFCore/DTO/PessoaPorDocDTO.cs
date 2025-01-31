namespace API_Pessoa_Usuario_EFCore.DTO
{
    public class PessoaPorDocDTO
    {
        public string Nome { get; set; }
        public string ApelidoFantasia { get; set; }
        public string TenantId { get; set; }
        public string? DocIdentidade { get; set; }
    }
}
