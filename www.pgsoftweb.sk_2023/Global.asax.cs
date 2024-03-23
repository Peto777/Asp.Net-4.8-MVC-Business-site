using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace www.pgsoftweb.sk_2023
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string request = HttpContext.Current.Request.Url.ToString();
            if (request.Contains("http://gloziksoft.sk") && !request.Contains("win-sirius"))
            {
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location",
                    Request.Url.ToString().ToLower().Replace("http://gloziksoft.sk", "http://www.gloziksoft.sk"));
            }
        }
    }
}
