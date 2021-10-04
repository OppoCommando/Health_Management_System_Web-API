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
    public class MyNoteBookController : ApiController
    {
        [HttpPost]
        public IHttpActionResult UserMaster_Register(UserMasterViewModel userObj)
        {
            Models.BusinessAccess.UserMasterBusiness user = new Models.BusinessAccess.UserMasterBusiness();
            UserMasterViewModel userResponse = new UserMasterViewModel();
            userResponse = user.UserMaster_Register(userObj);
           
            if (userResponse.Msg != null)
            {
                // userObj.Msg == msg;
                return Ok(userResponse);
            }

            else
            {
                return BadRequest();
            }
        }
    }
}