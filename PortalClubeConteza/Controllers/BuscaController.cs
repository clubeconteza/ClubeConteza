using PortalClubeConteza.DAO;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class BuscaController : Controller
    {
        private BannerDAO bannerDAO;

        public BuscaController(BannerDAO bannerDAO)
        {
            this.bannerDAO = bannerDAO;
        }

        public ActionResult Resultado(long cidade, int pagina, string buscar)
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            ViewBag.Banner = bannerDAO.BuscaBannerPortal(1, 3437);

            return View();
        }

        public ActionResult Parceiro(long cidade, int pagina, string buscar)
        {
            var parceiroDAO = new ParceiroDAO();
            var parceiro = parceiroDAO.ParceiroBuscaSessaoCidadeTodos(1, cidade, 8, pagina, buscar);
            return Json(parceiro);
        }
    }
}