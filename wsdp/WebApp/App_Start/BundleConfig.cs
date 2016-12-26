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

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/EditProperties").Include(
				"~/Scripts/Properties/bootstrap-combobox.js",
				"~/Scripts/Properties/EditProperties.js")
				);
			bundles.Add(new ScriptBundle("~/bundles/EditUsers").Include(
				"~/Scripts/Users/jquery.dataTables.min.js",
				"~/Scripts/Users/dataTables.bootstrap.min.js",
				"~/Scripts/Users/userConfig.js")
				);

			bundles.Add(new ScriptBundle("~/bundles/ExecutingInfo").Include(
			"~/Scripts/TaskExecutingInfo/ExecutingInfo.js")
				);

			bundles.Add(new ScriptBundle("~/bundles/WebShop").Include(
				"~/Scripts/WebShop/webshop.js")
				);
            bundles.Add(new ScriptBundle("~/bundles/GoodAutocomplete").Include(
                "~/Scripts/goodsAutocomplete.js")
                );
            bundles.Add(new ScriptBundle("~/bundles/PriceStat").Include("~/Scripts/PriceStat.js",
				"~/Scripts/Chart.js"));

			bundles.Add(new ScriptBundle("~/Scripts/IteratorPage").Include(
				"~/Scripts/IteratorPage/IteratorPage.js",
				"~/Scripts/Frame/IteratorMain.js"
				));

			bundles.Add(new ScriptBundle("~/Scripts/PriceFollowers").Include(
			"~/Scripts/PriceFollow/follow.js"));

			bundles.Add(new ScriptBundle("~/Scripts/GrabberJs").Include(
				"~/Scripts/Frame/GrabberMain.js"));

			bundles.Add(new ScriptBundle("~/Content/editcategories").Include(
				"~/Scripts/Categories/editcategories.js",
				"~/Scripts/Categories/jquery-sortable.js",
				"~/Scripts/Categories/jquery.treemenu.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
				"~/Scripts/JqueryValidate/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
				"~/Scripts/bootstrap/bootstrap.min.js",
				"~/Scripts/bootstrap/respond.js",
				"~/Scripts/bootstrap/respond.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
				"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/executing_tasks").Include(
					"~/Scripts/ExecutingTasks/dynamic_update.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-timeago").Include(
                    "~/Scripts/jquery.timeago.js"));



			bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
				"~/Scripts/Bootstrap-select/bootstrap-select.min.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

			bundles.Add(new StyleBundle("~/Content/EditUsers_css").Include(
				"~/Content/Users/dataTables.bootstrap.min.css",
				"~/Content/Users/EditUsersStylesheet.css"
				));
			bundles.Add(new StyleBundle("~/Content/IndexStylesheet").Include(
				"~/Content/Index/IndexStyleSheet.css",
				"~/Content/Index/Site.css"
				));
			bundles.Add(new StyleBundle("~/Content/EditPropertiesStylesheet").Include(
				"~/Content/bootstrap/bootstrap.css",
				"~/Content/Properties/EditPropertiesStylesheet.css",
				"~/Content/Properties/bootstrap-combobox.css"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/bootstrap.flaty.min.css"));

			bundles.Add(new StyleBundle("~/Content/styles").Include(
				"~/Content/style.css"));

			bundles.Add(new StyleBundle("~/Content/bootstrap-select").Include(
				"~/Content/bootstrap-select.min.css"));

			bundles.Add(new StyleBundle("~/Content/Categories").Include(
				"~/Content/Categories/jquery.treemenu.css",
				"~/Content/Categories/EditCategoriesStylesheet.css"));

			bundles.Add(new StyleBundle("~/Content/ConcreteGoodStylesheet").Include(
				"~/Content/Good/ConcreteGoodStylesheet.css",
				"~/Content/Index/Site.css"
				));
		}

	}
}