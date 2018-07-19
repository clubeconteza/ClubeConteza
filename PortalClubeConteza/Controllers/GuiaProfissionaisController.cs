using PortalClubeConteza.DAO;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class GuiaProfissionaisController : Controller
    {
        private BannerDAO bannerDAO;

        public GuiaProfissionaisController(BannerDAO bannerDAO)
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

            ViewBag.Banner = bannerDAO.BuscaBannerPortal(4, 3437);

            var categoriaDAO = new CategoriaDAO();
            ViewBag.NivelUm = categoriaDAO.RetornoCategoriaNivelUm(4);

            return View();
        }

        public ActionResult Profissionais(long id)
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            ViewBag.Banner = bannerDAO.BuscaBannerPortal(4, 3437);

            var parceiroDAO = new ParceiroDAO();
            var parceiro = parceiroDAO.RetornaDetalheParceiroPessoaFisica(id);
            return View(parceiro);
        }

        public ActionResult ParceiroSaude(long cidade, int pagina, string nivelUm, string nivelDois, string nivelTres)
        {
            nivelUm = string.IsNullOrEmpty(nivelUm) ? "0" : nivelUm.Replace(",", ";");
            nivelDois = string.IsNullOrEmpty(nivelDois) ? "0" : nivelDois.Replace(",", ";");
            nivelTres = string.IsNullOrEmpty(nivelTres) ? "0" : nivelTres.Replace(",", ";");

            var parceiroDAO = new ParceiroDAO();
            var parceiro = parceiroDAO.ParceiroSessaoCidade(4, cidade, 8, pagina, nivelUm, nivelDois, nivelTres);
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