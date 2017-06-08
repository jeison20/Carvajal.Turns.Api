using Component;
using System.Configuration;
using System.Web.Mvc;

namespace Carvajal.Turns.Api.Controllers
{
    public class DoLoginController : Controller
    {
        // GET: DoLogin
        public ActionResult Index(string msg)
        {
            string Url;
            if (!msg.Contains("Barer "))
            {
                Url = (ConfigurationManager.AppSettings["UrlSitio"] + "?msg=" + msg);
            }
            else
            {
                if (CClient.Instance.ValidToken(msg))
                {
                    Url = (ConfigurationManager.AppSettings["UrlSitio"] + "?msg=" + msg);
                }
                else
                {
                    Url = (ConfigurationManager.AppSettings["UrlSitio"] + "?msg=Error acceso denegado ");
                }
            }

            return Content("<script> window.location = '" + Url + "'" + "</script>");
        }
    }
}