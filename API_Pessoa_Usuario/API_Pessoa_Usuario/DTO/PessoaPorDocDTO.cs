namespace API_Pessoa_Usuario.DTO;

public class PessoaPorDocDTO
{
    public string Nome { get; set; }
    public string ApelidoFantasia { get; set; }
    public string? DocCpfCnpj { get; set; }
    public string? DocIdentidade { get; set; }
    public string? DocIdentidadeEmissor { get; set; }
}
