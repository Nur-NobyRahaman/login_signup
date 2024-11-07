using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult login()
        {
            return View();
        } public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string btnSubmit, string emailInput, string passwordInput)
        {
            string LoginMegs="";
            if(btnSubmit == "Login")
            {
                if (emailInput == "nurnoby@gmail.com" && passwordInput == "123456")
                {
                    Session["user"] = "nurnoby";
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    LoginMegs = "Failed, username/password not match";
                }
            }
            
           
            else if(btnSubmit =="Forget Password")
            {
                return RedirectToAction("forget", "Account");
            }
           ViewBag.LoginMegs = LoginMegs;
            return View();
        }

        public ActionResult forget() {
        return View();
        }
        [HttpPost]
        public ActionResult Logout() {
            Session["user"] = null;
        return RedirectToAction("login","Account");
        }
    }
}