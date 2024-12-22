using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

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
        public ActionResult login(string btnSubmit, BaseAccount baseAccount)
        {
            string LoginMegs="";
            if (baseAccount.UserName != null && baseAccount.Password != null)
            {
               
                if (btnSubmit == "Login")
                {
                    bool verifyStatus = baseAccount.VerifyLogin();
                    if (verifyStatus)
                    {
                        Session["user"] = baseAccount.UserName;
                        LoginMegs = "Login Successfully";
                        //return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        LoginMegs = "Failed, username/password not match";
                    }
                }
            }
            if (baseAccount.UserName == null && baseAccount.Password == null)
            {
                LoginMegs = "Enter valid userName and password";
            }




                if (btnSubmit =="Forget Password")
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