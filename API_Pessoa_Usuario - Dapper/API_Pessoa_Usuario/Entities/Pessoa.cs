using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pessoa_Usuario.Entities;
 
[Dapper.Contrib.Extensions.Table("Pessoa")]
public class Pessoa
{
    [Key]
    [Column("\"Id\"")]
    public Guid Id { get; set; } 
    public string? Id_Alternativo { get; set; } 
    public string? Tenant_Id { get; set; } 
    public string Name { get; set; } 
    public string? Apelido_Fantasia { get; set; } 
    public int? Fis_Jur { get; set; } = 0; 
    public string? Doc_Cpf_Cnpj { get; set; } 
    public string? Doc_Identidade { get; set; } 
    public string? Doc_Identidade_Emissor { get; set; } 
    public string Doc_Inscricao_Estadual { get; set; } = "ISENTO"; 
    public string? Doc_Inscricao_Municipal { get; set; } 
    public string? Doc_Inscricao_Produtor_Rural { get; set; }
    public string? Doc_Insc_Suframa { get; set; } 
    public DateTime? Nascimento_Fundacao { get; set; } 
    public int? Estado_Civil { get; set; } 
    public string? Nome_Conjuge { get; set; } 
    public string? Nome_Pai { get; set; } 
    public string? Nome_Mae { get; set; } 
    public int? Genero { get; set; } = -1;
    public string? Site { get; set; } 
    public string? Observacao { get; set; } 
    public bool Ativo { get; set; } = true;

    public Guid Created_By { get; set; } 
    public DateTime Created_On { get; set; } 
    public Guid Last_Modified_By { get; set; }
    public DateTime? Last_Modified_On { get; set; } 
    public DateTime? Deleted_On { get; set; }
    public Guid? Deleted_By { get; set; }
}
