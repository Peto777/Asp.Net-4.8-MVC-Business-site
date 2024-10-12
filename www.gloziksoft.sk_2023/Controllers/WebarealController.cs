using System.Web.Mvc;

namespace www.gloziksoft.sk_2023.Controllers
{
    public class WebarealController : Controller
    {
        public ActionResult RegisterWebarealSk()
        {
            return RedirectPermanent("https://www.webareal.sk/registration.html?a_box=7yv8ebe7&a_cam=2");
        }
    }
}
