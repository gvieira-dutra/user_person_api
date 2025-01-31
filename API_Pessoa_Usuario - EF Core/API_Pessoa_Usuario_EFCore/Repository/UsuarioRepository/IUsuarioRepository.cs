using API_Pessoa_Usuario_Dapper;
using API_Pessoa_Usuario_EFCore.Domain;
using API_Pessoa_Usuario_EFCore.DTO;

namespace API_Pessoa_Usuario_EFCore.Repository.UsuarioRepository;

public interface IUsuarioRepository
{
    //TO DO

    // Split Query
    // Manage connexion state
    // Lazy Loading
    // Eager Loading
    // Explicit Loading
    // Projection

    public bool Post(Usuario novoUsuario);
    public UsuarioLoginDTO PostLogin(UsuarioLoginDTO usuarioLogin);
    public UsuarioPessoaDTO GetUsuarioPorId(Guid id);
    public IEnumerable<UsuarioPessoaDTO> GetUsuarioPorApelido(string apelido);
    public UsuarioPessoaDTO GetUsuarioPorCpfCnpj(string cpfCnpj);
    public List<UsuarioPessoaDTO> GetUsuarioPorIdentidade(string identidade);
    public void Put(UsuarioEditDTO usuario);
    public void Delete(UsuarioDeleteDTO usuario);
}
