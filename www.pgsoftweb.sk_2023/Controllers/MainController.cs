using System.Web.Mvc;

namespace www.pgsoftweb.sk_2023.Controllers
{
    public class MainController : _PgsoftwebController
    {
        public MainController()
            : base("Main")
        {
        }

        //
        // GET: /Main/
        public ActionResult Index()
        {
            ViewBag.LangControllerEn = "Home";
            ViewBag.LangControllerSk = "HomeSk";
            ViewBag.LangAction = "Index";

            return View();
        }
    }
}