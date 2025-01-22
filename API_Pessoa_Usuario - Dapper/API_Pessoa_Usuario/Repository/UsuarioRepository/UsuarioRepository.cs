using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;
using API_Pessoa_Usuario_Dapper.Utilities;
using Dapper;

namespace API_Pessoa_Usuario.Repository.UsuarioRepository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DatabaseHelper _dbHelper;

    public UsuarioRepository(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }
   
    public bool Post(Usuario novoUsuario)
    {
        try
        {
            using var connection = _dbHelper.GetConnection();

            var query = "INSERT INTO usuario ( " +
                            "pessoa_id, tenant_id, created_by, " +
                            "created_on, last_modified_by, " +
                            "login, senha ) " +
                        "VALUES ( " +
                            "@PessoaId, @TenantId, @CreatedBy," +
                            "@CreatedOn, @LastModifiedBy, " +
                            "@Login, @Senha) ";

            connection.Execute(query,
                new { novoUsuario.PessoaId, novoUsuario.TenantId, novoUsuario.CreatedBy,
                    novoUsuario.CreatedOn, LastModifiedBy = novoUsuario.CreatedBy,
                    novoUsuario.Login, Senha = PasswordHash.PwHash(novoUsuario.Senha)});

        }
        catch
        {
            return false;
        }

        return true;
    }

    public UsuarioLoginDTO PostLogin(UsuarioLogin usuarioLogin)
    {
        using var connection = _dbHelper.GetConnection();
     
        var query = "SELECT u.senha,  u.apelido, p.name, p.id " +
                              "FROM usuario u " +
                              "INNER JOIN pessoa p " +
                              "ON u.login = @Login " +
                              "AND u.senha = @Senha " +
                              "AND u.ativo = true ";

        var usuario = connection.Query<Usuario, Pessoa, Usuario>(
            query,
            (usuario, pessoa) =>
            {
                usuario.PessoaPertencente = pessoa;
                return usuario;
            },
            (new {usuarioLogin.Login, Senha = PasswordHash.PwHash(usuarioLogin.Senha) }),
            splitOn: "name"           
            ).FirstOrDefault();

        if (usuario == null) return null;

        return new UsuarioLoginDTO()
        {
            Senha = usuario.Senha,
            Apelido = usuario.Apelido,
            Nome = usuario.PessoaPertencente.Name,
            Id = usuario.PessoaPertencente.Id,
            Mensagem = "Login efetuado com sucesso, pode trabalhar com o token"
        };
    }

    public UsuarioPessoaDto GetUsuarioPorId(Guid id)
    {
        using var connection = _dbHelper.GetConnection();

        var query = "SELECT u.login, p.name, u.apelido " +
                              "FROM usuario u " +
                              "INNER JOIN pessoa p " +
                              "ON p.id = u.pessoa_id " +
                              "WHERE u.id = @Id";
        var usuarioPessoa = connection.Query<Usuario, Pessoa, Usuario>(
            query,
            (usuario, pessoa) =>
            {
                usuario.PessoaPertencente = pessoa;
                return usuario;
            },
            new { Id = id},
            splitOn: "name"
            ).FirstOrDefault();

        if (usuarioPessoa == null) return null;

        return new UsuarioPessoaDto()
        {
            Nome = usuarioPessoa.PessoaPertencente.Name,
            Apelido = usuarioPessoa.Apelido,
            Login = usuarioPessoa.Login
        };
        

    }
    
    public IEnumerable<Usuario> GetUsuarioPorApelido(string apelido)
    {
        using var connection = _dbHelper.GetConnection();


        var usuarios = connection.Query<Usuario>(
        "SELECT * FROM usuario " +
                              "WHERE LOWER(apelido) " +
                              "LIKE LOWER('%' || @Apelido || '%')",
        new { Apelido = apelido });

        if (usuarios.Any())
        {
            return usuarios;
        }
            return null;
    }

    public UsuarioDocDTO GetUsuarioPorCpfCnpj(string cpfCnpj)
    {
        using var connection = _dbHelper.GetConnection();

        var usuario = connection.Query<Usuario, Pessoa, Usuario>( "SELECT u.apelido, p.name, p.tenant_id " +
                              "FROM usuario u " +
                              "INNER JOIN pessoa p " +
                              "ON u.pessoa_id = p.id " +
                              "WHERE p.doc_cpf_cnpj = @CpfCnpj " +
                              "AND u.ativo=true",
                              (usuario, pessoa) =>
                              {
                                  usuario.PessoaPertencente = pessoa;
                                  return usuario;
                              },
                       new {CpfCnpj = cpfCnpj},
                       splitOn: "name")
                       .FirstOrDefault();

        if(usuario != null) 
        {

            return (new UsuarioDocDTO()
            {
                Nome = usuario.PessoaPertencente.Name,
                Apelido = usuario.Apelido,
                TenantId = usuario.PessoaPertencente.Tenant_Id.ToString()
            });
        }

        return null;
    }

    public List<UsuarioDocDTO> GetUsuarioPorIdentidade(string identidade)
    {
        using var connection = _dbHelper.GetConnection();

        var query = "SELECT u.apelido, p.name, p.tenant_id, p.doc_identidade " +
                              "FROM usuario u " +
                              "INNER JOIN pessoa p " +
                              "ON u.pessoa_id = p.id " +
                              "WHERE p.doc_identidade = @Identidade";

       var usuarios = connection.Query<Usuario, Pessoa, Usuario>(
           query,
           (usuario, pessoa) =>
           {
               usuario.PessoaPertencente = pessoa;
               return usuario;
           },
           (new {Identidade = identidade}),
           splitOn: "name");

        if (usuarios.Count() == 0) return null;

        var usuariosDto = new List<UsuarioDocDTO>();

        foreach (var usuario in usuarios)
        {
            usuariosDto.Add(new UsuarioDocDTO()
            {
                Apelido = usuario.Apelido,
                Nome = usuario.PessoaPertencente.Name,
                TenantId = usuario.PessoaPertencente.Tenant_Id,
                DocIdentidade = usuario.PessoaPertencente.Doc_Identidade

            });
        }

        return usuariosDto;
    }

    public void Put(UsuarioEdit usuario)
    {
        using var connection = _dbHelper.GetConnection();
        var query = "UPDATE usuario " +
                              "SET apelido = @Apelido, " +
                                "last_modified_by = @Modifier_Id, " +
                                "last_modified_on = @Last_Modified_On " +
                              "WHERE id = @Id";

        connection.Execute(query,
            new { Apelido = usuario.Apelido, Modifier_Id = usuario.ModifierId, Last_Modified_On = DateTime.UtcNow,  Id = usuario.Id });
    }
    
    public void Delete(UsuarioDeleteModel deleteInfo)
    {
        using var connection = _dbHelper.GetConnection();

        var query = "UPDATE usuario " +
                              "SET ativo = false, " +
                                "deleted_by = @DeletedBy, " +
                                "deleted_on = @DeletedOn " +
                              "WHERE id = @Id";

        connection.Execute(query,
            new { deleteInfo.DeletedBy, DeletedOn = DateTime.Now, deleteInfo.Id });
    }
}
