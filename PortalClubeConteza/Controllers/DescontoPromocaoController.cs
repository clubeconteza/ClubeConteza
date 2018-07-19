using PortalClubeConteza.DAO;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class DescontoPromocaoController : Controller
    {
        private BannerDAO bannerDAO;

        public DescontoPromocaoController(BannerDAO bannerDAO)
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

            ViewBag.Banner = bannerDAO.BuscaBannerPortal(2, 3437);

            var chaveUsuario = Session["Acesso"];
            if (chaveUsuario != null)
            {
                ViewBag.LinkVouchers = "https://vouchers.sodaweb.com.br/varejo?userKey=" + chaveUsuario.ToString();
                ViewBag.ErrorMessage = null;
            }
            else
            {
                ViewBag.LinkVouchers = "https://vouchers.sodaweb.com.br/varejo";
                ViewBag.ErrorMessage = "O usuário deve estar logado para emitir os cupons.";
            }

            return View();
        }
    }
}