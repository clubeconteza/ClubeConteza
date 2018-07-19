using PortalClubeConteza.DAO;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class HomeController : Controller
    {
        private BannerDAO bannerDAO;

        public HomeController(BannerDAO bannerDAO)
        {
            this.bannerDAO = bannerDAO;
        }

        public ActionResult Index()
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            ViewBag.Banner = bannerDAO.BuscaBannerPortal(3, 3437);

            return View();
        }

        public ActionResult SucessoEnvio()
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