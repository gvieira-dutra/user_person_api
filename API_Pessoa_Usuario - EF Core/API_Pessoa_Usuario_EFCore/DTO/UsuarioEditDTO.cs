namespace API_Pessoa_Usuario_EFCore.DTO;

public class UsuarioEditDTO
{
    public Guid Id { get; set; }
    public string Apelido { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;
}
