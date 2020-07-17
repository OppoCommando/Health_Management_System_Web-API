using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalBedAndBloodManagementSystem.Models;

namespace HospitalBedAndBloodManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            LoginViewModel lvm = new LoginViewModel
            {
                Msg = "Invalid UserId/Password"
            };
            return View("LoginPage", lvm);
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

        [Authorize(Roles = "1")]
        public ActionResult District()
        {

            District obj = new District();
            obj.GetAllDistrict();
            return View("DistrictMaster", obj);
        }

        [Authorize(Roles = "1")]
        public ActionResult Save(District obj)
        {
            int result = obj.Save();
            if (result != 0)
            {
                if (obj.DistrictId == 0)
                    obj.Msg = "District Saved successfully";
                else
                    obj.Msg = "District Updated successfully";
            }
            else
            {
                if (obj.DistrictId == 0)
                    obj.Msg = "District can not be Saved";
                else
                    obj.Msg = "District can not be Updated";
            }
            obj.GetAllDistrict();
            return View("DistrictMaster", obj);

        }
        [Authorize(Roles = "1")]
        public ActionResult EditDistrict(int Id)
        {

            District obj = new District();
            obj.GetDistrictById(Id);
            return View("DistrictMaster", obj);
        }

        [Authorize(Roles = "1")]
        public ActionResult DeleteDistrict(int Id)
        {
            int Result = 0;
            District obj = new District();
            Result = obj.Delete(Id);
            if (Result != Id)
                obj.Msg = "District Deleted successfully";
            else
                obj.Msg = "District can not be Deleted";
            obj.GetAllDistrict();
            return View("DistrictMaster", obj);
        }



        [Authorize(Roles = "1")]
        public ActionResult AdminDashboard()
        {
            if (int.Parse(Session["UserTypeId"].ToString()) == 1)
                return View("AdminDashboard");
            else
                return View("UnauthorizedPage");
        }

        [Authorize(Roles = "0")]
        public ActionResult GeneralUserDashboard()
        {
            return View("GeneralUserDashboard");
        }

        [Authorize(Roles = "1,2") ]
        public ActionResult HospitalMemberDashboard()
        {
            if(int.Parse(Session["UserTypeId"].ToString())==3)
                return View("HospitalMemberDashboard");
            else
                return View("UnauthorizedPage");
        }

        [Authorize(Roles = "1,2")]
        public ActionResult BloodBankMemberDashboard()
        {
            if (int.Parse(Session["UserTypeId"].ToString()) == 4)
                return View("BloodBankMemberDashboard");
            else
                return View("UnauthorizedPage");
        }

        public ActionResult Unauthorized()
        {
            return View("UnauthorizedPage");
        }

        public ActionResult Help()
        {
            return View("Help");
        }
        public ActionResult ContactUs()
        {
            return View("Contact");
        }


    }
}