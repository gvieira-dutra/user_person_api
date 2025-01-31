namespace API_Pessoa_Usuario_EFCore.DTO
{
    public class PessoaDeleteDTO
    {
        public Guid Id { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
