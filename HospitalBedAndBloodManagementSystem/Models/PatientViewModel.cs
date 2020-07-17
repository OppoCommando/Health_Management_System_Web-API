using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalBedAndBloodManagementSystem.Models
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }

        public int PatientType { get; set; }


        public int Age { get; set; }

        public string BloodGroupId { get; set; }  //AP=A+,AN=A-,BP=B+,BN=B-,OP=O+,ON=O-,ABP=AB+,ABN=AB-

        public string BloodGroupName { get; set; }   

        public int GenderId { get; set; }

        public string GenderName { get; set; }

        public string Address { get; set; }


        public string Prescribtion { get; set; }

        public string Msg { get; set; }

        public DataTable Dt;

        public int Save()
        {
            int result = 0;

            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPatientId", SqlDbType.Int, ParameterDirection.InputOutput, this.PatientId);
                oDm.Add("@pPatientName", SqlDbType.VarChar, 50, this.PatientName);
                oDm.Add("@pBloodGroup", SqlDbType.VarChar, 50, this.BloodGroupId);
                oDm.Add("@pAddress", SqlDbType.VarChar, 50, this.Address);
                oDm.Add("@pGenderId", SqlDbType.Int, this.GenderId);
                oDm.Add("@pAge", SqlDbType.Int, this.Age);
                oDm.Add("@pPrescribtion", SqlDbType.VarChar, 50, this.Prescribtion);
                oDm.Add("@pCreatedBy", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]));
                oDm.CommandType = CommandType.StoredProcedure;
                result = oDm.ExecuteNonQuery("PatientMaster_Save");
            }

            return result;
        }

        public DataTable GetAllPatientDetailsByUserId()
        {
            int UserId = int.Parse(HttpContext.Current.Session["UserId"].ToString());
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pCreatedBy", SqlDbType.Int, UserId);
                oDm.CommandType = CommandType.StoredProcedure;
                this.Dt = oDm.ExecuteDataTable("PatientMaster_GetByUserId");

            }
            return this.Dt;
        }


    }
}