using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HospitalBedAndBloodManagementSystem.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public string Msg { get; set; }

        public DataTable Dt;

        public int Save()
        {
            int result = 0;

            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDistrictId", SqlDbType.Int, this.DistrictId);
                oDm.Add("@pDistrictName", SqlDbType.VarChar, 50, this.DistrictName);
                oDm.CommandType = CommandType.StoredProcedure;
                result = oDm.ExecuteNonQuery("DistrictMaster_Save");
            }

            return result;

        }

        public DataTable GetAllDistrict()
        {


            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;
                Dt = oDm.ExecuteDataTable("DistrictMaster_GetAll");
            }

            return Dt;

        }

        public void GetDistrictById(int Id)
        {
          using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDistrictId", SqlDbType.Int, Id);
                oDm.CommandType = CommandType.StoredProcedure;
                this.Dt = oDm.ExecuteDataTable("DistrictMaster_GetById");
                if (this.Dt != null)
                {
                    foreach (DataRow dr in this.Dt.Rows)
                    {
                        this.DistrictId = (@dr["DistrictId"] == DBNull.Value) ? 0 : Convert.ToInt32(@dr["DistrictId"]);
                        this.DistrictName = (@dr["DistrictName"] == DBNull.Value) ? "" : @dr["DistrictName"].ToString();

                    }
                }
          }
        }
            public int Delete(int Id)
            {
                int result = 0;

                using (DataManager oDm = new DataManager())
                {
                    oDm.Add("@pDistrictId", SqlDbType.Int,ParameterDirection.InputOutput, Id);
                    oDm.CommandType = CommandType.StoredProcedure;
                    oDm.ExecuteNonQuery("DistrictMaster_Delete");
                    result = (int)oDm["@pDistrictId"].Value;
                }

                return result;
            
            }

    }
}