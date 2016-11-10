using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/EditProperties").Include(
                    "~/Scripts/bootstrap-combobox.js",
                    "~/Scripts/EditProperties.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/EditUsers").Include(
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/userConfig.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/WebShop").Include(
                    "~/Scripts/webshop.js")
            );

            bundles.Add(new ScriptBundle("~/Content/editcategories").Include(
                    "~/Scripts/editcategories.js",
                    "~/Scripts/jquery-sortable.js",
                    "~/Scripts/jquery.treemenu.js"));

            bundles.Add(new StyleBundle("~/Content/EditUsers_css").Include(
        "~/Content/dataTables.bootstrap.min.css",
        "~/Content/EditUsersStylesheet.css"
        ));

            bundles.Add(new StyleBundle("~/Content/EditPropertiesStylesheet").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/EditPropertiesStylesheet.css",
                    "~/Content/bootstrap-combobox.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.js",
                "~/Scripts/respond.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.flaty.min.css"));

            bundles.Add(new StyleBundle("~/Content/PhoneStylesheet").Include(
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/FridgeStylesheet").Include(
                "~/Content/FridgeIndex.css"));

            bundles.Add(new StyleBundle("~/Content/TVStylesheet").Include(
                "~/Content/TVstylesheet.css"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/IFrameStyle").Include(
                "~/Content/Frame/highlight.css"));

            bundles.Add(new ScriptBundle("~/Content/IFrameJs").Include(
               "~/Scripts/Frame/Main.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                "~/Scripts/bootstrap-select.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-select").Include(
                "~/Content/bootstrap-select.min.css"));
        }
    }
}