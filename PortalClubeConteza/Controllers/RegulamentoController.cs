using PortalClubeConteza.DAO;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class RegulamentoController : Controller
    {
        public ActionResult Index()
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            return View();
        }

        public ActionResult Promocao()
        {
            var arquivoPdf = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/File/Regulamento.pdf"));
            return File(arquivoPdf, "application/pdf");
        }
    }
}