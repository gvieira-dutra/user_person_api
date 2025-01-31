using API_Pessoa_Usuario_EFCore.Data;
using API_Pessoa_Usuario_EFCore.Domain;
using API_Pessoa_Usuario_EFCore.DTO;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API_Pessoa_Usuario_EFCore.Repository.PessoaRepository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ApplicationContext _context;

        public PessoaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Post(Pessoa pessoa)
        {
            try
            {
                _context.Pessoas.Add(pessoa);

                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Pessoa GetPessoaPorId(Guid id)
        {
            // Query TAGs
            return _context.Pessoas.TagWith("Consulta por id").FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Pessoa> GetPessoaPorNome(string nome)
        {
            // Query TAGs
            var pessoas = _context.Pessoas.Where(p => p.Nome.ToLower().Contains(nome)).TagWith("Consulta por nome").ToList();

            return pessoas;
        }

        public PessoaPorDocDTO GetPorCpfCnpj(string cpfCnpj)
        {
            // Query TAGs
            var pessoa = _context.Pessoas.TagWith("Consulta por cpf / cnpj").Where(p => p.DocCpfCnpj.Equals(cpfCnpj)).FirstOrDefault();

            if (pessoa == null) return null;

            return new PessoaPorDocDTO()
            {
                Nome = pessoa.Nome,
                ApelidoFantasia = pessoa.ApelidoFantasia ?? string.Empty,
                DocIdentidade = pessoa.DocIdentidade,
                TenantId = pessoa.TenantId
            };
        }

        public IEnumerable<PessoaPorDocDTO> GetPorIdentidade(string identidade)
        {
            // Query TAGs
            var pessoas = _context.Pessoas.TagWith("Consulta por identidade").Where(p => p.DocIdentidade == identidade);

            var result = new List<PessoaPorDocDTO>();

            foreach (var pessoa in pessoas)
            {
                result.Add(new PessoaPorDocDTO { 
                    Nome = pessoa.Nome, 
                    ApelidoFantasia = pessoa.ApelidoFantasia ?? string.Empty, 
                    DocIdentidade = pessoa.DocIdentidade, 
                    TenantId = pessoa.TenantId });
            }

            return result;
        }

        public void PutPessoa(PessoaEditarDTO pessoa)
        {
            var pessoaId = new Pessoa
            {
                Id = pessoa.Id
            };

            var pessoaInfo = new
            {
                Nome = (pessoa?.Nome == null ? null : pessoa.Nome),
                ApelidoFantasia = (pessoa?.ApelidoFantasia == null ? null : pessoa.ApelidoFantasia),
                LastModifiedBy = pessoa?.Id,
                pessoa?.LastModifiedOn
            };

            _context.Attach(pessoaId);
            _context.Entry(pessoaId).CurrentValues.SetValues(pessoaInfo);

            _context.SaveChanges();
        }

        public void DeletePessoa(PessoaDeleteDTO deleteModel)
        {
            var pessoaId = new Pessoa
            {
                Id = deleteModel.Id,
            };

            var pessoaDelete = new
            {
                deleteModel.DeletedBy,
                Ativo = false
            };

            _context.Attach(pessoaId);
            _context.Entry(pessoaId).CurrentValues.SetValues(pessoaDelete);

            _context.SaveChanges();
        }
    }
}
