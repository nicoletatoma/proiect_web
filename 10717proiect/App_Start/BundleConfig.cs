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

               
                BundleConfig.RegisterBundles(BundleTable.Bundles);
            }
        }
    }
}