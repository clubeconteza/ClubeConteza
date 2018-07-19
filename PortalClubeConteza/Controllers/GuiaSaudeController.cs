using PortalClubeConteza.DAO;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class GuiaSaudeController : Controller
    {
        private BannerDAO bannerDAO;

        public GuiaSaudeController(BannerDAO bannerDAO)
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

            ViewBag.Banner = bannerDAO.BuscaBannerPortal(1, 3437);

            var categoriaDAO = new CategoriaDAO();
            ViewBag.NivelUm = categoriaDAO.RetornoCategoriaNivelUm(1);

            return View();
        }

        public ActionResult Profissionais(long id)
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            ViewBag.Banner = bannerDAO.BuscaBannerPortal(1, 3437);

            var parceiroDAO = new ParceiroDAO();
            var parceiro = parceiroDAO.RetornaDetalheParceiroPessoaFisica(id);

            var chaveUsuario = Session["Acesso"];
            if (chaveUsuario != null)
            {
                ViewBag.LinkVouchers = string.Concat("https://vouchers.sodaweb.com.br/emitir-voucher/", parceiro.Id_T020, "?userKey=", chaveUsuario.ToString());
            }
            else
            {
                ViewBag.LinkVouchers = "https://vouchers.sodaweb.com.br/emitir-voucher/";
            }

            return View(parceiro);
        }

        public ActionResult ParceiroSaude(long cidade, int pagina, string nivelUm, string nivelDois, string nivelTres)
        {
            nivelUm = string.IsNullOrEmpty(nivelUm) ? "0" : nivelUm.Replace(",", ";");
            nivelDois = string.IsNullOrEmpty(nivelDois) ? "0" : nivelDois.Replace(",", ";");
            nivelTres = string.IsNullOrEmpty(nivelTres) ? "0" : nivelTres.Replace(",", ";");

            var parceiroDAO = new ParceiroDAO();
            var parceiro = parceiroDAO.ParceiroSessaoCidade(1, cidade, 8, pagina, nivelUm, nivelDois, nivelTres);
            return Json(parceiro);
        }

        public ActionResult ComboDois(string nivelUm)
        {
            nivelUm = nivelUm.Replace(",", ";");

            var categoriaDAO = new CategoriaDAO();
            var comboDois = categoriaDAO.RetornoCategoriaNivelDois(nivelUm);
            return Json(comboDois);
        }

        public ActionResult ComboTres(string nivelDois)
        {
            nivelDois = nivelDois.Replace(",", ";");

            var categoriaDAO = new CategoriaDAO();
            var comboTres = categoriaDAO.RetornoCategoriaNivelTres(nivelDois);
            return Json(comboTres);
        }
    }
}