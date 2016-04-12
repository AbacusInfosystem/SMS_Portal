using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo.Common
{
	public enum StoreProcedures
	{
        //Login
        Authenticate_User_sp,

        //Category
        Insert_Category_Sp,
        Update_Category_Sp,
        Get_Category_Sp,
        Get_Category_By_Id_Sp,
        Delete_Category_By_Id_Sp,

        // Users
        Insert_Users_Sp,
	}
}
