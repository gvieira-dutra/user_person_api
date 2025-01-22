// Ignore Spelling: Pessoa Por Cnpj Cpf

using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API_Pessoa_Usuario.Repository.PessoaRepository;

public class PessoaRepository : IPessoaRepository
{
    private readonly DatabaseHelper _dbHelper;

    public PessoaRepository(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public bool Post(Pessoa pessoa)
    {
        try
        {
            using var connection = _dbHelper.GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO pessoa " +
                "(id_alternativo, tenant_id, name, fis_jur, " +
                "doc_cpf_cnpj, created_by, created_on, last_modified_by, last_modified_on" +
                ") VALUES ( @IdAlternativo, @TenantId, @Name, @FisJur," +
                "@DocCpfCnpj, @CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn ) ";
            command.Parameters.Add(new NpgsqlParameter("@IdAlternativo", pessoa.IdAlternativo));
            command.Parameters.Add(new NpgsqlParameter("@TenantId", pessoa.TenantId));
            command.Parameters.Add(new NpgsqlParameter("@Name", pessoa.Nome));
            command.Parameters.Add(new NpgsqlParameter("@FisJur", pessoa.FisJur));
            command.Parameters.Add(new NpgsqlParameter("@DocCpfCnpj", pessoa.DocCpfCnpj));
            command.Parameters.Add(new NpgsqlParameter("@CreatedBy", pessoa.CreatedBy));
            command.Parameters.Add(new NpgsqlParameter("@CreatedOn", pessoa.CreatedOn));
            command.Parameters.Add(new NpgsqlParameter("@LastModifiedBy", pessoa.LastModifiedBy));
            command.Parameters.Add(new NpgsqlParameter("@LastModifiedOn", pessoa.LastModifiedOn));

            command.ExecuteNonQuery();

            return true;
        }
        catch
        {
            return false;
        }


    }

    public Pessoa GetPessoaPorId(Guid id)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM pessoa WHERE id = @Id AND ativo=true";
        command.Parameters.Add(new NpgsqlParameter("@Id", id));

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Pessoa
            {
                Id = (Guid)reader["Id"],
                IdAlternativo = reader["id_alternativo"]?.ToString() ?? string.Empty,
                TenantId = reader["tenant_id"]?.ToString() ?? string.Empty,
                Nome = reader["name"]?.ToString() ?? string.Empty,
                ApelidoFantasia = reader["apelido_fantasia"]?.ToString() ?? string.Empty,
                FisJur = reader["fis_jur"] as int? ?? 0,
                DocCpfCnpj = reader["doc_cpf_cnpj"]?.ToString() ?? string.Empty,
                DocIdentidade = reader["doc_identidade"]?.ToString() ?? string.Empty,
                DocIdentidadeEmissor = reader["doc_identidade_emissor"]?.ToString() ?? string.Empty,
                DocInscricaoEstadual = reader["doc_inscricao_estadual"]?.ToString() ?? "ISENTO",
                DocInscricaoMunicipal = reader["doc_inscricao_municipal"]?.ToString() ?? string.Empty,
                DocInscricaoProdutorRural = reader["doc_inscricao_produtor_rural"]?.ToString() ?? string.Empty,
                DocInscSuframa = reader["doc_insc_suframa"]?.ToString() ?? string.Empty,
                NascimentoFundacao = reader["nascimento_fundacao"] as DateTime? ?? default,
                EstadoCivil = reader["estado_civil"] as int? ?? 0,
                NomeConjuge = reader["nome_conjuge"]?.ToString() ?? string.Empty,
                NomePai = reader["nome_pai"]?.ToString() ?? string.Empty,
                NomeMae = reader["nome_mae"]?.ToString() ?? string.Empty,
                Genero = reader["genero"] as int? ?? -1,
                Site = reader["site"]?.ToString() ?? string.Empty,
                Observacao = reader["observacao"]?.ToString() ?? string.Empty,
                Ativo = reader["ativo"] as bool? ?? true,
                CreatedBy = (Guid)reader["created_by"],
                CreatedOn = reader["created_on"] as DateTime? ?? default,
                LastModifiedBy = (Guid)reader["last_modified_by"],
                LastModifiedOn = reader["last_modified_on"] as DateTime? ?? default,
                DeletedOn = reader["deleted_on"] as DateTime?,
                DeletedBy = reader["deleted_by"] as Guid?,
            };

        }
        return null;
    }

    public List<Pessoa> GetPessoaPorNome(string nome)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM pessoa WHERE LOWER(name) LIKE LOWER('%' || @nome || '%') AND ativo=true";
        command.Parameters.Add(new NpgsqlParameter("@nome", nome));

        using var reader = command.ExecuteReader();
        var pessoas = new List<Pessoa>();

        while (reader.Read())
        {
            pessoas.Add(new Pessoa
            {
                Id = (Guid)reader["Id"],
                Nome = reader["name"]?.ToString() ?? string.Empty,
                IdAlternativo = reader["id_alternativo"]?.ToString() ?? string.Empty,
                TenantId = reader["tenant_id"]?.ToString() ?? string.Empty,
                ApelidoFantasia = reader["apelido_fantasia"]?.ToString() ?? string.Empty,
                FisJur = reader["fis_jur"] as int? ?? 0,
                DocCpfCnpj = reader["doc_cpf_cnpj"]?.ToString() ?? string.Empty,
                DocIdentidade = reader["doc_identidade"]?.ToString() ?? string.Empty,
                DocIdentidadeEmissor = reader["doc_identidade_emissor"]?.ToString() ?? string.Empty,
                DocInscricaoEstadual = reader["doc_inscricao_estadual"]?.ToString() ?? "ISENTO",
                DocInscricaoMunicipal = reader["doc_inscricao_municipal"]?.ToString() ?? string.Empty,
                DocInscricaoProdutorRural = reader["doc_inscricao_produtor_rural"]?.ToString() ?? string.Empty,
                DocInscSuframa = reader["doc_insc_suframa"]?.ToString() ?? string.Empty,
                NascimentoFundacao = reader["nascimento_fundacao"] as DateTime? ?? default,
                EstadoCivil = reader["estado_civil"] as int? ?? 0,
                NomeConjuge = reader["nome_conjuge"]?.ToString() ?? string.Empty,
                NomePai = reader["nome_pai"]?.ToString() ?? string.Empty,
                NomeMae = reader["nome_mae"]?.ToString() ?? string.Empty,
                Genero = reader["genero"] as int? ?? -1,
                Site = reader["site"]?.ToString() ?? string.Empty,
                Observacao = reader["observacao"]?.ToString() ?? string.Empty,
                Ativo = reader["ativo"] as bool? ?? true,
                CreatedBy = (Guid)reader["created_by"],
                CreatedOn = reader["created_on"] as DateTime? ?? default,
                LastModifiedBy = (Guid)reader["last_modified_by"],
                LastModifiedOn = reader["last_modified_on"] as DateTime? ?? default,
                DeletedOn = reader["deleted_on"] as DateTime?,
                DeletedBy = reader["deleted_by"] as Guid?,
            });

        }

        if (pessoas.Count > 0)
        {
            return pessoas;
        }
        else
        {
            return null;
        }

    }

    public PessoaPorDocDTO GetPorCpfCnpj(string cpfCnpj)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT name, apelido_fantasia, doc_cpf_cnpj " +
                              "FROM pessoa " +
                              "WHERE doc_cpf_cnpj = @docCpfCnpj " +
                              "AND ativo=true";
        command.Parameters.Add(new NpgsqlParameter("@docCpfCnpj", cpfCnpj));

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new PessoaPorDocDTO()
            {
                Nome = reader["name"].ToString() ?? string.Empty,
                ApelidoFantasia = reader["apelido_fantasia"].ToString() ?? string.Empty,
                DocCpfCnpj = reader["doc_cpf_cnpj"].ToString() ?? string.Empty
            };
        }

        return null;
    }

    public List<PessoaPorDocDTO> GetPorIdentidade(string identidade)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT name, doc_cpf_cnpj, doc_identidade, doc_identidade_emissor " +
                              "FROM pessoa " +
                              "WHERE doc_identidade = @DocIdentidade " +
                              "AND ativo = true";

        command.Parameters.Add(new NpgsqlParameter("@DocIdentidade", identidade));

        using var reader = command.ExecuteReader();

        var pessoas = new List<PessoaPorDocDTO>();

        while (reader.Read())
        {
            pessoas.Add(new PessoaPorDocDTO()
            {
                Nome = reader["name"].ToString() ?? string.Empty,
                DocCpfCnpj = reader["doc_cpf_cnpj"].ToString() ?? string.Empty,
                DocIdentidade = reader["doc_identidade"].ToString() ?? string.Empty,
                DocIdentidadeEmissor = reader["doc_identidade_emissor"].ToString() ?? string.Empty,
            });
        }

        if (pessoas.Count > 0)
        {
            return pessoas;
        }
        else
        {
            return null;
        }
    }

    public void PutPessoa(PessoaEdit pessoa)
    {
        using var connection = _dbHelper.GetConnection(); ;
        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText = "UPDATE pessoa SET name=@NewName WHERE id=@Id ";
        command.Parameters.Add(new NpgsqlParameter("@NewName", pessoa.Nome));
        command.Parameters.Add(new NpgsqlParameter("@Id", pessoa.Id));

        command.ExecuteNonQuery();
    }

    public void DeletePessoa(PessoaDeleteModel deleteModel)
    {
        var connection = _dbHelper.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "UPDATE pessoa SET ativo=false, deleted_by=@DeletedById, deleted_on=@DeletedOn WHERE id=@Id";
        command.Parameters.Add(new NpgsqlParameter("@Id", deleteModel.Id));
        command.Parameters.Add(new NpgsqlParameter("@DeletedById", deleteModel.DeletedBy));
        command.Parameters.Add(new NpgsqlParameter("@DeletedOn", DateTime.Now));

        command.ExecuteNonQuery();
    }
}
