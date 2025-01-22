// Ignore Spelling: Pessoa Por Cnpj Cpf

using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
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
            pessoa.Id = Guid.NewGuid(); 

            var query = @"
            INSERT INTO pessoa (
                ""id"", ""id_alternativo"", ""tenant_id"", ""name"", ""fis_jur"", 
                ""doc_cpf_cnpj"", ""created_on"", ""created_by"", ""last_modified_by"", ""last_modified_on""
            )
            VALUES (
                @Id, @Id_Alternativo, @Tenant_Id, @Name, @Fis_Jur, 
                @Doc_Cpf_Cnpj, @Created_On, @Created_By, @Last_Modified_By, @Last_Modified_On
            )";

            var result = connection.Execute(query, pessoa);



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

        var pessoa = connection.QuerySingleOrDefault<Pessoa>(
        "SELECT * FROM pessoa WHERE id = @Id AND ativo=@Ativo",
        new {Id = id, Ativo = true});

        if (pessoa == null)
        {
            return null;
        }
        return pessoa;
    }

    public IEnumerable<Pessoa> GetPessoaPorNome(string nome) 
    {
        using var connection = _dbHelper.GetConnection();

        var pessoas = connection.Query<Pessoa>(
        "SELECT * FROM pessoa WHERE LOWER(name) LIKE LOWER('%' || @Nome || '%') AND ativo=true",
        new { Nome = nome});

        if (pessoas?.Count() > 0) return pessoas;
        else return null;

    }

    public PessoaPorDocDTO GetPorCpfCnpj(string cpfCnpj)
    {
        using var connection = _dbHelper.GetConnection();


        var pessoa = connection.QuerySingleOrDefault(
        "SELECT name, apelido_fantasia, doc_cpf_cnpj " +
                              "FROM pessoa " +
                              "WHERE doc_cpf_cnpj = @DocCpfCnpj " +
                              "AND ativo=true",
        new { DocCpfCnpj = cpfCnpj});

        if (pessoa == null)
        {
            return null;
        }
        else
        {
            return (new PessoaPorDocDTO()
            {
                Nome = pessoa.name,
                Apelido_Fantasia = pessoa.apelido_fantasia,
                Doc_Cpf_Cnpj = pessoa.doc_cpf_cnpj
            });
        }
    }

    public IEnumerable<PessoaPorDocDTO> GetPorIdentidade(string identidade)
    {
        using var connection = _dbHelper.GetConnection();

        var pessoas = connection.Query<Pessoa>(
            "SELECT name, doc_cpf_cnpj, doc_identidade, doc_identidade_emissor " +
            "FROM pessoa " +
            "WHERE doc_identidade = @DocIdentidade " +
            "AND ativo = true",
            new { DocIdentidade = identidade });

        if (!pessoas.Any())
        {
            return null; 
        }

        var pessoasPerDocDTO = pessoas.Select(p => new PessoaPorDocDTO
        {
            Nome = p.Name,
            Doc_Cpf_Cnpj = p.Doc_Cpf_Cnpj,
            Doc_Identidade = p.Doc_Identidade,
            Doc_Identidade_Emissor = p.Doc_Identidade_Emissor
        });

        return pessoasPerDocDTO;
    }

    public void PutPessoa(Pessoa pessoa)
    {
        using var connection = _dbHelper.GetConnection(); ;

        var query = "UPDATE pessoa SET name=@NewName WHERE id=@Id";

        connection.Execute(query, new { NewName = pessoa.Name, Id = pessoa.Id });
    }

    public void DeletePessoa(PessoaDeleteModel deleteModel)
    {
        var connection = _dbHelper.GetConnection();

        var query = "UPDATE pessoa SET ativo=false, deleted_by=@DeletedById, deleted_on=@DeletedOn WHERE id=@Id";

        connection.Execute(query, new { Id = deleteModel.Id, DeletedById = deleteModel.DeletedBy, DeletedOn = DateTime.Now });
    }
}
