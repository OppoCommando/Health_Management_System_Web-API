using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using System.Net.Http;

//using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace HospitalBedAndBloodManagementSystem.Controllers
{
    public class ApiMainController : ApiController
    {
        // GET: ApiMain
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("value");
        }
        [HttpGet]
        public IHttpActionResult Login()
        {
            return Ok("Success");
        }
       
    }
}