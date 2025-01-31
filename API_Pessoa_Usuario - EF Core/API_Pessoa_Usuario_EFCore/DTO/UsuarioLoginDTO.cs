namespace API_Pessoa_Usuario_EFCore.DTO
{
    public class UsuarioLoginDTO
    {
        public UsuarioLoginDTO(string login, string senha, Guid? usuarioId, string? pessoaNome, string? pessoaSite, string? usuarioApelido, DateTime? usuarioCreatedOn)
        {
            Login = login;
            Senha = senha;
            UsuarioId = usuarioId;
            PessoaNome = pessoaNome;
            PessoaSite = pessoaSite;
            UsuarioApelido = usuarioApelido;
            UsuarioCreatedOn = usuarioCreatedOn;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        public Guid? UsuarioId { get; set; }
        public string? PessoaNome { get; set; }
        public string? PessoaSite { get; set; }
        public string? UsuarioApelido { get; set; }
        public DateTime? UsuarioCreatedOn { get; set; }
        public string Mensagem { get; set; } = "Login e senha validados. Pode trabalhar com o token.";
    }
}
