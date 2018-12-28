using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using bankLedger.Web.App_Start;
using System.Web.Optimization;
using StructureMap;

namespace bankLedger.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            IContainer container = TypeConfig.RegisterTypes();
            DependencyResolver.SetResolver(new StructureMapResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
