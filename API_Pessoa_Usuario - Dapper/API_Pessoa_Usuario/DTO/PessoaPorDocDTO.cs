namespace API_Pessoa_Usuario.DTO;

public class PessoaPorDocDTO
{
    public string Nome { get; set; }
    public string Apelido_Fantasia { get; set; }
    public string? Doc_Cpf_Cnpj { get; set; }
    public string? Doc_Identidade { get; set; }
    public string? Doc_Identidade_Emissor { get; set; }
}
