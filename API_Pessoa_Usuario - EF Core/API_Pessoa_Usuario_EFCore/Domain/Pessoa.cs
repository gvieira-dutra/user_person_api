namespace API_Pessoa_Usuario_EFCore.Domain
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public string IdAlternativo { get; set; }
        public string TenantId { get; set; }
        public string Nome { get; set; }
        public string? ApelidoFantasia { get; set; }
        public int FisJur { get; set; } = 0;
        public string DocCpfCnpj { get; set; }
        public string? DocIdentidade { get; set; }
        public string? DocIdentidadeEmissor { get; set; }
        public string? DocInscricaoEstadual { get; set; } = "ISENTO";
        public string? DocInscricaoMunicipal { get; set; }
        public string? DocInscricaoProdutorRural { get; set; }
        public string? DocInscSuframa { get; set; }
        public DateTime? NascimentoFundacao { get; set; }
        public int? EstadoCivil { get; set; }
        public string? NomeConjuge { get; set; }
        public string? NomePai { get; set; }
        public string? NomeMae { get; set; }
        public int? Genero { get; set; } = -1;
        public string? Site { get; set; }
        public string? Observacao { get; set; }
        public bool Ativo { get; set; } = true;

        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedOn { get; set; }
        public Guid? DeletedBy { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
