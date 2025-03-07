﻿using System;
using System.Collections.Generic;
using System.Data;
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
            if (Session["Role"].ToString() == "Admin")
            {

                List<BaseEquipment> plsData = BaseEquipment.ListEquipmentData();
                DataTable dtCustomerEquip = BaseCustomer.ListCustomerEquipment();
                ViewBag.dtCustomerEquip = dtCustomerEquip;
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
            if (btnSubmit == "Save Customer")
            {
                BaseCustomer customer = new BaseCustomer();
                customer.CustomerName = frm["textCustomerName"].Trim().ToString();
                customer.CustomerMobile = frm["textCustomerPhoneNumber"].Trim().ToString();
                int result = customer.SaveCustomer();
                if (result > 0)
                {
                    ViewBag.OperationResult = "Save Customer Successfully";
                }
            }
            if(btnSubmit == "Save Assignment")
            {
                int customerId = Convert.ToInt32(frm["ddlPartialCustomerId"].ToString());
                int equipmentId = Convert.ToInt32(frm["ddlAssignEquipmentId"].ToString()); 
                int equipCount = Convert.ToInt32(frm["textPartialEqipQuantity"].ToString());
                BaseCustomer customer = new BaseCustomer();
               int result = customer.EquipCustomerAssign(customerId, equipmentId, equipCount);
                if (result > 0)
                {
                    ViewBag.OperationResult = "Save Customer Assign Successfully";
                }
            }
            List<BaseEquipment> plsData = BaseEquipment.ListEquipmentData();
            ViewBag.plsData = plsData;
            DataTable dtCustomerEquip = BaseCustomer.ListCustomerEquipment();
            ViewBag.dtCustomerEquip = dtCustomerEquip;
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

        // internal api
        [HttpGet]
        public ActionResult LstEquipment()
        {
            List<BaseEquipment> plsData = BaseEquipment.ListEquipmentData();
            // linku 
            var plist = (from e in plsData select new
            {
                EuipmentId= e.EquipmentId,
                Name = e.Name,
                Quantity = e.EqCount.ToString(),
                EntryDate = e.EntryDate.ToString("MM-dd-yyyy"),
            }).ToList();
            return Json(plist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LstCustomer()
        {
            List<BaseCustomer> plsCustomer = BaseCustomer.ListCustomer();
           

            return Json(plsCustomer, JsonRequestBehavior.AllowGet);
        }
    }
}