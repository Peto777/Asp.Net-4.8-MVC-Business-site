using System.Web.Mvc;
using System.Web.Routing;

namespace www.gloziksoft.sk_2023
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // SEO optimized URLs
            //
            // Home controller
            routes.MapRoute("en", "en", new { controller = "Home", action = "Index" });
            routes.MapRoute("en/software-development", "en/software-development", new { controller = "Home", action = "Portfolio" });
            routes.MapRoute("en/reprogramming", "en/reprogramming", new { controller = "Home", action = "Reprogramming" });
            routes.MapRoute("en/website-design", "en/website-design", new { controller = "Home", action = "Web" });
            routes.MapRoute("en/website-design/websites-delivered", "en/website-design/websites-delivered", new { controller = "Home", action = "Web_Projects" });
            routes.MapRoute("en/windows-apps", "en/windows-apps", new { controller = "Home", action = "Windows" });
            routes.MapRoute("en/mobile-apps", "en/mobile-apps", new { controller = "Home", action = "Mobile" });
            routes.MapRoute("en/contact", "en/contact", new { controller = "Home", action = "Contact" });
            routes.MapRoute("en/contact/message", "en/contact/message", new { controller = "Home", action = "ContactSendSuccess" });
            routes.MapRoute("en/price", "en/price", new { controller = "Home", action = "Price" });
            routes.MapRoute("en/sitemap", "en/sitemap", new { controller = "Home", action = "SiteMap" });
            routes.MapRoute("en/useful-links", "en/useful-links", new { controller = "Home", action = "Links" });

            // HomeSk controller
            routes.MapRoute("sk", "sk", new { controller = "HomeSk", action = "Index" });
            routes.MapRoute("sk/tvorba-webov", "sk/tvorba-webov", new { controller = "HomeSk", action = "Web_" });
            routes.MapRoute("sk/tvorba-webov/seo", "sk/tvorba-webov/seo", new { controller = "HomeSk", action = "Web_Seo" });
            routes.MapRoute("sk/tvorba-webov/blog", "sk/tvorba-webov/blog", new { controller = "HomeSk", action = "Web_Blog" });
            routes.MapRoute("sk/tvorba-webov/blog-news", "sk/tvorba-webov/blog-news", new { controller = "HomeSk", action = "Web_BlogNews" });
            routes.MapRoute("sk/tvorba-webov/eshop", "sk/tvorba-webov/eshop", new { controller = "HomeSk", action = "Web_Eshop" });
            routes.MapRoute("sk/tvorba-webov/mikrostranka", "sk/tvorba-webov/mikrostranka", new { controller = "HomeSk", action = "Web_Microsite" });
            routes.MapRoute("sk/tvorba-webov/cms", "sk/tvorba-webov/cms", new { controller = "HomeSk", action = "Web_Cms" });
            routes.MapRoute("sk/tvorba-webov/realizovane-weby", "sk/tvorba-webov/realizovane-weby", new { controller = "HomeSk", action = "Web_Projects" });
            routes.MapRoute("sk/webove-aplikacie", "sk/webove-aplikacie", new { controller = "HomeSk", action = "Web" });
            routes.MapRoute("sk/kontakt", "sk/kontakt", new { controller = "HomeSk", action = "Contact" });
            routes.MapRoute("sk/kontakt/odoslanie-spravy", "sk/kontakt/odoslanie-spravy", new { controller = "HomeSk", action = "ContactSendSuccess" });
            routes.MapRoute("sk/cena", "sk/cena", new { controller = "HomeSk", action = "Price" });
            routes.MapRoute("sk/mapa-stranky", "sk/mapa-stranky", new { controller = "HomeSk", action = "SiteMap" });
            routes.MapRoute("sk/uzitocne-odkazy", "sk/uzitocne-odkazy", new { controller = "HomeSk", action = "Links" });
            routes.MapRoute("sk/video", "sk/video", new { controller = "HomeSk", action = "Video" });
            routes.MapRoute("sk/eshop-webareal-registracia", "sk/eshop-webareal-registracia", new { controller = "Webareal", action = "RegisterWebarealSk" });
            routes.MapRoute("sk/ochrana-osobnych-udajov", "sk/ochrana-osobnych-udajov", new { controller = "HomeSk", action = "Oou" });
            routes.MapRoute("sk/odstupenie-od-zmluvy", "sk/odstupenie-od-zmluvy", new { controller = "HomeSk", action = "CancelContract" });
            routes.MapRoute("sk/obchodne-podmienky", "sk/obchodne-podmienky", new { controller = "HomeSk", action = "TradeRules" });

            routes.MapRoute("sk/webove_stranky", "sk/webove_stranky", new { controller = "HomeSk", action = "SEO_WebDevelopment" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
