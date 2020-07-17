using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HospitalBedAndBloodManagementSystem.Models
{
    public class HospitalInfoModel
    {
        public int HospitalInfoId { get; set; }
        public int HospitalId { get; set; }

        public string Hospital { get; set; }

        public int AvailableBeds { get; set; }
        

        public string Msg { get; set; }

        public List<HospitalInfoModel> hospitals { get { return GetAllHospital(); } }

        public List<HospitalInfoModel> GetAllHospital()
        {
            List<HospitalInfoModel> HList = new List<HospitalInfoModel>();

            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = oDm.ExecuteReader("Hospital_GetAll");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HospitalInfoModel h = new HospitalInfoModel();
                        h.HospitalId = (dr["HospitalId"] == DBNull.Value ? 0 : int.Parse(dr["HospitalId"].ToString()));
                        h.Hospital = (dr["Hospital"] == DBNull.Value ? "" : dr["Hospital"].ToString());
                        HList.Add(h);
                    }

                }


            }
            if (this.HospitalId > 0)
                HList = HList.FindAll(x => x.HospitalId == this.HospitalId);
            return HList;
        }

        public void GetBedInfo()
        {


            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pHospitalID", SqlDbType.Int, Convert.ToInt32((System.Web.HttpContext.Current.Session["HospitalId"])));
                SqlDataReader dr = oDm.ExecuteReader("HospitalInfo_GetByHospitalId");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        this.HospitalId = (dr["HospitalId"] == DBNull.Value ? 0 : int.Parse(dr["HospitalId"].ToString()));
                        this.AvailableBeds = (dr["AvailableBeds"] == DBNull.Value ? 0 : int.Parse(dr["AvailableBeds"].ToString()));
                       this.HospitalInfoId = (dr["HospitalInfoId"] == DBNull.Value ? 0 : int.Parse(dr["HospitalInfoId"].ToString()));
                    }

                }


            }


        }



        public int SaveBedInfo()
        {
            int result = 0;

            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pHospitalInfoId", SqlDbType.Int, this.HospitalInfoId);
                oDm.Add("@pHospitalId", SqlDbType.Int, this.HospitalId);
                oDm.Add("@pUserId", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]));
                oDm.Add("@pAvailableBeds", SqlDbType.Int, this.AvailableBeds);
                oDm.CommandType = CommandType.StoredProcedure;
                result = oDm.ExecuteNonQuery("HospitalInfo_Update");
                this.HospitalId = (int)oDm["@pHospitalId"].Value;
            }

            return result;
        }

    }
}