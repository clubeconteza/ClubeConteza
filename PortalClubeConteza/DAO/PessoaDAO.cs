using PortalClubeConteza.Entities;
using System;
using System.Linq;

namespace PortalClubeConteza.DAO
{
    public class PessoaDAO
    {
        private EntidadesContext contexto;

        public PessoaDAO(EntidadesContext contexto)
        {
            this.contexto = contexto;
        }

        public Pessoa BuscaPessoaPorId(long idPessoa)
        {
            try
            {
                var busca = from p in contexto.Pessoas
                            where p.Id == idPessoa
                            select p;

                var pessoa = busca.FirstOrDefault();
                return pessoa ?? new Pessoa();
            }
            catch
            {
                return new Pessoa();
            }
        }

        public Pessoa BuscaPessoaAtivaPorCpfCnpj(string cpf)
        {
            try
            {
                var busca = from p in contexto.Pessoas
                            join c in contexto.Contratos on p.IdContratos equals c.Id
                            where p.Status == 1
                               && c.Status == 1
                               && p.CpfCnpj == cpf.Trim()
                            select p;

                var pessoa = busca.FirstOrDefault();
                return pessoa;
            }
            catch
            {
                return null;
            }
        }

        public Pessoa BuscaPessoaComAcessosMultiplosPorCpfCnpj(string cpf)
        {
            try
            {
                var busca = from p  in contexto.Pessoas
                            join pc in contexto.PessoaContratos on p.Id equals pc.IdPessoa
                            join c  in contexto.Contratos on pc.IdContratos equals c.Id
                            where c.Status == 1
                               && p.CpfCnpj == cpf.Trim()
                            select p;

                var pessoa = busca.FirstOrDefault();
                return pessoa;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}