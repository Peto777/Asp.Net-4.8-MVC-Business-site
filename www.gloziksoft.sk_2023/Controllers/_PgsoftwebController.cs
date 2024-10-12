using System.Web.Mvc;

namespace www.gloziksoft.sk_2023.Controllers
{
    public class _PgsoftwebController : Controller
    {
        private string controllerNameEn;
        private string controllerNameSk;

        public _PgsoftwebController(string aControllerName) : base()
        {
            this.controllerNameEn = aControllerName;
            this.controllerNameSk = string.Format("{0}Sk", aControllerName);
        }

        protected void SetLanguageAction(string actionName)
        {
            ViewBag.LangControllerEn = this.controllerNameEn;
            ViewBag.LangControllerSk = this.controllerNameSk;
            ViewBag.LangAction = actionName;
        }
    }
}