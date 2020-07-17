using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalBedAndBloodManagementSystem.Models
{
    public class BookingModel
    {
        public int  BookingRequestId { get; set; }

        public int PatiendId { get; set; }

        public int RequestedHospitalId { get; set; }

        public int RequestedBloodBankId { get; set; }

        public int PatiendBloodGroupId { get; set; }

        public string Msg { get; set; }


        public DataTable Dt;


        public int SaveBookingRequest()
        {
            int result=0;





            return result;
        }

        public int ApproveBookingRequest()
        {
            int result = 0;





            return result;
        }

        public int RejectBookingRequest()
        {
            int result = 0;





            return result;
        }

        public DataTable GetAllBookingRequestByHospitalId()
        {
            





            return Dt;
        }

        public DataTable GetAllBookingRequestByBloodBankId()
        {






            return Dt;
        }



        public DataTable GetBookingStatusByUserId(int UserId)
        {






            return Dt;
        }

    }
}