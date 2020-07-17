using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalBedAndBloodManagementSystem.Models.ViewModels
{
    public class UserMasterViewModel
    {
        public int UserId { get; set; }

        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public string UserFieldName { get; set; }
        public string GeneralUser { get; set; }
        public int DistrictId { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Msg { get; set; }
        public int HospitalId { get; set; }
        public int BloodBankId { get; set; }
        public int GenderId { get; set; }

        public int IsHospitalMember { get; set; }

        public int IsBloodBankMember { get; set; }

        public int IsAuthorised { get; set; }
        public string GenderName { get; set; }

        public string Address { get; set; }
        public DateTime DOB { get; set; }

        public string DeviceId { get; set; }

        public int Role { get; set; }
    }
}