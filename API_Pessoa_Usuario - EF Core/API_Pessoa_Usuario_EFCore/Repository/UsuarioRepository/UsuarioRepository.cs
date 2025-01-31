using API_Pessoa_Usuario_Dapper;
using API_Pessoa_Usuario_EFCore.Data;
using API_Pessoa_Usuario_EFCore.Domain;
using API_Pessoa_Usuario_EFCore.DTO;
using API_Pessoa_Usuario_EFCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace API_Pessoa_Usuario_EFCore.Repository.UsuarioRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Post(Usuario novoUsuario)
        {
            try
            {
                novoUsuario.Senha = PasswordHash.PwHash(novoUsuario.Senha);

                _context.Add(novoUsuario);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UsuarioLoginDTO PostLogin(UsuarioLoginDTO usuarioLogin) => _context.Usuarios
                .Where(p => p.Login == usuarioLogin.Login
                            && p.Senha == PasswordHash.PwHash(usuarioLogin.Senha))
                .Include(p => p.PessoaPertencente)
                .Select(p => new UsuarioLoginDTO(
                    p.Login,
                    p.Senha,
                    p.Id,
                    p.PessoaPertencente != null ? p.PessoaPertencente.Nome : string.Empty,
                    p.PessoaPertencente != null ? p.PessoaPertencente.Site : string.Empty,
                    p.Apelido,
                    p.CreatedOn
                    ))
                .FirstOrDefault();

        public UsuarioPessoaDTO GetUsuarioPorId(Guid id)
        {
            // Projection
            var usr = _context.Usuarios
                .Where(p => p.Id == id)
                .Select(p => new UsuarioPessoaDTO(
                    p.Login,
                    p.PessoaPertencente != null ? p.PessoaPertencente.Nome : string.Empty,
                    p.Apelido,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocCpfCnpj : string.Empty,
                    p.CreatedOn,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocIdentidade : string.Empty,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocIdentidadeEmissor : string.Empty
                ))
                .FirstOrDefault();

            if (usr == null) return null;

            return usr;
        }

        public UsuarioPessoaDTO GetUsuarioPorCpfCnpj(string cpfCnpj)
        {
            // Projection
            var usr = _context.Usuarios
                .Where(p => p.PessoaPertencente != null && p.PessoaPertencente.DocCpfCnpj == cpfCnpj)
                .Select(p => new UsuarioPessoaDTO(
                    p.Login,
                    p.PessoaPertencente != null ? p.PessoaPertencente.Nome : string.Empty,
                    p.Apelido,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocCpfCnpj : string.Empty,
                    p.CreatedOn,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocIdentidade : string.Empty,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocIdentidadeEmissor : string.Empty
                ))
                .FirstOrDefault();

            if (usr == null) return null;

            return usr;
        }

        public IEnumerable<UsuarioPessoaDTO> GetUsuarioPorApelido(string apelido)
        {
            // Split Query
            var usuario = _context.Usuarios
                .Include(p => p.PessoaPertencente)
                .Where(p => p.Apelido.Contains(apelido))
                .Select(p => new UsuarioPessoaDTO(
                    p.Login,
                    p.PessoaPertencente != null ? p.PessoaPertencente.Nome : string.Empty,
                    p.Apelido,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocCpfCnpj : string.Empty,
                    p.CreatedOn,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocIdentidade : string.Empty,
                    p.PessoaPertencente != null ? p.PessoaPertencente.DocIdentidadeEmissor : string.Empty
                ))
                .AsSplitQuery()
                .ToList();

            return usuario;
        }

        public List<UsuarioPessoaDTO> GetUsuarioPorIdentidade(string identidade)
        {
            // Split Query
            var usuario = _context.Usuarios
             .Include(p => p.PessoaPertencente)
             .Where(p => p.PessoaPertencente != null && p.PessoaPertencente.DocIdentidade == identidade)
             .Select(p => new UsuarioPessoaDTO(
                 p.Login,
                 p.PessoaPertencente != null ? p.PessoaPertencente.Nome : string.Empty,
                 p.Apelido,
                 p.PessoaPertencente != null ? p.PessoaPertencente.DocCpfCnpj : string.Empty,
                 p.CreatedOn,
                 p.PessoaPertencente != null ? p.PessoaPertencente.DocIdentidade : string.Empty,
                 p.PessoaPertencente != null ? p.PessoaPertencente.DocIdentidadeEmissor : string.Empty
             ))
             .AsSplitQuery()
             .ToList();

            return usuario;
        }

        public void Put(UsuarioEditDTO usuario)
        {
            var usr = new Usuario
            {
                Id = usuario.Id
            };

            var editUsuario = new
            {
                usuario.Apelido,
                usuario.LastModifiedBy,
                LastModifiedOn = DateTime.UtcNow
            };

            _context.Attach(usr);
            _context.Entry(usr).CurrentValues.SetValues(editUsuario);

            _context.SaveChanges();
        }

        public void Delete(UsuarioDeleteDTO usuario)
        {
            var usr = new Usuario
            {
                Id = usuario.Id
            };

            var usuarioDelte = new
            {
                Ativo = false,
                usuario.DeletedBy,
                DeletedOn = DateTime.UtcNow
            };

            _context.Attach(usr);
            _context.Entry(usr).CurrentValues.SetValues(usuarioDelte);

            _context.SaveChanges();
        }
    }
}
