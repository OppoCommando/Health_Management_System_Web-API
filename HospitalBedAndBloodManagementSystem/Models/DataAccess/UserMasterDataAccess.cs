using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HospitalBedAndBloodManagementSystem.Models.ViewModels;

namespace HospitalBedAndBloodManagementSystem.Models.DataAccess
{
    public class UserMasterDataAccess
    {
        internal static string UserMaster_Save(UserMasterViewModel userObj)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMsg", SqlDbType.VarChar, 100, ParameterDirection.InputOutput, userObj.Msg);

                oDm.Add("@pUserId", SqlDbType.Int, ParameterDirection.InputOutput, userObj.UserId);
                oDm.Add("@pName", SqlDbType.VarChar, 50, userObj.GenderName);//User Name (Full Name)
                oDm.Add("@pUserName", SqlDbType.VarChar, 50, userObj.UserName);// Login Id
                oDm.Add("@pPassword", SqlDbType.VarChar, 50, userObj.Password);
                oDm.Add("@pUserTypeId", SqlDbType.Int, userObj.UserTypeId);
                oDm.Add("@pContactNo", SqlDbType.VarChar, 10, userObj.ContactNo);
                oDm.Add("@pEmail", SqlDbType.VarChar, 50, userObj.EmailID);
                oDm.Add("@pIsAuthorised", SqlDbType.Int, userObj.IsAuthorised);
                oDm.Add("@pAddress", SqlDbType.VarChar, 50, userObj.Address);

                if (userObj.DistrictId == null)
                {
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pDistrictId", SqlDbType.Int, userObj.DistrictId);
                }

                if (userObj.City == null)
                {
                    oDm.Add("@pCity", SqlDbType.VarChar, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pCity", SqlDbType.VarChar, 20, userObj.City);
                }


                if (userObj.Pincode == null)
                {
                    oDm.Add("@pPincode", SqlDbType.VarChar, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pPincode", SqlDbType.VarChar, 10, userObj.Pincode);
                }


                if (userObj.DOB == null)
                {
                    oDm.Add("@pDOB", SqlDbType.Date, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pDOB", SqlDbType.Date, userObj.DOB);
                }

                if (userObj.GenderId == null)
                {
                    oDm.Add("@pGenderId", SqlDbType.Int, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pGenderId", SqlDbType.Int, userObj.GenderId);
                }

                if (userObj.IsHospitalMember == null)
                {
                    oDm.Add("@pIsHospitalMember", SqlDbType.Int, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pIsHospitalMember", SqlDbType.Int, userObj.IsHospitalMember);
                }

                if (userObj.IsBloodBankMember == null)
                {
                    oDm.Add("@pIsBloodBankMember", SqlDbType.Int, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pIsBloodBankMember", SqlDbType.Int, userObj.IsBloodBankMember);
                }

                if (userObj.HospitalId == null)
                {
                    oDm.Add("@pHospitalId", SqlDbType.Int, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pHospitalId", SqlDbType.Int, userObj.HospitalId);
                }

                if (userObj.BloodBankId == null)
                {
                    oDm.Add("@pBloodBankId", SqlDbType.Int, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pBloodBankId", SqlDbType.Int, userObj.BloodBankId);
                }

                if (userObj.DeviceId == null)
                {
                    oDm.Add("@pDeviceId", SqlDbType.Int, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pDeviceId", SqlDbType.VarChar, 100, userObj.DeviceId);
                }


                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("App_UserMaster_Save");

                userObj.Msg= (string)oDm["@pMsg"].Value;

                return userObj.Msg;
            }
            
        }

        internal static UserMasterViewModel Login(string userName, string password,int UserType)
        {

            UserMasterViewModel userObj = new UserMasterViewModel();
            using (DataManager oDM = new DataManager())
            {
               
                    oDM.Add("@pUserName", SqlDbType.VarChar, 50, userName);
                
                    oDM.Add("@pPassword", SqlDbType.VarChar, 20, password);

                    oDM.Add("@pUserType", SqlDbType.Int, UserType);




                oDM.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = oDM.ExecuteReader("App_Authenticate_Login");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userObj.UserName = (dr["UserName"] == DBNull.Value) ? "" : dr["UserName"].ToString();
                        userObj.UserId = (dr["UserId"] == DBNull.Value) ? 0 : int.Parse(dr["UserId"].ToString());
                        userObj.GeneralUser = (dr["Name"] == DBNull.Value) ? "" : dr["Name"].ToString();
                        userObj.Password = (dr["Password"] == DBNull.Value) ? "" : (dr["Password"].ToString());
                        userObj.UserTypeId = (dr["UserType"] == DBNull.Value) ? 0 : int.Parse(dr["UserType"].ToString());
                        userObj.HospitalId = (dr["HospitalId"] == DBNull.Value) ? 0 : int.Parse(dr["HospitalId"].ToString());
                        userObj.BloodBankId = (dr["BloodBankId"] == DBNull.Value) ? 0 : int.Parse(dr["BloodBankId"].ToString());
                        if (userObj.HospitalId != 0)
                            userObj.Role = (dr["HospitalRole"] == DBNull.Value) ? 0 : int.Parse(dr["HospitalRole"].ToString());
                        else if (userObj.BloodBankId != 0)
                            userObj.Role = (dr["BloodBankRole"] == DBNull.Value) ? 0 : int.Parse(dr["BloodBankRole"].ToString());
                        else
                            userObj.Role = 0;
                        userObj.EmailID = (dr["EmailID"] == DBNull.Value) ? "" : dr["EmailID"].ToString();
                        userObj.ContactNo = (dr["ContactNo"] == DBNull.Value) ? "" : (dr["ContactNo"].ToString());
                        userObj.Msg = (dr["Msg"] == DBNull.Value) ? "" : (dr["Msg"].ToString());

                    }
                }
            }

            return userObj;

        }
    }
}