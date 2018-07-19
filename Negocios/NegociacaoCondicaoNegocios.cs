using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocios
{
    public class NegociacaoCondicaoNegocios
    {



        public List<NegociacaoCondicaoController> NegociacaoCondicaoPorUsuario(Int64 TB011_Id)
        {
            try
            {
                return new NegociacaoCondicaoDAO().NegociacaoCondicaoPorUsuario(TB011_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NegociacaoCondicaoController NegociacaoCondicaoId(long TB036_Id)
        {
            try
            {
                return new NegociacaoCondicaoDAO().NegociacaoCondicaoId(TB036_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
