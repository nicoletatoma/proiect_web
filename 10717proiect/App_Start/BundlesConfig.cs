using System;
using System.Web.Optimization;


namespace _10717proiect.App_Start
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/assets/css/aplicatie.css").Include(
                "~/Content/assets/css/aplicatie.css"));
            bundles.Add(new StyleBundle("~/Content/assets/css/app.css").Include(
                "~/Content/assets/css/app.css"));
            bundles.Add(new StyleBundle("~/Content/assets/css/bootstrap.css").Include(
                "~/Content/assets/css/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/assets/css/icons.css").Include(
                "~/Content/assets/css/icons.css"));


        }

    }
}
