using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HospitalBedAndBloodManagementSystem.Models
{
    public class GeneralViewModel
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



        public List<District> districts { get { return GetAllDistrict(); } }

        public List<HospitalViewModel> hospitals { get { return GetAllHospital(); } }

        public List<BloodBankViewModel> bloodBanks { get { return GetAllBloodBank(); } }

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


        public List<HospitalViewModel> GetAllHospital()
        {
            List<HospitalViewModel> HList = new List<HospitalViewModel>();

            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = oDm.ExecuteReader("Hospital_GetAll");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HospitalViewModel h = new HospitalViewModel();
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


        public List<BloodBankViewModel> GetAllBloodBank()
        {
            List<BloodBankViewModel> BList = new List<BloodBankViewModel>();

            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = oDm.ExecuteReader("BloodBank_GetAll");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        BloodBankViewModel b = new BloodBankViewModel();
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


        public int SaveRegistrationDetails()
        {
            int result = 0;
            if (this.UserTypeId == 1)
                this.IsAuthorised = 1;
            if (this.UserTypeId == 2)
                this.IsAuthorised = 1;
            if (this.UserTypeId == 3)
                this.IsHospitalMember = 1;
            if (this.UserTypeId == 4)
                this.IsBloodBankMember = 1;
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserId", SqlDbType.Int, ParameterDirection.InputOutput, this.UserId);
                oDm.Add("@pName", SqlDbType.VarChar, 50, this.GeneralUser);
                oDm.Add("@pDistrictId", SqlDbType.Int, this.DistrictId);
                oDm.Add("@pCity", SqlDbType.VarChar, 20, this.City);
                oDm.Add("@pPincode", SqlDbType.VarChar, 10, this.Pincode);
                oDm.Add("@pContactNo", SqlDbType.VarChar, 10, this.ContactNo);
                oDm.Add("@pEmail", SqlDbType.VarChar, 50, this.EmailID);
                oDm.Add("@pUserName", SqlDbType.VarChar, 50, this.UserName);
                oDm.Add("@pPassword", SqlDbType.VarChar, 50, this.Password);
                oDm.Add("@pAddress", SqlDbType.VarChar, 50, this.Address);
                oDm.Add("@pDOB", SqlDbType.Date, this.DOB);
                oDm.Add("@pGenderId", SqlDbType.Int, this.GenderId);
                oDm.Add("@pUserTypeId", SqlDbType.Int, this.UserTypeId);
                oDm.Add("@pIsAuthorised", SqlDbType.Int, this.IsAuthorised);
                oDm.Add("@pIsHospitalMember", SqlDbType.Int, this.IsHospitalMember);
                oDm.Add("@pIsBloodBankMember", SqlDbType.Int, this.IsBloodBankMember);
                oDm.Add("@pHospitalId", SqlDbType.Int, this.HospitalId);
                oDm.Add("@pBloodBankId", SqlDbType.Int, this.BloodBankId);
                oDm.Add("@pDeviceId", SqlDbType.VarChar, 100, DBNull.Value);



                oDm.CommandType = CommandType.StoredProcedure;
                result = oDm.ExecuteNonQuery("UserMaster_Save");
                this.UserId = (int)oDm["@pUserId"].Value;
            }

            return result;
        }

        public void getAllUserDetailsByUserTypeId(int id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserTypeId", SqlDbType.Int, id);
                oDm.Add("@pHospitalId", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["HospitalId"]));
                oDm.Add("@pBloodBankId", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["BloodBankId"]));
                oDm.CommandType = CommandType.StoredProcedure;

                Dt = oDm.ExecuteDataTable("UserMaster_GetAllByUserType");

            }

        }

        public void getAllUserDetailsByUserTypeIdForADMIN(int id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserTypeId", SqlDbType.Int, id);
               
                oDm.CommandType = CommandType.StoredProcedure;

                Dt = oDm.ExecuteDataTable("UserMaster_GetAllByUserTypeForADMIN");

            }

        }

        public void MakeHospitalAdmin(int id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserId", SqlDbType.Int, id);
                oDm.Add("@pUpdatedBy", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]));
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("HospitalUserMapping_Update_ADMIN");

            }

        }


        public void MakeBloodBankAdmin(int id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserId", SqlDbType.Int, id);
                oDm.Add("@pUpdatedBy", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]));
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("BloodBankUserMapping_Update_ADMIN");

            }


        }

        public void MakeHospitalMember(int id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserId", SqlDbType.Int, id);
                oDm.Add("@pUpdatedBy", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]));
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("HospitalUserMapping_Update_HospitalADMIN");

            }

        }


        public void MakeBloodBankMember(int id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserId", SqlDbType.Int, id);
                oDm.Add("@pUpdatedBy", SqlDbType.Int, Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]));
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("BloodBankUserMapping_Update_BloodBankADMIN");

            }

        }


    }
}