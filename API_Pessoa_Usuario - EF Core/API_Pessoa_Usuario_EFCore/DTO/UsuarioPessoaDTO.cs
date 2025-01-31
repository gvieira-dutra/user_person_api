using API_Pessoa_Usuario_EFCore.Domain;

namespace API_Pessoa_Usuario_Dapper
{
    public class UsuarioPessoaDTO
    {
        public UsuarioPessoaDTO(string usuarioLogin, string pessoaNome, string? usuarioApelido, string? pessoaDocCpfCnpj, DateTime? usuarioCreatedOn, string? pessoaDocIdentidade, string? pessoaDocIdentidadeEmissor)
        {
            UsuarioLogin = usuarioLogin;
            PessoaNome = pessoaNome;
            UsuarioApelido = usuarioApelido;
            PessoaDocCpfCnpj = pessoaDocCpfCnpj;
            UsuarioCreatedOn = usuarioCreatedOn;
            PessoaDocIdentidade = pessoaDocIdentidade;
            PessoaDocIdentidadeEmissor = pessoaDocIdentidadeEmissor;
        }

        public string UsuarioLogin { get; set; }
        public string PessoaNome { get; set; }
        public string? UsuarioApelido { get; set; }
        public string? PessoaDocCpfCnpj { get; set; }
        public DateTime? UsuarioCreatedOn { get; set; }
        public string? PessoaDocIdentidade { get; set; }
        public string? PessoaDocIdentidadeEmissor { get; set; }

    }
}
