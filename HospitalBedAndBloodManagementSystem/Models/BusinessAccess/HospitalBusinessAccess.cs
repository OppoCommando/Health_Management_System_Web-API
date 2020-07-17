using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HospitalBedAndBloodManagementSystem.Models.BusinessAccess
{
    public class HospitalBusinessAccess
    {
        internal DataTable App_HospitalMaster_GetByHospitalType(int hospitalType)
        {
            return DataAccess.HospitalDataAccess.App_HospitalMaster_GetByHospitalType(hospitalType);
        }
    }
}