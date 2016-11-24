using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Change(string language, string url)
        {
            if (language != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = language;
            Response.Cookies.Add(cookie);

            return Redirect(url);
        }

    }
}