using System;
using System.Web.Optimization;
namespace bankLedger.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/navbar.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jQuery-3.3.1.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                "~/Scripts/login.js",
                "~/Scripts/ledgerForm.js"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
