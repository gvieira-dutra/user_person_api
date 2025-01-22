using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

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
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO usuario ( " +
                                    "pessoa_id, tenant_id, created_by, " +
                                    "created_on, last_modified_by, " +
                                    "login, senha ) " +
                                  "VALUES ( " +
                                    "@pessoaId, @tenantId, @createdBy," +
                                    "@createdOn, @lastModifiedBy, " +
                                    "@login, @senha) ";

            command.Parameters.Add(new NpgsqlParameter("@pessoaId", novoUsuario.PessoaId));
            command.Parameters.Add(new NpgsqlParameter("@tenantId", novoUsuario.TenantId));
            command.Parameters.Add(new NpgsqlParameter("@createdBy", novoUsuario.CreatedBy));
            command.Parameters.Add(new NpgsqlParameter("@createdOn", DateTime.Now));
            command.Parameters.Add(new NpgsqlParameter("@lastModifiedBy", novoUsuario.CreatedBy));
            command.Parameters.Add(new NpgsqlParameter("@login", novoUsuario.Login));
            command.Parameters.Add(new NpgsqlParameter("@senha", Usuario.HashSenha(novoUsuario.Senha)));

            command.ExecuteNonQuery();
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
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT u.senha, p.name, p.id, u.apelido " +
                              "FROM usuario u " +
                              "INNER JOIN pessoa p " +
                              "ON u.login = @Login " +
                              "AND u.senha = @Senha " +
                              "AND u.ativo = true ";

        command.Parameters.Add(new NpgsqlParameter("@Login", usuarioLogin.Login));
        command.Parameters.Add(new NpgsqlParameter("@Senha", Usuario.HashSenha(usuarioLogin.Senha)));

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new UsuarioLoginDTO()
            {
                Nome = reader["name"]?.ToString() ?? string.Empty,
                Apelido = reader["apelido"]?.ToString() ?? string.Empty,
                Id = (Guid)reader["id"],
                Senha = reader["senha"]?.ToString() ?? string.Empty,
                Mensagem = ("Login e senha corretos, agora pode trabalhar com token.")
            };

        }

        return null;
    }

    public UsuarioPessoaDto GetUsuarioPorId(Guid id)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT u.login, p.name, u.apelido " +
                              "FROM usuario u " +
                              "INNER JOIN pessoa p " +
                              "ON p.id = u.pessoa_id " +
                              "WHERE u.id = @Id";

        command.Parameters.Add(new NpgsqlParameter("@Id", id));

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return (new UsuarioPessoaDto()
            {

                Login = reader["login"]?.ToString() ?? string.Empty,
                Nome = reader["name"]?.ToString() ?? string.Empty,
                Apelido = reader["apelido"]?.ToString() ?? string.Empty
            });
        }

        return null;
    }
    
    public List<Usuario> GetUsuarioPorApelido(string apelido)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM usuario " +
                              "WHERE LOWER(apelido) " +
                              "LIKE LOWER('%' || @apelido || '%')";

        command.Parameters.Add(new NpgsqlParameter("@apelido", apelido));

        using var reader = command.ExecuteReader();
        var usuarios = new List<Usuario>();

        while (reader.Read())
        {
            usuarios.Add(new Usuario
            {
                Id = (Guid)reader["id"],
                PessoaId = (Guid)reader["pessoa_id"],
                TenantId = (Guid)reader["tenant_id"],
                Apelido = reader["apelido"]?.ToString() ?? string.Empty,
                CodigoAlternativo = reader["codigo_alternativo"]?.ToString() ?? string.Empty,
                CreatedBy = (Guid)reader["created_by"],
                CreatedOn = reader["created_on"] as DateTime? ?? default(DateTime),
                LastModifiedBy = (Guid)reader["last_modified_by"],
                LastModifiedOn = reader["last_modified_on"] as DateTime? ?? default(DateTime),
                DeletedBy = reader["deleted_by"] as Guid?,
                DeletedOn = reader["deleted_on"] as DateTime? ?? default(DateTime),
                Ativo = (bool)reader["ativo"]
            });

        }

        if (usuarios.Count > 0)
        {
            return usuarios;
        }
            return null;
    }

    public UsuarioDocDTO GetUsuarioPorCpfCnpj(string cpfCnpj)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT u.apelido, p.name, p.tenant_id " +
                              "FROM usuario u " +
                              "INNER JOIN pessoa p " +
                              "ON u.pessoa_id = p.id " +
                              "WHERE p.doc_cpf_cnpj = @CpfCnpj " +
                              "AND u.ativo=true";
        command.Parameters.Add(new NpgsqlParameter("@CpfCnpj", cpfCnpj));

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return (new UsuarioDocDTO()
            {
                Nome = reader["name"].ToString() ?? string.Empty,
                Apelido = reader["apelido"].ToString() ?? string.Empty,
                TenantId = reader["tenant_id"].ToString() ?? string.Empty
            });
        }
        return null;
    }

    public List<UsuarioDocDTO> GetUsuarioPorIdentidade(string identidade)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT p.doc_identidade, u.apelido, p.name, p.tenant_id " +
                              "FROM usuario u " +
                              "INNER JOIN pessoa p " +
                              "ON u.pessoa_id = p.id " +
                              "WHERE p.doc_identidade = @Identidade";
        command.Parameters.Add(new NpgsqlParameter("@Identidade", identidade));

        using var reader = command.ExecuteReader();

        var usuarios = new List<UsuarioDocDTO>();

        while (reader.Read())
        {
            usuarios.Add(new UsuarioDocDTO()
            {
                
                Apelido = reader["apelido"].ToString() ?? string.Empty,
                Nome = reader["name"].ToString() ?? string.Empty,
                TenantId = reader["tenant_id"].ToString() ?? string.Empty,
                DocIdentidade = reader["doc_identidade"].ToString() ?? string.Empty
            });
        }

        if (usuarios.Count > 0)
        {
            return usuarios;
        }
        else
        {
            return null;
        }
    }

    public void Put(UsuarioEdit usuario)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText = "UPDATE usuario " +
                              "SET apelido = @Apelido, " +
                                "last_modified_by = @ModifierId, " +
                                "last_modified_on = @LastModifiedOn " +
                              "WHERE id = @Id";

        command.Parameters.Add(new NpgsqlParameter("@Apelido", usuario.Apelido));
        command.Parameters.Add(new NpgsqlParameter("@Id", usuario.Id));
        command.Parameters.Add(new NpgsqlParameter("@ModifierId", usuario.ModifierId));
        command.Parameters.Add(new NpgsqlParameter("@LastModifiedOn", usuario.LastModifiedOn));

        command.ExecuteNonQuery();
    }
    
    public void Delete(UsuarioDeleteModel deleteInfo)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE usuario " +
                              "SET ativo = false, " +
                                "deleted_by = @DeletedBy, " +
                                "deleted_on = @DeletedOn " +
                              "WHERE id = @Id";

        command.Parameters.Add(new NpgsqlParameter("@DeletedBy", deleteInfo.DeletedBy));
        command.Parameters.Add(new NpgsqlParameter("@DeletedOn", deleteInfo.DeletedOn));
        command.Parameters.Add(new NpgsqlParameter("@Id", deleteInfo.Id));

        command.ExecuteNonQuery();

    }
}
