using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace HospitalBedAndBloodManagementSystem.Models
{
    public class HospitalViewModel
    {
        public int HospitalId { get; set; }
        public string Hospital { get; set; }

        public string Address { get; set; }

        public int DistrictId { get; set; }

        public string Pincode { get; set; }
        public string City { get; set; }
        public string RegistrationNo { get; set; }
        public string ContactNo { get; set; }
        public string LandLineNo { get; set; }
        public string EmailID { get; set; }

        public string HospitalTypeName { get; set; }
        public int HospitalTypeId { get; set; }
        public string Msg { get; set; }
        public List<District> districts { get { return GetAllDistrict(); } }

        public DataTable Dt;

        public List<District> GetAllDistrict()
        {
            List<District> DList = new List<District>();

            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = oDm.ExecuteReader("District_GetAll");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        District d = new District();
                        d.DistrictId = (dr["DistrictId"] == DBNull.Value ? 0 : int.Parse(dr["DistrictId"].ToString()));
                        d.DistrictName = (dr["DistrictName"] == DBNull.Value ? "" : dr["DistrictName"].ToString());
                        DList.Add(d);
                    }

                }


            }
            if (this.DistrictId > 0)
                DList = DList.FindAll(x => x.DistrictId == this.DistrictId);
            return DList;
        }

        public int Save()
        {
            int result = 0;

            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pHospitalId", SqlDbType.Int,ParameterDirection.InputOutput, this.HospitalId);
                oDm.Add("@pHospitalname", SqlDbType.VarChar, 50, this.Hospital);
                oDm.Add("@pDistrictId", SqlDbType.Int, this.DistrictId);
                oDm.Add("@pCity", SqlDbType.VarChar, 20, this.City);
                oDm.Add("@pPincode", SqlDbType.VarChar, 10, this.Pincode);
                oDm.Add("@pContactNo", SqlDbType.VarChar, 10, this.ContactNo);
                oDm.Add("@pEmailID", SqlDbType.VarChar, 50, this.EmailID);
                oDm.Add("@pHospitalTypeId", SqlDbType.Int, this.HospitalTypeId);
                oDm.Add("@pRegistrationNo", SqlDbType.VarChar, 50, this.RegistrationNo);
                oDm.Add("@pAddress", SqlDbType.VarChar, 50, this.Address);
                if (this.LandLineNo =="")
                    oDm.Add("@pLandLineNo", SqlDbType.VarChar, 10, DBNull.Value);
                else
                    oDm.Add("@pLandLineNo", SqlDbType.VarChar, 11, this.LandLineNo);
                oDm.Add("@pCreatedBy", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]));
                oDm.CommandType = CommandType.StoredProcedure;
                result = oDm.ExecuteNonQuery("HospitalMaster_Save");
            }

            return result;
        }

        public DataTable GetAllHospital()
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;
                Dt = oDm.ExecuteDataTable("HospitalMaster_GetAll");
            }

            return Dt;

        }

        public void GetHospitalById(int Id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pHospitalId", SqlDbType.Int, Id);
                oDm.CommandType = CommandType.StoredProcedure;
                this.Dt = oDm.ExecuteDataTable("HospitalMaster_GetById");
                if (this.Dt != null)
                {
                    foreach (DataRow dr in this.Dt.Rows)
                    {
                        this.HospitalId = (@dr["HospitalId"] == DBNull.Value) ? 0 : Convert.ToInt32(@dr["HospitalId"]);
                        this.Hospital = (@dr["HospitalName"] == DBNull.Value) ? "" : @dr["HospitalName"].ToString();
                        this.DistrictId = (@dr["DistrictId"] == DBNull.Value) ? 0 : Convert.ToInt32(@dr["DistrictId"]);
                        this.Address = (@dr["Address"] == DBNull.Value) ? "" : @dr["Address"].ToString();
                        this.Pincode = (@dr["Pin"] == DBNull.Value) ? "" : @dr["Pin"].ToString();
                        this.RegistrationNo = (@dr["RegistrationNo"] == DBNull.Value) ? "" : @dr["RegistrationNo"].ToString();
                        this.ContactNo = (@dr["ContactNo"] == DBNull.Value) ? "" : @dr["ContactNo"].ToString();
                        this.LandLineNo = (@dr["LandLineNo"] == DBNull.Value) ? "" : @dr["LandLineNo"].ToString();
                        this.City = (@dr["City"] == DBNull.Value) ? "" : @dr["City"].ToString();
                        this.EmailID = (@dr["Email"] == DBNull.Value) ? "" : @dr["Email"].ToString();
                        this.HospitalTypeId = (@dr["HospitalType"] == DBNull.Value) ? 0 : Convert.ToInt32(@dr["HospitalType"]);





                    }
                }
            }
        }
        public int Delete(int Id)
        {
            int result = 0;

            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pHospitalId", SqlDbType.Int,ParameterDirection.InputOutput, Id);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("HospitalMaster_Delete");
                result = (int)oDm["@pHospitalId"].Value;
            }

            return result;

        }


      












    }
}