using Boleto.Controller;
using Boleto.Negocios;
using System.Web.Services;

namespace Boleto
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Login : WebService
    {
        [WebMethod(Description = "Acesso ao usuário")]
        public AcessoUsuarioController AcessoUsuario(string chaveUsuario, string cnpjParceiro, string senhaParceiro)
        {
            return new AcessoUsuarioNegocios().AcessoUsuarioPlano(chaveUsuario, cnpjParceiro, senhaParceiro);
        }
    }
}
