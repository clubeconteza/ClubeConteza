using Controller;
using Portal.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Negocios
{
    public class UsuarioPortalNegocios
    {
        public UsuarioPortalController LoginUsuarioPortal(String CPF, string Senha)
        {
            try
            {
                PortalUsuarioDao DAO = new PortalUsuarioDao();

                UsuarioPortalController LoginUsuarioPortal = new UsuarioPortalController();
                LoginUsuarioPortal = DAO.LoginUsuarioPortal(CPF, Senha);



                return LoginUsuarioPortal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
