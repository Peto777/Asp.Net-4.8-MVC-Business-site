using System.Collections.Generic;
using System.Web.Mvc;
using www.pgsoftweb.sk_2023.AppLib.Mail;
using www.pgsoftweb.sk_2023.AppLib.Template;
using www.pgsoftweb.sk_2023.Models;
using www.pgsoftweb.sk_2023.Models.Request;

namespace www.pgsoftweb.sk_2023.Controllers
{
    public class HomeController : _PgsoftwebController
    {
        public HomeController()
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

        public ActionResult Web_()
        {
            SetLanguageAction("Web_");
            return RedirectToAction("Web");
        }

        public ActionResult Web()
        {
            SetLanguageAction("Web");
            SetMenuCssClass("Portfolio", "Web");
            return View();
        }

        public ActionResult Web_Seo()
        {
            SetLanguageAction("Web_Seo");
            return RedirectToAction("Web");
        }

        public ActionResult Web_Blog()
        {
            SetLanguageAction("Web_Blog");
            return RedirectToAction("Web");
        }

        public ActionResult Web_Eshop()
        {
            SetLanguageAction("Web_Eshop");
            return RedirectToAction("Web");
        }

        public ActionResult Web_Microsite()
        {
            SetLanguageAction("Web_Microsite");
            return RedirectToAction("Web");
        }

        public ActionResult Web_Cms()
        {
            SetLanguageAction("Web_Cms");
            return RedirectToAction("Web");
        }

        public ActionResult Web_Projects()
        {
            SetLanguageAction("Web_Projects");
            SetMenuCssClass("Portfolio", "Web");
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
            RequestSendModel_En model = new RequestSendModel_En();

            SetLanguageAction("Contact");
            SetMenuCssClass("Contact");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(RequestSendModel_En model)
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
                        ModelState.AddModelError("", "You must check that you are not a robot.");
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
                        ModelState.AddModelError("", "You must check that you are not a robot.");
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
                        ModelState.AddModelError("", "Invalid request.");
                        Mailer.SendAdminMail("Invalid request",
                            string.Format("Name: '{0}'\nEmail: '{1}'\nText: '{2}'\nCaptcha: '{3}'", model.Name, model.Email, model.Text, model.Captcha));
                    }
                    else
                    {
                        List<TextTemplateParam> paramList = new List<TextTemplateParam>();
                        paramList.Add(new TextTemplateParam("NAME", model.Name));
                        paramList.Add(new TextTemplateParam("EMAIL", model.Email));
                        paramList.Add(new TextTemplateParam("TEXT", model.Text));

                        // Odoslanie uzivatelovi
                        Mailer.SendMailTemplate(
                            "Your message",
                            TextTemplate.GetTemplateText("ContactSendSuccess_En", paramList),
                            model.Email, "_En", null);

                        return RedirectToAction("ContactSendSuccess", "Home");
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

        #region SEO actions
        public ActionResult SEO_AppDevelopment()
        {
            SetLanguageAction("SEO_AppDevelopment");
            return RedirectToAction("Portfolio");
        }
        public ActionResult SEO_SoftDevelopment()
        {
            SetLanguageAction("SEO_SoftDevelopment");
            return RedirectToAction("Portfolio");
        }
        public ActionResult SEO_WebDevelopment()
        {
            SetLanguageAction("SEO_WebDevelopment");
            return RedirectToAction("Web");
        }
        #endregion

        void SetMenuCssClass(string menuItemId, string subMenuItemId = null)
        {
            ViewBag.CssClassMenuIndex = "";
            ViewBag.CssClassMenuPortfolio = "";
            ViewBag.CssClassMenuPrice = "";
            ViewBag.CssClassMenuContact = "";
            switch (menuItemId)
            {
                case "Index":
                    ViewBag.CssClassMenuIndex = "active";
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
            ViewBag.CssClassSubMenuMenuMobile = "";
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
            }
        }
    }
}