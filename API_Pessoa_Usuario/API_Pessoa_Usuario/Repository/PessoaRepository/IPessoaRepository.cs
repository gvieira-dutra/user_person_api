using API_Pessoa_Usuario.DTO;
using API_Pessoa_Usuario.Entities;

namespace API_Pessoa_Usuario.Repository.PessoaRepository
{
    public interface IPessoaRepository
    {
        public bool Post(Pessoa pessoa);
        public Pessoa GetPessoaPorId(Guid id);
        public List<Pessoa> GetPessoaPorNome(string nome);
        public PessoaPorDocDTO GetPorCpfCnpj(string cpfCnpj);
        public List<PessoaPorDocDTO> GetPorIdentidade(string identidade);
        public void PutPessoa(PessoaEdit pessoa);
        public void DeletePessoa(PessoaDeleteModel deleteModel);


    }
}
