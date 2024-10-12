using System.Web.Mvc;

namespace www.gloziksoft.sk_2023.Controllers
{
    public class RedirectController : Controller
    {
        //
        // GET: /Redirect/

        public ActionResult Cena()
        {
            return RedirectPermanent(Url.Action("Price", "HomeSk"));
        }
        public ActionResult Kontakt()
        {
            return RedirectPermanent(Url.Action("Contact", "HomeSk"));
        }
        public ActionResult OnlineUctovnictvo()
        {
            return RedirectPermanent(Url.Action("Reprogramming", "HomeSk"));
        }
        public ActionResult Bookkeeping()
        {
            return RedirectPermanent(Url.Action("Reprogramming", "HomeSk"));
        }
        public ActionResult ProduktyASluzby()
        {
            return RedirectPermanent(Url.Action("Portfolio", "HomeSk"));
        }



        public ActionResult PortfolioSk()
        {
            return RedirectPermanent(Url.Action("Portfolio", "HomeSk"));
        }
        public ActionResult Web_SeoSk()
        {
            return RedirectPermanent(Url.Action("Web_Seo", "HomeSk"));
        }
        public ActionResult Web_BlogSk()
        {
            return RedirectPermanent(Url.Action("Web_Blog", "HomeSk"));
        }
        public ActionResult Web_EshopSk()
        {
            return RedirectPermanent(Url.Action("Web_Eshop", "HomeSk"));
        }
        public ActionResult Web_MicrositeSk()
        {
            return RedirectPermanent(Url.Action("Web_Microsite", "HomeSk"));
        }
        public ActionResult Web_CmsSk()
        {
            return RedirectPermanent(Url.Action("Web_Cms", "HomeSk"));
        }



        public ActionResult PortfolioEn()
        {
            return RedirectPermanent(Url.Action("Portfolio", "Home"));
        }
        public ActionResult ReprogrammingEn()
        {
            return RedirectPermanent(Url.Action("Reprogramming", "Home"));
        }
        public ActionResult WebEn()
        {
            return RedirectPermanent(Url.Action("Web", "Home"));
        }
        public ActionResult WindowsEn()
        {
            return RedirectPermanent(Url.Action("Windows", "Home"));
        }
        public ActionResult MobileEn()
        {
            return RedirectPermanent(Url.Action("Mobile", "Home"));
        }
        public ActionResult PriceEn()
        {
            return RedirectPermanent(Url.Action("Price", "Home"));
        }
        public ActionResult ContactEn()
        {
            return RedirectPermanent(Url.Action("Contact", "Home"));
        }
        public ActionResult ContactSendSuccessEn()
        {
            return RedirectPermanent(Url.Action("ContactSendSuccess", "Home"));
        }
        public ActionResult SiteMapEn()
        {
            return RedirectPermanent(Url.Action("SiteMap", "Home"));
        }
        public ActionResult LinksEn()
        {
            return RedirectPermanent(Url.Action("Links", "Home"));
        }

        public ActionResult En_WebApps()
        {
            return RedirectPermanent(Url.Action("Web", "Home"));
        }
    }
}
