namespace API_Pessoa_Usuario.DTO
{
    public class UsuarioLoginDTO
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public Guid Id { get; set; }
        public string Senha { get; set; }
        public string Mensagem { get; set; }
    }
}
