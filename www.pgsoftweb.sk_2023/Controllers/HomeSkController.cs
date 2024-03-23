using System.Collections.Generic;
using System.Web.Mvc;
using www.pgsoftweb.sk_2023.AppLib.Mail;
using www.pgsoftweb.sk_2023.AppLib.Template;
using www.pgsoftweb.sk_2023.Models;
using www.pgsoftweb.sk_2023.Models.Request;

namespace www.pgsoftweb.sk_2023.Controllers
{
    public class HomeSkController : _PgsoftwebController
    {
        public HomeSkController()
            : base("Home")
        {
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            SetLanguageAction("Index");
            SetMenuCssClass("Index");
            return View();
        }

        public ActionResult Portfolio()
        {
            SetLanguageAction("Portfolio");
            SetMenuCssClass("Portfolio", "Portfolio");
            return View();
        }

        public ActionResult Web()
        {
            SetLanguageAction("Web");
            SetMenuCssClass("Portfolio", "Web");
            return View();
        }

        public ActionResult Web_()
        {
            SetLanguageAction("Web_");
            SetMenuCssClass("Web", "Web_");
            return View();
        }

        public ActionResult Web_Seo()
        {
            SetLanguageAction("Web_Seo");
            SetMenuCssClass("Web", "Web_Seo");
            return View();
        }

        public ActionResult Web_Blog()
        {
            SetLanguageAction("Web_Blog");
            SetMenuCssClass("Web", "Web_Blog");
            return View();
        }

        public ActionResult Web_Eshop()
        {
            SetLanguageAction("Web_Eshop");
            SetMenuCssClass("Web", "Web_Eshop");
            return View();
        }

        public ActionResult Web_Microsite()
        {
            SetLanguageAction("Web_Microsite");
            SetMenuCssClass("Web", "Web_Microsite");
            return View();
        }

        public ActionResult Web_Cms()
        {
            SetLanguageAction("Web_Cms");
            SetMenuCssClass("Web", "Web_Cms");
            return View();
        }

        public ActionResult Web_Projects()
        {
            SetLanguageAction("Web_Projects");
            SetMenuCssClass("Web", "Web_Projects");
            return View();
        }

        public ActionResult Windows()
        {
            SetLanguageAction("Windows");
            SetMenuCssClass("Portfolio", "Windows");
            return View();
        }

        public ActionResult Price()
        {
            SetLanguageAction("Price");
            SetMenuCssClass("Price");
            return View();
        }

