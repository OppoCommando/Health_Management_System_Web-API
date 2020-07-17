using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalBedAndBloodManagementSystem.Models;

namespace HospitalBedAndBloodManagementSystem.Controllers
{
    public class HospitalController : Controller
    {
        // GET: Hospital
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "1")]
        public ActionResult Registration()
        {
            HospitalViewModel hvm = new HospitalViewModel();

            if (Convert.ToInt32(Session["HospitalId"]) == 0)
                hvm.GetAllHospital();
            else
                hvm.GetHospitalById(Convert.ToInt32(Session["HospitalId"]));

            return View("HospitalRegistration",hvm);
        }
        [AllowAnonymous]
        public ActionResult MemberRegistration()
        {
            GeneralViewModel obj = new GeneralViewModel();
            obj.UserTypeId = 3;
            return View("HospitalMemberRegistration",obj);
        }

        [Authorize(Roles = "1")]
        public ActionResult Save(HospitalViewModel hvm)
        {
            int res = hvm.Save();
            HospitalViewModel hvm2 = new HospitalViewModel();
            if (res != 0)
                hvm2.Msg = "Hospital Saved Successfully";
            else
                hvm2.Msg = "Hospital Can not be Saved ";

            if (Convert.ToInt32(Session["HospitalId"]) == 0)
                hvm2.GetAllHospital();
            else
                hvm2.GetHospitalById(Convert.ToInt32(Session["HospitalId"]));

            return View("HospitalRegistration", hvm2);
        }

        [AllowAnonymous]
        public ActionResult SaveRegistration(GeneralViewModel gvm)
        {
            int Res = 0;
            gvm.UserTypeId = 3;
            Res = gvm.SaveRegistrationDetails();
            GeneralViewModel gvm2 = new GeneralViewModel();
            if (Res > 0)
            {
                gvm2.Msg = "Registration Done Successfully.After the Hospital Admin Authorisation you can login.Thank you for registering...";
                return View("HospitalMemberRegistration", gvm2);
            }
            else
            {
                gvm2.Msg = "Registration Can not be done.Some problem occurs.Please Try Later..";
                return View("HospitalMemberRegistration", gvm2);
            }
        }

        [Authorize(Roles = "1,2")]
        public ActionResult BedInfo()
        {
            HospitalInfoModel bim = new HospitalInfoModel();
            bim.GetBedInfo();
            return View("HospitalInformationPage", bim);
        }

        [Authorize(Roles = "1,2")]
        public ActionResult SaveBedInfo(HospitalInfoModel bim)
        {
            int res = 0;
            res = bim.SaveBedInfo();
            HospitalInfoModel obj2 = new HospitalInfoModel();
            if (res != 0)
                obj2.Msg = "Bed Info Updated Successfully";
            else
                obj2.Msg = "Bed Info can not ne Updated. Some problem Occurs.Please Try again later...";
            obj2.GetBedInfo();

            return View("HospitalInformationPage", obj2);
        }

        [Authorize(Roles = "1")]
        public ActionResult EditHospital(int Id)
        {

            HospitalViewModel obj = new HospitalViewModel();
            obj.GetHospitalById(Id);
            return View("HospitalRegistration", obj);
        }

        [Authorize(Roles = "1")]
        public ActionResult DeleteHospital(int Id)
        {
            int Result = 0;
            HospitalViewModel obj = new HospitalViewModel();
            Result = obj.Delete(Id);
            if (Result != Id)
                obj.Msg = "Hospital Deleted successfully";
            else
                obj.Msg = "Hospital can not be Deleted.Dependency exists.";
            obj.GetAllHospital();
            return View("HospitalRegistration", obj);
        }







    }
}