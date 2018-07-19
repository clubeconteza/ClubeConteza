using System.Web.Mvc;
using System.Web.Routing;

namespace PortalClubeConteza.Filters
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuario = filterContext.HttpContext.Session["Usuario"];

            if (usuario == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            action = "Index",
                            controller = "Login"
                        }));
            }
        }
    }
}