using System;
using System.Web.Mvc;
using System.Web.Optimization;
namespace _10717proiect.App_Start
{
	public static class BundleConfig
	{
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bootstrap style
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                      "~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
            void Application_Start(object sender, EventArgs e)
            {

                // Bootstrap style
                bundles.Add(new
                StyleBundle("~/bundles/bootstrap/css").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
                // Bootstrap
                bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                "~/Scripts/bootstrap.min.js"));
                //Bootstrap style
                bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include( "~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
                // Font Awesome icons style
                bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                //Toaster
                "~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));
                bundles.Add(new StyleBundle("~/bundles/toaster/css").Include(
                "~/Vendors/toastr/toastr.min.css", new CssRewriteUrlTransform()));
                //DataTables
                bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                "~/Vendors/datatables/datatables.min.css", new CssRewriteUrlTransform()));
                BundleConfig.RegisterBundles(BundleTable.Bundles);
            }
        }
    }
}