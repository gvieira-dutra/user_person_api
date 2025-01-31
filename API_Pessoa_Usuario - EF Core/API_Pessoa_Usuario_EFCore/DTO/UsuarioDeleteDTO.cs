namespace API_Pessoa_Usuario_EFCore.DTO;

public class UsuarioDeleteDTO
{
    public Guid Id { get; set; }
    public Guid DeletedBy { get; set; }
    public DateTime DeletedOn { get; set; } = DateTime.UtcNow;
}
