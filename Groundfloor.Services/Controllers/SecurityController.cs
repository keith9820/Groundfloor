using System;
using System.Configuration;
using System.Web.Mvc;
using Groundfloor.Security;

namespace GroundFloor.Services.Controllers
{
    public class SecurityController : Controller
    {
        public ActionResult Encrypt(string data, string secret, bool urlencode = true)
        {
            var result = Encryptor.Encrypt64(data, secret);

            if (urlencode)
                result = result.EncodeUrl();

            return Json(result, "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Decrypt(string data, string secret, bool urldecode = true)
        {
            if (urldecode)
                data = data.DecodeUrl();

            return Json(data.Decrypt64(secret), "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetToken(bool urlencode = true)
        {
            string secret = ConfigurationManager.AppSettings["AppSecret"];
            string data = DateTime.Now.ToUniversalTime().ToString();
            return Json(Encrypt(data, secret, urlencode), "text/plain", JsonRequestBehavior.AllowGet);
        }
    }
}
