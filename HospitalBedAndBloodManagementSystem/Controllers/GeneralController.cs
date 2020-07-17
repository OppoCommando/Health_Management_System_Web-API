using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalBedAndBloodManagementSystem.Models;

namespace HospitalBedAndBloodManagementSystem.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            GeneralViewModel gen = new GeneralViewModel();
           
            return View("GeneralRegistration", gen);
        }
        public ActionResult SaveRegistration(GeneralViewModel gvm)
        {
            int Res = 0;
            gvm.UserTypeId = 2;
            Res = gvm.SaveRegistrationDetails();
            GeneralViewModel gvm2 = new GeneralViewModel();
            if (Res >0)
            {
                gvm2.Msg = "Registration Done Successfully.Thanks for registering.You can login now...";
                return View("GeneralRegistration", gvm2);
            }
            else
            {
                gvm2.Msg = "Registration Can not be done.Some problem occurs.Please Try Later..";
                return View("GeneralRegistration", gvm2);
            }
        }

        [Authorize(Roles = "1")]
        public ActionResult HospitalMemberDetails()
        {
            GeneralViewModel gen = new GeneralViewModel();
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"]) == 1)
                gen.getAllUserDetailsByUserTypeIdForADMIN(3);
            else
                gen.getAllUserDetailsByUserTypeId(3);
            gen.UserTypeId = 3;
            gen.UserTypeName = "Hospital Member";
            gen.UserFieldName = "Hospital";
            if (int.Parse(Session["UserTypeId"].ToString()) == 3 || int.Parse(Session["UserTypeId"].ToString()) == 1)
               return View("UserDetails", gen);
            else
                return View("UnauthorizedPage");
        }

        [Authorize(Roles = "1")]
        public ActionResult BloodBankMemberDetails()
        {
            GeneralViewModel gen = new GeneralViewModel();
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"]) == 1)
                gen.getAllUserDetailsByUserTypeIdForADMIN(4);
            else
                gen.getAllUserDetailsByUserTypeId(4);
            gen.UserTypeId = 4;
            gen.UserTypeName = "Blood Bank Member";
            gen.UserFieldName = "Blood Bank";
            if (int.Parse(Session["UserTypeId"].ToString()) == 4 || int.Parse(Session["UserTypeId"].ToString()) == 1)
                return View("UserDetails", gen);
            else
                return View("UnauthorizedPage");
        }

        [Authorize(Roles = "1")]
        public ActionResult GeneralUserDetails()
        {
            GeneralViewModel gen = new GeneralViewModel();
            if(Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"])==1)
            gen.getAllUserDetailsByUserTypeIdForADMIN(2);
            else
            gen.getAllUserDetailsByUserTypeIdForADMIN(0);
            gen.UserTypeId = 2;
            gen.UserTypeName = "Genneral User";
            gen.UserFieldName = "NA";
            if (int.Parse(Session["UserTypeId"].ToString()) == 1)
                return View("UserDetails", gen);
            else
                return View("UnauthorizedPage");
        }

        [Authorize(Roles = "1")]
        public ActionResult MakeHospitalAdmin(int Id)
        {
            GeneralViewModel gen = new GeneralViewModel();
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"]) == 1)
            { 
                gen.MakeHospitalAdmin(Id);
            }
            gen.Msg = "The User has been successfully made Hospital Admin";
            gen.UserTypeId = 3;
            gen.UserTypeName = "Hospital Member";
            gen.UserFieldName = "Hospital";
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"]) == 1)
                gen.getAllUserDetailsByUserTypeIdForADMIN(3);
            else
                gen.getAllUserDetailsByUserTypeId(3);
            if (int.Parse(Session["UserTypeId"].ToString()) == 3)
                return View("UserDetails", gen);
            else
                return View("UnauthorizedPage");
        }

        [Authorize(Roles = "1")]
        public ActionResult MakeBloodBankAdmin(int Id)
        {
            GeneralViewModel gen = new GeneralViewModel();
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"]) == 1)
            {
                gen.MakeBloodBankAdmin(Id);
            }
            gen.Msg = "The User has been successfully made Blood Bank Admin";
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"]) == 1)
                gen.getAllUserDetailsByUserTypeIdForADMIN(4);
            else
               gen.getAllUserDetailsByUserTypeId(4);
            gen.UserTypeId = 4;
            gen.UserTypeName = "Blood Bank Member";
            gen.UserFieldName = "Blood Bank";
            return View("UserDetails", gen);
        }

        [Authorize(Roles = "1")]
        public ActionResult MakeHospitalMember(int Id)
        {
            GeneralViewModel gen = new GeneralViewModel();
            if (int.Parse(Session["UserTypeId"].ToString()) == 3 || int.Parse(Session["UserTypeId"].ToString()) == 1)
            {
                gen.MakeHospitalMember(Id);
            }
            gen.Msg = "The User has been successfully made Hospital Member";
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"]) == 1)
                gen.getAllUserDetailsByUserTypeIdForADMIN(3);
            else
               gen.getAllUserDetailsByUserTypeId(3);
            gen.UserTypeId = 3;
            gen.UserTypeName = "Hospital Member";
            gen.UserFieldName = "Hospital";
            return View("UserDetails", gen);
        }

        [Authorize(Roles = "1")]
        public ActionResult MakeBloodBankMember(int Id)
        {
            GeneralViewModel gen = new GeneralViewModel();
            if (int.Parse(Session["UserTypeId"].ToString()) == 4 || int.Parse(Session["UserTypeId"].ToString()) == 1)
            {
                gen.MakeBloodBankMember(Id);
            }
            gen.Msg = "The User has been successfully made Blood Bank Member";
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["UserTypeId"]) == 1)
                gen.getAllUserDetailsByUserTypeIdForADMIN(4);
            else
                gen.getAllUserDetailsByUserTypeId(4);

            gen.UserTypeId = 4;
            gen.UserTypeName = "Blood Bank Member";
            gen.UserFieldName = "Blood Bank";
            return View("UserDetails", gen);
        }

        [AllowAnonymous]
        public ActionResult PatientRegistration()
        {
            PatientViewModel gen = new PatientViewModel();
            gen.GetAllPatientDetailsByUserId();
            return View("Patient", gen);
        }



        [Authorize]
        public ActionResult Booking(int PatientId)
        {
            PatientViewModel gen = new PatientViewModel();

            return View("Patient", gen);
        }

        
        
        
        
        // This action handles the form POST and the upload
        [HttpPost]
        public ActionResult PatientRegistration(PatientViewModel pvm,HttpPostedFileBase Prescribtion)
        {
            // Verify that the user selected a file
            if (Prescribtion != null && Prescribtion.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(Prescribtion.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Prescriptions"), fileName);
                Prescribtion.SaveAs(path);
                pvm.Prescribtion = "Prescriptions/" + fileName;
            }
            else
            {
                pvm.Prescribtion = "";

            }
            
            
            int res = pvm.Save();
            if(res!=0)
            {
                pvm.Msg = "Patient Details Saved Successfully";
            }
            else
            {
                pvm.Msg = "Patient Details Cannot be saved";
            }
            pvm.GetAllPatientDetailsByUserId();

            // redirect back to the index action to show the form once again
            return View("PatientRegistration",pvm);
        }

    }
}