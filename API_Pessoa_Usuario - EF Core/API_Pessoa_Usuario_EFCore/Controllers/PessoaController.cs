using API_Pessoa_Usuario_EFCore.Domain;
using API_Pessoa_Usuario_EFCore.DTO;
using API_Pessoa_Usuario_EFCore.Repository.PessoaRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_Pessoa_Usuario_EFCore.Controllers;

[Route("api/v1/pessoa")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaRepository _pessoaRepo;

    public PessoaController(IPessoaRepository pessoaRepository)
    {
        _pessoaRepo = pessoaRepository;
    }

    [HttpPost]
    public ActionResult Post([FromBody] Pessoa pessoa)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Erros de validação ocorreram e essa pessoa não pode ser criada!"
            });
        }

        if (_pessoaRepo.Post(pessoa))
        {
            return Ok();
        }
        else
        {
            return StatusCode(500, "Nao foi possivel adicionar essa pessoa");
        }

    }

    [HttpGet("por-id/{id:guid}")]
    public ActionResult<Pessoa> GetByID(Guid id)
    {
        var pessoa = _pessoaRepo.GetPessoaPorId(id);

        if (pessoa == null)
        {
            return NotFound("Não encontramos essa pessoa!");
        }

        return Ok(pessoa);
    }

    [HttpGet("por-nome/{nome}")]
    public ActionResult<List<Pessoa>> GetByName(string nome)
    {
        var pessoas = _pessoaRepo.GetPessoaPorNome(nome);

        if (pessoas == null)
        {
            return NotFound("Ninguém com esse nome foi encontrado");
        }

        return Ok(pessoas);
    }

    [HttpGet("por-cpf/{cpfCnpj}")]
    public ActionResult<List<Pessoa>> GetByCpf(string cpfCnpj)
    {
        var pessoas = _pessoaRepo.GetPorCpfCnpj(cpfCnpj);

        if (pessoas == null)
        {
            return NotFound("Ninguém com esse CPF ou CNPJ foi encontrado");
        }

        return Ok(pessoas);
    }

    [HttpGet("por-identidade/{identidade}")]
    public ActionResult<List<Pessoa>> GetPorIdentidade(string identidade)
    {
        var pessoas = _pessoaRepo.GetPorIdentidade(identidade);

        if (pessoas.Count() == 0) 
            return NotFound("Não encontramos ninguém com esse número de identidade");

        return Ok(pessoas);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, [FromBody] PessoaEditarDTO pessoaEditar)
    {
        if (id != pessoaEditar.Id) return BadRequest("1 Erros de validação ocorreram e essa pessoa não pode ser criada!.");

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "2 Erros de validação ocorreram e essa pessoa não pode ser criada!"
            });
        }

        _pessoaRepo.PutPessoa(pessoaEditar);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id, [FromBody] PessoaDeleteDTO pessoaDel)
    {
        if (id != pessoaDel.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Houve um problem ao tentar deletar o usuário, tente novamente!"
            });
        }


        _pessoaRepo.DeletePessoa(pessoaDel);

        return NoContent();
    }
}
