using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;



namespace HospitalBedAndBloodManagementSystem.Models
{
    public class LoginViewModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public int UserTypeId { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public String Msg { get; set; }

        public int HospitalId { get; set; }

        public int BloodBankId { get; set; }

        public int Role { get; set; }


        public void AuthenticateUser(string UserName)
        {
            using (DataManager oDM = new DataManager())
            {
                if (this.UserName != null && this.UserName.Length > 0)
                    oDM.Add("@pUserName", SqlDbType.VarChar, 50, UserName);
                else
                    oDM.Add("@pUserName", SqlDbType.VarChar, 50, DBNull.Value);

                oDM.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = oDM.ExecuteReader("Authenticate_Login");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        this.UserName= (dr["UserName"] == DBNull.Value) ? "" : dr["UserName"].ToString();
                        this.UserId = (dr["UserId"] == DBNull.Value) ? 0 : int.Parse(dr["UserId"].ToString());
                        this.Name = (dr["Name"] == DBNull.Value) ? "" : dr["Name"].ToString();
                        this.Password = (dr["Password"] == DBNull.Value) ? "" : (dr["Password"].ToString());
                        this.UserTypeId = (dr["UserType"] == DBNull.Value) ? 0 : int.Parse(dr["UserType"].ToString());
                        this.HospitalId = (dr["HospitalId"] == DBNull.Value) ? 0 : int.Parse(dr["HospitalId"].ToString());
                        this.BloodBankId = (dr["BloodBankId"] == DBNull.Value) ? 0 : int.Parse(dr["BloodBankId"].ToString());
                        if (this.HospitalId != 0)
                            this.Role = (dr["HospitalRole"] == DBNull.Value) ? 0 : int.Parse(dr["HospitalRole"].ToString());
                        else if (this.BloodBankId != 0)
                            this.Role = (dr["BloodBankRole"] == DBNull.Value) ? 0 : int.Parse(dr["BloodBankRole"].ToString());
                        else
                            this.Role = 0;

                    }
                }
            }
        }



        
    }
}