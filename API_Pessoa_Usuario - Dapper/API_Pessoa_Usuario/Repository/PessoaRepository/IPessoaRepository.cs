using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;

namespace API_Pessoa_Usuario.Repository.PessoaRepository
{
    public interface IPessoaRepository
    {
        public bool Post(Pessoa pessoa);
        public Pessoa GetPessoaPorId(Guid id);
        public IEnumerable<Pessoa> GetPessoaPorNome(string nome);
        public PessoaPorDocDTO GetPorCpfCnpj(string cpfCnpj);
        public IEnumerable<PessoaPorDocDTO> GetPorIdentidade(string identidade);
        public void PutPessoa(Pessoa pessoa);
        public void DeletePessoa(PessoaDeleteModel deleteModel);


    }
}
