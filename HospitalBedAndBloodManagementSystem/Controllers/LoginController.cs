using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalBedAndBloodManagementSystem.Models;
using HospitalBedAndBloodManagementSystem.Controllers;
using System.Web.Security;

namespace HospitalBedAndBloodManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            LoginViewModel lvm = new LoginViewModel();

            return View("LoginPage",lvm);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AuthenticateLogin(LoginViewModel lvm)
        {

            string u = lvm.UserName;
            string p = lvm.Password;
         
            lvm.AuthenticateUser(u);

            if (lvm != null && lvm.UserId!=0 )
            {
                if (lvm.Password == p)
                {
                    int UserId = lvm.UserId;
                    if(lvm.UserTypeId==1)
                    {
                        lvm.Role = 1;
                    }
                    //FormsAuthentication.SetAuthCookie(EmployeeId.ToString(), true);
                    FormsAuthenticationTicket Authticket = new FormsAuthenticationTicket(
                                                               1,
                                                               UserId.ToString() + "," + lvm.Name,
                                                               DateTime.Now,
                                                               DateTime.Now.AddMinutes(60),
                                                               false,
                                                               lvm.Role.ToString());
                    string hash = FormsAuthentication.Encrypt(Authticket);
                    HttpCookie Authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (Authticket.IsPersistent)
                        Authcookie.Expires = Authticket.Expiration;
                    Response.Cookies.Add(Authcookie);
                    Session["UserId"] = UserId;
                    Session["UserTypeId"] = lvm.UserTypeId;
                    Session["HospitalId"] = lvm.HospitalId;
                    Session["BloodBankId"] = lvm.BloodBankId;
                    Session.Timeout = 60;
                    //Response.Redirect(@"~/admin/Default.aspx");
                    if(lvm.UserTypeId==2)
                        return RedirectToAction("GeneralUserDashboard", "Home");
                    else if (lvm.UserTypeId == 4)
                        return RedirectToAction("BloodBankMemberDashboard", "Home");
                    else if (lvm.UserTypeId == 3)
                       return RedirectToAction("HospitalMemberDashboard", "Home");
                    else
                        return RedirectToAction("AdminDashboard", "Home");
                   
                }
                else
                {

                    lvm.Msg = "Invalid Password";

                    return View("LoginPage", lvm);
                   
                }
            }
            else
            {



                lvm.Msg = "Invalid UserId Or Password";

                return View("LoginPage", lvm);

            }



        }


        public ActionResult forgotpassword(string UserName)
        {
            LoginViewModel lvm = new LoginViewModel();
            lvm.Msg = "Your password has been sent to your registered email and mobile number";
            return View("LoginPage",lvm);
        }
       [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserId"] = "0";
            Session["UserTypeId"] = "0";
            Session["HospitalId"] = "0";
            Session["BloodBankId"] = "0";
            return RedirectToAction("Index");
        }



    }
}