        public ActionResult Contact()
        {
            RequestSendModel_Sk model = new RequestSendModel_Sk();

            SetLanguageAction("Contact");
            SetMenuCssClass("Contact");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(RequestSendModel_Sk model)
        {
            SetLanguageAction("Contact");
            SetMenuCssClass("Contact");
            if (ModelState.IsValid)
            {
                bool robotOk = false;
                if (System.Configuration.ConfigurationManager.AppSettings["recaptchaVersion"] == "pgsoftweb")
                {
                    if (!new ApiKeyValidator().IsValid(model.Password, model.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "Musíte označiť, že nie ste robot.");
                        //Mailer.SendAdminMail("Odoslanie ROBOT správy",
                        //    string.Format("Meno: '{0}'\nEmail: '{1}'\nTelefón: '{2}'\nText: '{3}'",
                        //    model.Name,
                        //    model.Email,
                        //    model.Phone,
                        //    model.Text));

                        //return RedirectToAction("ContactSendSuccess", "HomeSk");
                    }
                    else
                    {
                        robotOk = true;
                    }
                }
                else
                {
                    if (!new GoogleRecaptcha().IsValidRecaptcha())
                    {
                        ModelState.AddModelError("", "Musíte označiť, že nie ste robot.");
                    }
                    else
                    {
                        robotOk = true;
                    }
                }

                if (robotOk)
                {
                    if (!string.IsNullOrEmpty(model.Captcha))
                    {
                        ModelState.AddModelError("", "Neplatná požiadavka.");
                        Mailer.SendAdminMail("Neplatná požiadavka",
                            string.Format("Meno: '{0}'\nEmail: '{1}'\nText: '{2}'\nCaptcha: '{3}'", model.Name, model.Email, model.Text, model.Captcha));
                    }
                    else
                    {
                        List<TextTemplateParam> paramList = new List<TextTemplateParam>();
                        paramList.Add(new TextTemplateParam("NAME", model.Name));
                        paramList.Add(new TextTemplateParam("EMAIL", model.Email));
                        paramList.Add(new TextTemplateParam("PHONE", model.Phone));
                        paramList.Add(new TextTemplateParam("TEXT", model.Text));
                        paramList.Add(new TextTemplateParam("PRICE", string.IsNullOrEmpty(model.Price) ? string.Empty : string.Format("Vaša predstava o cene: {0}", model.Price)));

                        // Odoslanie uzivatelovi
                        Mailer.SendMailTemplate(
                            "Odoslanie správy",
                            TextTemplate.GetTemplateText("ContactSendSuccess_Sk", paramList),
                            model.Email, "_Sk", null);

                        return RedirectToAction("ContactSendSuccess", "HomeSk");
                    }
                }
            }

            return View(model);
        }

        public ActionResult ContactSendSuccess()
        {
            SetLanguageAction("Contact");
            SetMenuCssClass("Contact");
            return View();
        }

        public ActionResult SiteMap()
        {
            SetLanguageAction("SiteMap");
            SetMenuCssClass("");
            return View();
        }

        public ActionResult Links()
        {
            SetLanguageAction("Links");
            SetMenuCssClass("");
            return View();
        }

        #region ebooks
        [HttpPost]
        public string RegisterMail(RegisterMailModel_Sk model)
        {
            System.Threading.Thread.Sleep(2000);

            if (ModelState.IsValid)
            {
                List<TextTemplateParam> paramList = new List<TextTemplateParam>();
                paramList.Add(new TextTemplateParam("EMAIL", model.Email));
                paramList.Add(new TextTemplateParam("EBOOK", "Web, ktorý zarába"));
                paramList.Add(new TextTemplateParam("TEXT",
                    "Online podnikanie sa rozširuje čoraz viac. Mnoho živnostníkov a podnikateľov si dalo vytvoriť webové stránky a eshopy v presvedčení, že im budú prinášať nových zákazníkov a nové objednávky. Aké je však ich rozčarovanie, keď zistia, že to nie je pravda a takmer nikto u nich cez web nenakupuje.<br /> Posielame vám e-book, ktorý vám vysvetlí, prečo to tak je a čo by ste mali robiť, aby váš web skutočne zarábal."
                    ));

                List<string> attList = new List<string>();
                attList.Add(MailAttachement.GetAttachementPath(null, "Ebook-Web-ktory-zaraba.pdf"));

                // Odoslanie uzivatelovi
                Mailer.SendMailTemplate(
                    "Váš e-book: Web, ktorý zarába",
                    TextTemplate.GetTemplateText("EbookSendSuccess_Sk", paramList),
                    model.Email, "_Sk", attList);

                return "OK";
            }

            return "ERR";
        }

        [HttpPost]
        public string RegisterMail_PerfectWebsite(RegisterMailModel_Sk model)
        {
            System.Threading.Thread.Sleep(2000);

            if (ModelState.IsValid)
            {
                List<TextTemplateParam> paramList = new List<TextTemplateParam>();
                paramList.Add(new TextTemplateParam("EMAIL", model.Email));
                paramList.Add(new TextTemplateParam("EBOOK", "Prvotriedny web"));
                paramList.Add(new TextTemplateParam("TEXT",
                    "Áno pekný vzhľad je dôležitý. Avšak, aby webová stránka plnila svoj cieľ a poslanie, tak iba pekný vzhľad nestačí. Niekedy môže byť krása dokonca na škodu. Tento ebook je návodom na kontrolu a zadanie požiadaviek pre tvorcu vášho nového webu, aby si zaslúžil vaše peniaze."
                    ));

                List<string> attList = new List<string>();
                attList.Add(MailAttachement.GetAttachementPath(null, "Ebook-Prvotriedny-web.pdf"));

                // Odoslanie uzivatelovi
                Mailer.SendMailTemplate(
                    "Váš e-book: Prvotriedny web",
                    TextTemplate.GetTemplateText("EbookSendSuccess_Sk", paramList),
                    model.Email, "_Sk", attList);

                return "OK";
            }

            return "ERR";
        }
        #endregion


        #region SEO actions
        public ActionResult SEO_AppDevelopment()
        {
            SetLanguageAction("SEO_AppDevelopment");
            SetMenuCssClass("Portfolio", "Portfolio");
            return View();
        }
        public ActionResult SEO_SoftDevelopment()
        {
            SetLanguageAction("SEO_SoftDevelopment");
            SetMenuCssClass("Portfolio", "Portfolio");
            return View();
        }
        public ActionResult SEO_WebDevelopment()
        {
            SetLanguageAction("SEO_WebDevelopment");
            SetMenuCssClass("Web", "Web_");
            return View();
        }
        #endregion

        #region GDPR
        public ActionResult OOU()
        {
            SetLanguageAction("Oou");
            SetMenuCssClass("");
            return View();
        }
        public ActionResult CancelContract()
        {
            SetLanguageAction("CancelContract");
            SetMenuCssClass("");
            return View();
        }
        public ActionResult TradeRules()
        {
            SetLanguageAction("TradeRules");
            SetMenuCssClass("");
            return View();
        }
        #endregion

        void SetMenuCssClass(string menuItemId, string subMenuItemId = null)
        {
            ViewBag.CssClassMenuIndex = "";
            ViewBag.CssClassMenuWeb = "";
            ViewBag.CssClassMenuPortfolio = "";
            ViewBag.CssClassMenuPrice = "";
            ViewBag.CssClassMenuContact = "";
            switch (menuItemId)
            {
                case "Index":
                    ViewBag.CssClassMenuIndex = "active";
                    break;
                case "Web":
                    ViewBag.CssClassMenuWeb = "active";
                    break;
                case "Portfolio":
                    ViewBag.CssClassMenuPortfolio = "active";
                    break;
                case "Price":
                    ViewBag.CssClassMenuPrice = "active";
                    break;
                case "Contact":
                    ViewBag.CssClassMenuContact = "active";
                    break;
            }

            ViewBag.CssClassSubMenuPortfolio = "";
            ViewBag.CssClassSubMenuWeb = "";
            ViewBag.CssClassSubMenuWindows = "";
            ViewBag.CssClassSubMenuWeb_ = "";
            ViewBag.CssClassSubMenuWeb_Microsite = "";
            ViewBag.CssClassSubMenuWeb_Blog = "";
            ViewBag.CssClassSubMenuWeb_Cms = "";
            ViewBag.CssClassSubMenuWeb_Eshop = "";
            ViewBag.CssClassSubMenuWeb_Seo = "";
            ViewBag.CssClassSubMenuWeb_Projects = "";
            switch (subMenuItemId)
            {
                case "Portfolio":
                    ViewBag.CssClassSubMenuPortfolio = "active";
                    break;
                case "Web":
                    ViewBag.CssClassSubMenuWeb = "active";
                    break;
                case "Windows":
                    ViewBag.CssClassSubMenuWindows = "active";
                    break;
                case "Mobile":
                    ViewBag.CssClassSubMenuMenuMobile = "active";
                    break;
                case "Web_":
                    ViewBag.CssClassSubMenuWeb_ = "active";
                    break;
                case "Web_Microsite":
                    ViewBag.CssClassSubMenuWeb_Microsite = "active";
                    break;
                case "Web_Blog":
                    ViewBag.CssClassSubMenuWeb_Blog = "active";
                    break;
                case "Web_Cms":
                    ViewBag.CssClassSubMenuWeb_Cms = "active";
                    break;
                case "Web_Eshop":
                    ViewBag.CssClassSubMenuWeb_Eshop = "active";
                    break;
                case "Web_Seo":
                    ViewBag.CssClassSubMenuWeb_Seo = "active";
                    break;
                case "Web_Projects":
                    ViewBag.CssClassSubMenuWeb_Projects = "active";
                    break;
            }
        }
    }
}
