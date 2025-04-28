using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Library_Management_System.Models;
using Library_Management_System.Manager;

namespace Library_Management_System.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            string loggedinUsername = Session["Username"] as string;
            ViewBag.LoggedInUsername = loggedinUsername;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login,string returnUrl)
        {

            if (ModelState.IsValid)
            {
                LoginManager obj = new LoginManager();
                bool isValid = obj.ValidateLogin(login.Username, login.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(login.Username, false);
                    Session["username"] = login.Username;
                    string loggedinUsername = Session["Username"] as string;
                    ViewBag.LoggedInUsername = loggedinUsername;
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "UserType");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }
    }
}