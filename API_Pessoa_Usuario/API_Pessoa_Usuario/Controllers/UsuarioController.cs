using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;
using API_Pessoa_Usuario.Repository.PessoaRepository;
using API_Pessoa_Usuario.Repository.UsuarioRepository;
using Microsoft.AspNetCore.Mvc;

namespace API_Pessoa_Usuario.Controllers;

[ApiController]
[Route("api/vi/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly DatabaseHelper _dbHelper;
    private readonly IPessoaRepository _pessoaRepo;
    private readonly IUsuarioRepository _usuarioRepo;

    public UsuarioController(IUsuarioRepository usuarioRepo, DatabaseHelper dbHelper, IPessoaRepository pessoaRepo)
    {
        _dbHelper = dbHelper;
        _pessoaRepo = pessoaRepo;
        _usuarioRepo = usuarioRepo;
    }

    [HttpPost]
    public ActionResult<Usuario> Post([FromBody] Usuario novoUsuario)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });
        }

        if (_usuarioRepo.Post(novoUsuario))
        {
            return CreatedAtAction(nameof(GetUsuarioPorId), new { id = novoUsuario.Id }, novoUsuario);
        }
        else
        {
            return BadRequest("Algo deu errado, verifique as informacoes e tente novamente.");
        }

    }

    [HttpPost("login")]
    public ActionResult<UsuarioLoginDTO> PostLogin([FromBody] UsuarioLogin usuarioInfo)
    {
        var usuarioLogado = _usuarioRepo.PostLogin(usuarioInfo);

        if (usuarioLogado == null)
        {
            return NotFound("Usuário ou senha inválidos");
        }

        return Ok(usuarioLogado);

    }

    [HttpGet("por-id/{id:Guid}")]
    public IActionResult GetUsuarioPorId(Guid id)
    {

        var pessoaRetorno = _usuarioRepo.GetUsuarioPorId(id);

        if (pessoaRetorno == null)
        {
            return NotFound("Usuário não foi encontrado. Realize nova busca.");
        }
            return Ok(pessoaRetorno);
    }

    [HttpGet("por-apelido/{apelido}")]
    public IActionResult GetUsuarioPorApelido(string apelido)
    {
        var usuarios = _usuarioRepo.GetUsuarioPorApelido(apelido);

        if (usuarios == null)
        {
            return NotFound("Não encontramos usuários com esse apelido, realize nova busca.");
        }
            return Ok(usuarios);
    }

    [HttpGet("por-cpfcnpj/{cpfCnpj}")]
    public IActionResult GetUsuarioPorCpfCnpj(string cpfCnpj)
    {
        var usuario = _usuarioRepo.GetUsuarioPorCpfCnpj(cpfCnpj);

        if (usuario == null)
        {
            return NotFound("Pessoa nao encontrada, forneça outro número de documento para consulta");
        }
            return Ok(usuario);
    }

    [HttpGet("por-identidade/{identidade}")]
    public IActionResult GetUsuarioPorIdentidade(string identidade)
    {
        var usuarios = _usuarioRepo.GetUsuarioPorIdentidade(identidade);

        if (usuarios == null )
        {
            return NotFound("Usuário não encontrado com identidade disponibilizada, tente novamente");
        }
            return Ok(usuarios);
    }

    [HttpPut("{id:Guid}")]
    public ActionResult<Usuario> Put(Guid id, [FromBody] UsuarioEdit usuario)
    {
        _usuarioRepo.Put(usuario);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id, [FromBody] UsuarioDeleteModel deleteInfo)
    {
        if (id != deleteInfo.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });
        }

        _usuarioRepo.Delete(deleteInfo);

        return NoContent();
    }


}
