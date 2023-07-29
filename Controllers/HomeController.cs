using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLoginSession.Models;

namespace MVCLoginSession.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
                using (MVCAuthenticationEntities db = new MVCAuthenticationEntities())
                {
                    if (ModelState.IsValid)
                    {
                        bool IsValidUser = db.Users
                       .Any(u => u.Username.ToLower() == objUser
                       .Username.ToLower() && objUser
                       .Password == objUser.Password);

                        if (IsValidUser)
                        {
                            Session["UserID"] = objUser.Id.ToString();
                            Session["UserName"] = objUser.Username.ToString();
                        //    Session["Password"] = objUser.Password.ToString();
                        //if (Session["UserName"] != null && Session["Password"] != null)
                        //{
                            return RedirectToAction("UserDashBoard");
                       // }
                        }
                    }
                    ModelState.AddModelError("", "invalid Username or Password");
                    return View();
                }
            //return View();
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}