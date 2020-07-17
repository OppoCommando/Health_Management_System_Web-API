using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HospitalBedAndBloodManagementSystem.Models.DataAccess
{
    public class HospitalDataAccess
    {
        internal static DataTable App_HospitalMaster_GetByHospitalType(int hospitalType)
        {
            using (DataManager oDm = new DataManager())
            {
                if (hospitalType == 0)
                {
                    oDm.Add("@pHospitalType", SqlDbType.Int, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pHospitalType", SqlDbType.Int, hospitalType);
                }
                
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteDataTable("App_HospitalMaster_GetByHospitalType");
            }

        }
    }
}