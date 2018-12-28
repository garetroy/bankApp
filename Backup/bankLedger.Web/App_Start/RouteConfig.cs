using System.Web.Mvc;
using System.Web.Routing;

namespace bankLedger.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            RegisterLoginPaths(routes);
            RegisterAccountPaths(routes);
        }

        public static void RegisterLoginPaths(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Login",
                url: "Login/",
                defaults: new { controller = "Login", action = "Login" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "AttemptLogin",
                url: "Login/AttemptLogin",
                defaults: new { controller = "Login", action = "AttemptLogin" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
                );

            routes.MapRoute(
                name: "CreateAccount",
                url: "Login/CreateAccount",
                defaults: new { controller = "Login", action = "CreateAccount" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "CreateAccountSubmit",
                url: "Login/CreateAccountSubmit",
                defaults: new { controller = "Login", action = "CreateAccountSubmit" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
                );

            routes.MapRoute(
                name: "Logout",
                url: "Login/Logout",
                defaults: new { controller = "Login", action = "Logout" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );
        }

        public static void RegisterAccountPaths(RouteCollection routes)
        {
            routes.MapRoute(
                name: "AccountInfo",
                url: "Account/AccountInfo",
                defaults: new { controller = "Account", action = "AccountInfo" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "AccountLedger",
                url: "Account/AccountLedger",
                defaults: new { controller = "Account", action = "AccountLedger" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "CreateAccountLedger",
                url: "Account/CreateAccountLedger",
                defaults: new { controller = "Account", action = "CreateAccountLedger" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
                );
        }
    }
}
