using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;
using HospitalBedAndBloodManagementSystem.Models;
using HospitalBedAndBloodManagementSystem.Controllers;



namespace HospitalBedAndBloodManagementSystem.Models
{
    public class BloodBankInfoModel
    {
        public int BloodInfoId { get; set; }
        public int BloodBankId { get; set;  }

        public string BloodBank { get; set; }

        public int AP { get; set; }
        public int AN { get; set; }

        public int BP { get; set; }
        public int BN { get; set; }

        public int ABP { get; set; }
        public int ABN { get; set; }

        public int OP { get; set; }
        public int ON { get; set; }

        public string Msg { get; set; }

        public List<BloodBankInfoModel> bloodBanks { get { return GetAllBloodBank(); } }

        public List<BloodBankInfoModel> GetAllBloodBank()
        {
            List<BloodBankInfoModel> BList = new List<BloodBankInfoModel>();

            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = oDm.ExecuteReader("BloodBank_GetAll");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        BloodBankInfoModel b = new BloodBankInfoModel();
                        b.BloodBankId = (dr["BloodBankId"] == DBNull.Value ? 0 : int.Parse(dr["BloodBankId"].ToString()));
                        b.BloodBank = (dr["BloodBank"] == DBNull.Value ? "" : dr["BloodBank"].ToString());
                        BList.Add(b);
                    }

                }


            }
            if (this.BloodBankId > 0)
                BList = BList.FindAll(x => x.BloodBankId == this.BloodBankId);
            return BList;
        }

        public void GetBloodInfo()
        {
           

            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pBloodBankId", SqlDbType.Int, Convert.ToInt32((System.Web.HttpContext.Current.Session["BloodBankId"])));
                SqlDataReader dr = oDm.ExecuteReader("BloodInfo_GetByBloodBankId");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        
                        this.BloodBankId = (dr["BloodBankId"] == DBNull.Value ? 0 : int.Parse(dr["BloodBankId"].ToString()));
                        this.AP= (dr["AP"] == DBNull.Value ? 0 : int.Parse(dr["AP"].ToString()));
                        this.AN = (dr["AN"] == DBNull.Value ? 0 : int.Parse(dr["AN"].ToString()));
                        this.BP = (dr["BP"] == DBNull.Value ? 0 : int.Parse(dr["BP"].ToString()));
                        this.BN = (dr["BN"] == DBNull.Value ? 0 : int.Parse(dr["BN"].ToString()));
                        this.OP = (dr["OP"] == DBNull.Value ? 0 : int.Parse(dr["OP"].ToString()));
                        this.ON = (dr["ON"] == DBNull.Value ? 0 : int.Parse(dr["ON"].ToString()));
                        this.ABP = (dr["ABP"] == DBNull.Value ? 0 : int.Parse(dr["ABP"].ToString()));
                        this.ABN = (dr["ABN"] == DBNull.Value ? 0 : int.Parse(dr["ABN"].ToString()));
                        this.BloodInfoId= (dr["BloodInfoId"] == DBNull.Value ? 0 : int.Parse(dr["BloodInfoId"].ToString()));
                    }

                }


            }
            
           
        }



        public int SaveBloodInfo()
        {
            int result = 0;
            
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pBloodInfoId", SqlDbType.Int,this.BloodInfoId);
                oDm.Add("@pBloodBankId", SqlDbType.Int, this.BloodBankId);
                oDm.Add("@pUserId", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]));
                oDm.Add("@pAP", SqlDbType.Int, this.AP);
                oDm.Add("@pAN", SqlDbType.Int,this.AN);
                oDm.Add("@pBP", SqlDbType.Int, this.BP);
                oDm.Add("@pBN", SqlDbType.Int, this.BN);
                oDm.Add("@pOP", SqlDbType.Int, this.OP);
                oDm.Add("@pON", SqlDbType.Int, this.ON);
                oDm.Add("@pABP", SqlDbType.Int, this.ABP);
                oDm.Add("@pABN", SqlDbType.Int, this.ABN);
                



                oDm.CommandType = CommandType.StoredProcedure;
                result = oDm.ExecuteNonQuery("BloodInfo_Update");
                this.BloodInfoId = (int)oDm["@pBloodInfoId"].Value;
            }

            return result;
        }







    }
}