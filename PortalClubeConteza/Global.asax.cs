using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using PortalClubeConteza.DAO;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PortalClubeConteza
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<EntidadesContext>().ToSelf().InRequestScope();
        }

        protected void Application_BeginRequest()
        {
            if (!Request.Url.Host.ToLower().StartsWith("www.") && !Request.Url.IsLoopback && Request.Url.HostNameType.Equals(UriHostNameType.Dns))
            {
                var uri = new UriBuilder(Request.Url);
                uri.Scheme = "https";
                uri.Host = string.Concat("www.", Request.Url.Host);
                Response.Redirect(uri.ToString(), true);
            }
        }
    }
}
