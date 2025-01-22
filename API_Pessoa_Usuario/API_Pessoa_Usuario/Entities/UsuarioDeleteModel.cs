namespace API_Pessoa_Usuario.Entities;

public class UsuarioDeleteModel
{
    public Guid Id { get; set; }
    public Guid DeletedBy { get; set; }
    public DateTime DeletedOn { get; set; }
}
