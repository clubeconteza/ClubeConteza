using PortalClubeConteza.DAO;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class CorporativoController : Controller
    {
        // GET: Corporativo
        public ActionResult Index()
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            return View();
        }
    }
}