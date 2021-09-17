using HospitalBedAndBloodManagementSystem.Models.ViewModels;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HospitalBedAndBloodManagementSystem.Controllers
{
    public class HBBSMainController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Login(string UserName,string Password,int UserType)
        {
            Models.BusinessAccess.UserMasterBusiness user = new Models.BusinessAccess.UserMasterBusiness();
            var userobj = new UserMasterViewModel();
            userobj = user.LoginUser(UserName, Password, UserType);

            return Ok(userobj);
        }

        [HttpPost]
        public IHttpActionResult UserMaster_Save(UserMasterViewModel userObj)
        {
            Models.BusinessAccess.UserMasterBusiness user = new Models.BusinessAccess.UserMasterBusiness();
            string msg = user.UserMaster_Save(userObj);
            msg = null;
            if (msg != null)
            {
               // userObj.Msg == msg;
                return Ok(userObj.Msg);
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IHttpActionResult App_HospitalMaster_GetByHospitalType( int HospitalType)
        {
            Models.BusinessAccess.HospitalBusinessAccess user = new Models.BusinessAccess.HospitalBusinessAccess();
           HospitalViewModel hosobj = new HospitalViewModel();
           DataTable dt = user.App_HospitalMaster_GetByHospitalType(HospitalType);
            if (dt != null && dt.Rows.Count > 0)
            {
                hosobj.HospitalList = dt.AsEnumerable().Select(dr => new HospitalViewModel
                {
                    HospitalId = (dr["HospitalId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HospitalId"]),
                    Hospital = (dr["HospitalName"] == DBNull.Value) ? "" : dr["HospitalName"].ToString(),
                    DistrictId = (dr["DistrictId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DistrictId"]),
                    Address = (dr["Address"] == DBNull.Value) ? "" : dr["Address"].ToString(),
                    Pincode = (dr["Pin"] == DBNull.Value) ? "" : dr["Pin"].ToString(),
                    RegistrationNo = (dr["RegistrationNo"] == DBNull.Value) ? "" : dr["RegistrationNo"].ToString(),
                    ContactNo = (dr["ContactNo"] == DBNull.Value) ? "" : dr["ContactNo"].ToString(),
                    LandLineNo = (dr["LandLineNo"] == DBNull.Value) ? "" : dr["LandLineNo"].ToString(),
                    City = (dr["City"] == DBNull.Value) ? "" : dr["City"].ToString(),
                    EmailID = (dr["Email"] == DBNull.Value) ? "" : dr["Email"].ToString(),
                    HospitalTypeId = (dr["HospitalType"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HospitalType"]),
                    HospitalTypeName = (dr["HospitalTypeName"] == DBNull.Value) ? "" : dr["HospitalTypeName"].ToString(),
                }).ToList();

                //return Ok(user.comment);
            }
            else
            {
                return BadRequest("No Data Found");
            }

            return Ok(hosobj.HospitalList);
        }
    }
}
