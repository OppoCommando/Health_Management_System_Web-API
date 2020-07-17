using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalBedAndBloodManagementSystem.Models;


namespace HospitalBedAndBloodManagementSystem.Controllers
{
    public class BloodBankController : Controller
    {
        // GET: BloodBank
        public ActionResult Index()
        {
            
            return View();
        }





        [Authorize(Roles = "1")]
        public ActionResult Registration()
        {
            BloodBankViewModel obj = new BloodBankViewModel();
            if (Convert.ToInt32(Session["BloodbankId"]) == 0)
                obj.GetAllBloodBank();
            else
                obj.GetBloodBankById(Convert.ToInt32(Session["BloodbankId"]));
            return View("BloodBankRegistrationPage",obj);
           
        }
        [AllowAnonymous]
        public ActionResult MemberRegistration()
        {
            GeneralViewModel obj = new GeneralViewModel();
            obj.UserTypeId = 4;
            return View("BloodBankMemberRegistration",obj);
          
        }

        [Authorize(Roles = "1")]
        public ActionResult Save(BloodBankViewModel bvm)
        {
            int res = bvm.Save();
          
            BloodBankViewModel bvm2 = new BloodBankViewModel();
            if (res != 0)
                bvm2.Msg = "Blood Bank Saved Successfully";
            else
                bvm2.Msg = "Blood Bank Can not be Saved ";

            if (Convert.ToInt32(Session["BloodbankId"]) == 0)
                bvm2.GetAllBloodBank();
            else
                bvm2.GetBloodBankById(Convert.ToInt32(Session["BloodbankId"]));

            return View("BloodBankRegistrationPage", bvm2);
        }
        [AllowAnonymous]
        public ActionResult SaveRegistration(GeneralViewModel gvm)
        {
            int Res = 0;
            gvm.UserTypeId = 4;
            Res = gvm.SaveRegistrationDetails();
            GeneralViewModel gvm2 = new GeneralViewModel();
            if (Res > 0)
            {
                gvm2.Msg = "Registration Done Successfully.After the Blood Bank Admin Authorisation you can login.Thank you for registering...";
                return View("BloodBankMemberRegistration", gvm2);
            }
            else
            {
                gvm2.Msg = "Registration Can not be done.Some problem occurs.Please Try Later..";
                return View("BloodBankMemberRegistration", gvm2);
            }
        }

        [Authorize(Roles = "1,2")]
        public ActionResult BloodBankInfo()
        {
            BloodBankInfoModel bim = new BloodBankInfoModel();
            bim.GetBloodInfo();
            return View("BloodBankInformationPage", bim);
        }

        [Authorize(Roles = "1,2")]
        public ActionResult SaveBloodInfo(BloodBankInfoModel obj)
        {
            int res = 0;
            
            res = obj.SaveBloodInfo();
            BloodBankInfoModel obj2 = new BloodBankInfoModel();
            if (res != 0)
                obj2.Msg = "Blood Info Updated Successfully";
            else
                obj2.Msg = "Blood Info can not ne Updated. Some problem Occurs.";
            obj2.GetBloodInfo();
            return View("BloodBankInformationPage",obj2);
        }

        [Authorize(Roles = "1")]
        public ActionResult EditBloodBank(int Id)
        {

            BloodBankViewModel obj = new BloodBankViewModel();
            obj.GetBloodBankById(Id);
            return View("BloodBankRegistrationPage", obj);
        }

        [Authorize(Roles = "1")]
        public ActionResult DeleteBloodBank(int Id)
        {
            int Result = 0;
            BloodBankViewModel obj = new BloodBankViewModel();
            Result = obj.Delete(Id);
            if (Result != Id)
                obj.Msg = "Blood Bank Deleted successfully";
            else
                obj.Msg = "Blood Bank can not be Deleted";
            obj.GetAllBloodBank();
            return View("BloodBankRegistrationPage", obj);
        }
    }
}