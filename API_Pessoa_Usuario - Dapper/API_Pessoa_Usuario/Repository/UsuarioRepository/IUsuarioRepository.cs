using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API_Pessoa_Usuario.Repository.UsuarioRepository;

public interface IUsuarioRepository
{
    public bool Post(Usuario novoUsuario);
    public UsuarioLoginDTO PostLogin(UsuarioLogin usuarioLogin);
    public UsuarioPessoaDto GetUsuarioPorId(Guid id);
    public IEnumerable<Usuario> GetUsuarioPorApelido(string apelido);
    public UsuarioDocDTO GetUsuarioPorCpfCnpj(string cpfCnpj);
    public List<UsuarioDocDTO> GetUsuarioPorIdentidade(string identidade);
    public void Put(UsuarioEdit usuario);
    public void Delete(UsuarioDeleteModel deleteInfo);



}
