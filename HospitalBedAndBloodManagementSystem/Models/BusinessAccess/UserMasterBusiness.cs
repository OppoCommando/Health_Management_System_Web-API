using HospitalBedAndBloodManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalBedAndBloodManagementSystem.Models.BusinessAccess
{
    public class UserMasterBusiness
    {
        internal string UserMaster_Save(UserMasterViewModel userObj)
        {
            return DataAccess.UserMasterDataAccess.UserMaster_Save(userObj);
        }

        internal UserMasterViewModel LoginUser(string userName, string password,int UserType)
        {
            return DataAccess.UserMasterDataAccess.Login(userName, password, UserType);
        }

        internal UserMasterViewModel UserMaster_Register(UserMasterViewModel userObj)
        {
            return DataAccess.UserMasterDataAccess.UserMaster_Register(userObj);
        }
    }
}