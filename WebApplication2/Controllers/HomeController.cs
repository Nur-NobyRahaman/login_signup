using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            if (Session["user"] != null)
            {

                List<BaseEquipment> plsData = BaseEquipment.ListEquipmentData();
                ViewBag.plsData = plsData;
                ViewBag.txtName = "";
                return View();
            }
            else
            {
                return RedirectToAction("login", "Account");
            }
        }
        [HttpPost]
        public ActionResult Dashboard(FormCollection frm, string btnSubmit)
        {
            if(btnSubmit== "Save Equipment")
            {
                BaseEquipment baseEquipment = new BaseEquipment();
                baseEquipment.Name = frm["textEquipmentName"].Trim().ToString();
                 baseEquipment.EqCount =Convert.ToInt32(frm["textQuantity"].ToString());
                baseEquipment.EntryDate = Convert.ToDateTime(frm["textEntryDate"].ToString());
                int result = baseEquipment.saveEquipment();
                if (result > 0) {
                    ViewBag.OperationResult = "Save Successfully";
                }
            }
            List<BaseEquipment> plsData = BaseEquipment.ListEquipmentData();
            ViewBag.plsData = plsData;
            ViewBag.txtName = "";
            if (btnSubmit == "Search")
            ViewBag.txtName = frm["txtName"].Trim();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}