using System.Web.Optimization;

namespace bankLedger.Web.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                "~/Content/css/bootstrap.min.css"
            ));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/css/navbar.css",
                "~/Content/css/login.css",
                "~/Content/css/main.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jQuery-3.3.1.min.js",
                "~/Scripts/bootstrap.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                "~/Scripts/login.js",
                "~/Scripts/createAccount.js",
                "~/Scripts/accountLedger.js"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}