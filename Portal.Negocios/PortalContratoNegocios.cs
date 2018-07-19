using Controller;
using Portal.DAO;
using System;

namespace Portal.Negocios
{
    public class PortalContratoNegocios
    {
        public ContratosController AcessoUsuarioPlanoFamiliar(string Chave)
        {
            try
            {
                PortalContratoDAO DAO = new PortalContratoDAO();
                return DAO.AcessoUsuarioPlanoFamiliar(Chave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
