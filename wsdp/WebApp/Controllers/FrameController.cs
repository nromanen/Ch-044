using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class FrameController : Controller
    {
        // GET: Frame
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IFrame()
        {
            return View();
        }
    }
}