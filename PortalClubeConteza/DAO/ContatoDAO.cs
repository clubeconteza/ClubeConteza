using System;
using System.Linq;

namespace PortalClubeConteza.DAO
{
    public class ContatoDAO
    {
        private EntidadesContext contexto;

        public ContatoDAO(EntidadesContext contexto)
        {
            this.contexto = contexto;
        }

        public Entities.Contato BuscaContatoEmailPorPessoa(long idPessoa)
        {
            var resultado = new Entities.Contato();

            try
            {
                var busca = from c in contexto.Contatos
                            join p in contexto.Pessoas on c.IdPessoa equals p.Id
                            where c.Tipo == 3
                               && p.Id == idPessoa
                            select c;

                var contato = busca.FirstOrDefault();
                resultado = contato ?? new Entities.Contato();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }
    }
}