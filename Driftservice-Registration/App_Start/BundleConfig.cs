using System.Web;
using System.Web.Optimization;

namespace Driftservice_Registration
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                "~/Scripts/datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                "~/Scripts/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/tabtoggle").Include(
                "~/Scripts/tabtoggle.js"));

            bundles.Add(new ScriptBundle("~/bundles/checkboxes").Include(
                "~/Scripts/checkboxes.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css"));
        }
    }
}
