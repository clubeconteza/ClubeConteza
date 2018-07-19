using System.Linq;

namespace PortalClubeConteza.DAO
{
    public class MunicipioDAO
    {
        private EntidadesContext contexto;

        public MunicipioDAO(EntidadesContext contexto)
        {
            this.contexto = contexto;
        }

        public string BuscaCidadesAtivas()
        {
            var busca = from m in contexto.Municipios
                        join e in contexto.Estados on m.IdEstado equals e.Id
                        join c in contexto.Contratos on m.Id equals c.IdMunicipio
                        where c.Status == 1 && c.TipoContrato == 2
                        group m by new { m.Id, m.Municipios, e.Sigla } into g
                        orderby g.Key.Sigla, g.Key.Municipios
                        select g.Key;

            var lista = busca.ToList();

            return "";
        }
    }
}