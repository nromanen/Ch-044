using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-sortable.js"));
            bundles.Add(new ScriptBundle("~/bundles/EditProperties").Include(
                    "~/Scripts/jquery-1.10.2.js",
                    "~/Scripts/bootstrap-combobox.js",
                    "~/Scripts/bootstrap.min.js")
            );
            bundles.Add(new StyleBundle("~/Content/EditPropertiesStylesheet").Include(
                "~/Content/bootstrap.css",
                "~/Content/EditPropertiesStylesheet.css",
                "~/Content/bootstrap-combobox.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/PhoneStylesheet").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/FridgeStylesheet").Include(
                "~/Content/bootstrap.css",
                "~/Content/FridgeIndex.css"));
            bundles.Add(new StyleBundle("~/Content/TVStylesheet").Include(
                "~/Content/bootstrap.css",
                "~/Content/TVstylesheet.css"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/Frame/highlight.css"));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                        "~/Content/Frame/Main.js"));

        }
    }
}

