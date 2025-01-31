using API_Pessoa_Usuario_EFCore.Domain;
using API_Pessoa_Usuario_EFCore.DTO;

namespace API_Pessoa_Usuario_EFCore.Repository.PessoaRepository
{
    public interface IPessoaRepository
    {
        public bool Post(Pessoa pessoa);
        public Pessoa GetPessoaPorId(Guid id);
        public IEnumerable<Pessoa> GetPessoaPorNome(string nome);
        public PessoaPorDocDTO GetPorCpfCnpj(string cpfCnpj);
        public IEnumerable<PessoaPorDocDTO> GetPorIdentidade(string identidade);
        public void PutPessoa(PessoaEditarDTO pessoa);
        public void DeletePessoa(PessoaDeleteDTO deleteModel);
    }
}
