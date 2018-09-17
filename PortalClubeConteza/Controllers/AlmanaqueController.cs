using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class AlmanaqueController : Controller
    {
        public ActionResult Index()
        {
            var arquivoPdf = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/File/Almanaque/Almanaque.pdf"));
            return File(arquivoPdf, "application/pdf");
        }
    }
}