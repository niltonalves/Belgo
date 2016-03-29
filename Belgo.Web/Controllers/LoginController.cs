using Belgo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Belgo.Web.Controllers
{
    public class LoginController : Controller
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (FormsAuthentication.Authenticate(model.Email, model.Senha))
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public ActionResult Sair() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
