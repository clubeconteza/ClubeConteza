using System.Web.Optimization;

namespace PortalClubeConteza
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            StyleBundle styles = new StyleBundle("~/Bundles/styles");
            styles.Include("~/Content/principal.css");
            bundles.Add(styles);

            ScriptBundle scripts = new ScriptBundle("~/Bundles/scripts");
            bundles.Add(scripts);
        }
    }
}