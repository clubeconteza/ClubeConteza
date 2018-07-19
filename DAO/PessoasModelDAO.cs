using Controller;
using DAO.Infrastructure;

namespace DAO
{
    public class PessoasModelDAO
    {
        public PessoasModelController Pessoas { get; set; }

        private UnidadeTrabalho unidadeTrabalho;

        public PessoasModelDAO(IUnidadeTrabalho unidadeTrabalho)
        {
            this.unidadeTrabalho = unidadeTrabalho as UnidadeTrabalho;
            Pessoas = new PessoasModelController();
        }
    }
}
