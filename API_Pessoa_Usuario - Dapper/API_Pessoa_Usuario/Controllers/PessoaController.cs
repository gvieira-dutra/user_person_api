using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;
using API_Pessoa_Usuario.Repository.PessoaRepository;
using Microsoft.AspNetCore.Mvc;

namespace API_Pessoa_Usuario.Controllers;

[ApiController]
[Route("api/vi/pessoa/dapper")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaRepository _pessoaRepo;

    public PessoaController(IPessoaRepository repo)
    {
        _pessoaRepo = repo;
    }

    [HttpPost]
    public ActionResult<Pessoa> Post([FromBody] Pessoa pessoa)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });
        }

        if (_pessoaRepo.Post(pessoa))
        {
            return CreatedAtAction(nameof(GetPessoaPorId), new {id = pessoa.Id}, pessoa);
        }
        else
        {
            return StatusCode(500, "Não foi possível adicionar essa pessoa");
        }

    }

    [HttpGet("por-id/{id:Guid}")]
    public IActionResult GetPessoaPorId(Guid id)
    {
        var pessoa = _pessoaRepo.GetPessoaPorId(id);

        if (pessoa == null)
        {
            return NotFound("Essa pessoa não foi encontrada. Tente novamente.");
        }

        return Ok(pessoa);
    }

    [HttpGet("por-nome/{nome}")]
    public ActionResult<Pessoa> GetPessoaPorNome(string nome)
    {
        var pessoas = _pessoaRepo.GetPessoaPorNome(nome);

        if (pessoas != null)
        {
            return Ok(pessoas);
        }
        else
        {
            return NotFound("Não encontramos pessoas com esse nome, realize nova busca.");
        }
    }

    [HttpGet("por-cpfcnpj/{cpfCnpj}")]
    public ActionResult<PessoaPorDocDTO> GetPorCpfCnpj(string cpfCnpj)
    {
        var pessoa = _pessoaRepo.GetPorCpfCnpj(cpfCnpj);

        if (pessoa == null)
        {
            return NotFound("Essa pessoa não foi encontrada. Tente novamente.");
        }

        return Ok(pessoa);
    }

    [HttpGet("por-identidade/{identidade}")]
    public ActionResult<List<PessoaPorDocDTO>> GetPorIdentidade(string identidade)
    {
        var pessoas = _pessoaRepo.GetPorIdentidade(identidade);
        
        if (pessoas == null)
        {
            return NotFound("Não foram encontradas pessoas com a identidade fornecida. Entre outro número e pesquise novamente.");
        }

        return Ok(pessoas);

    }

    [HttpPut("{id:Guid}")]
    public IActionResult PutPessoa(Guid id, [FromBody] Pessoa pessoa)
    {
        if (id != pessoa.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });
        }

        _pessoaRepo.PutPessoa(pessoa);

        return NoContent();

    }

    [HttpDelete("{id:Guid}")]
    public IActionResult DeletePessoa(Guid id, [FromBody] PessoaDeleteModel deleteModel)
    {
        if (id != deleteModel.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });
        }

        _pessoaRepo.DeletePessoa(deleteModel);

        return NoContent();
    }
}
