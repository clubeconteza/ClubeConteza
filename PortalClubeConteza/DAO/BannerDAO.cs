using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PortalClubeConteza.DAO
{
    public class BannerDAO
    {
        private EntidadesContext contexto;

        public BannerDAO(EntidadesContext contexto)
        {
            this.contexto = contexto;
        }

        public List<Entities.Banner> BuscaBannerPortal(int sessao, Int64 cidade)
        {
            var resultado = new List<Entities.Banner>();

            try
            {
                var busca = from b in contexto.Banners
                            join m in contexto.Municipios on b.IdMunicipio equals m.Id
                            where b.Sessao == sessao
                               && m.Id == cidade
                               && b.Status == 1
                            orderby b.Sequencia
                            select b;

                resultado = busca.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }
    }
}