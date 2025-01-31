using API_Pessoa_Usuario_Dapper;
using API_Pessoa_Usuario_EFCore.Domain;
using API_Pessoa_Usuario_EFCore.DTO;
using API_Pessoa_Usuario_EFCore.Repository.UsuarioRepository;
using Microsoft.AspNetCore.Mvc;

namespace API_Pessoa_Usuario_EFCore.Controllers;

[Route("api/v1/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepo;

    public UsuarioController(IUsuarioRepository usuarioRepo)
    {
        _usuarioRepo = usuarioRepo;
    }
    [HttpPost]
    public ActionResult Post([FromBody] Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Problema ao criar o usuario, verifique os dados e tente novamente."
            });
        }

        if (_usuarioRepo.Post(usuario))
        {
            return Ok();
        }

        return BadRequest("Não foi possível a criaçao desse usuário.");

    }

    [HttpPost("login")]
    public ActionResult<UsuarioLoginDTO> PostLogin([FromBody] UsuarioLoginDTO loginInfo)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Problema ao validar dados. Nao foi possivel validar seu login"
            });
        }

        var usuarioLogin = _usuarioRepo.PostLogin(loginInfo);

        if (usuarioLogin == null) return NotFound("Login ou senha incorretos.");

        return Ok(usuarioLogin);

    }

    [HttpGet("por-id/{id:guid}")]
    public ActionResult<UsuarioPessoaDTO> GetById(Guid id)
    {
        var usuario = _usuarioRepo.GetUsuarioPorId(id);

        if (usuario == null) return NotFound();

        return Ok(usuario);

    }

    [HttpGet("por-cpf-cnpj/{cpfCnpj}")]
    public ActionResult<UsuarioPessoaDTO> GetByCpfCnpj(string cpfCnpj)
    {
        var usuario = _usuarioRepo.GetUsuarioPorCpfCnpj(cpfCnpj);

        if (usuario == null) return NotFound();

        return Ok(usuario);
    }

    [HttpGet("por-apelido/{apelido}")]
    public ActionResult<UsuarioPessoaDTO> GetByApelido(string apelido)
    {
        var usuario = _usuarioRepo.GetUsuarioPorApelido(apelido);

        if (usuario.Count() == 0) return NotFound();

        return Ok(usuario);
    }

    [HttpGet("por-identidade/{identidade}")]
    public ActionResult<UsuarioPessoaDTO> GetByIdentidade(string identidade)
    {
        var usuario = _usuarioRepo.GetUsuarioPorIdentidade(identidade);

        if (usuario.Count == 0) return NotFound();

        return Ok(usuario);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, [FromBody] UsuarioEditDTO usuarioEdit)
    {
        if (id != usuarioEdit.Id) return BadRequest("1 Inconsistencias nas informações não permitem prosseguir com o processo.");

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "2 Inconsistencias nas informações não permitem prosseguir com o processo."
            });
        }

        _usuarioRepo.Put(usuarioEdit);

        return NoContent();

    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id, [FromBody] UsuarioDeleteDTO usuarioDelete)
    {
        if (id != usuarioDelete.Id) return BadRequest("1 Inconsistencias nas informações não permitem prosseguir com o processo. Verifique e tente novamente.");

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "2 Inconsistencias nas informações não permitem prosseguir com o processo. Verifique e tente novamente."
            });
        }

        _usuarioRepo.Delete(usuarioDelete);

        return NoContent();
    }
}
