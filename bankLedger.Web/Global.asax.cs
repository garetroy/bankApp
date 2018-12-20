using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using bankLedger.Web.App_Start;
using System.Web.Optimization;

namespace bankLedger.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
