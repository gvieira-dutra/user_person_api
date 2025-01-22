namespace API_Pessoa_Usuario.Entities
{
    public class UsuarioEdit
    {
        public Guid Id { get; set; }
        public string Apelido { get; set; }
        public Guid ModifierId { get; set; }
    }
}
