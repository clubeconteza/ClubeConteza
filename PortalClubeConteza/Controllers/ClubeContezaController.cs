using PortalClubeConteza.DAO;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class ClubeContezaController : Controller
    {
        private BannerDAO bannerDAO;

        public ClubeContezaController(BannerDAO bannerDAO)
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
    }
}