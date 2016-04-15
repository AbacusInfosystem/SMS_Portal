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
        Get_Category_By_Name_Sp,
        Get_Category_By_Id_Sp,
        Delete_Category_By_Id_Sp,
        Check_Existing_Category,

        // Users
        Insert_Users_Sp,
        Get_Users_Sp,
        Get_Users_By_User_Name_Sp,
        Update_Users_Sp,
        Get_Users_By_Id_Sp,
        Get_Entity_By_Role_Sp,
        Check_Existing_User,

        //Sub Category
        Get_Sub_Category_Sp,
        Get_Sub_Category_By_Id_Sp,
        Insert_Sub_Category_Sp,
        Update_Sub_Category_Sp,
        Get_Subcateory_Autocomplete_Sp,
        Check_Existing_Sub_Category,

        //Brands
        Delete_Brand_By_Id_Sp,
        Get_Brand_By_Name_Sp,
        Get_Brand_By_Id_Sp,
        Get_Brand_Sp,
        Update_Brand_Sp,
        Insert_Brand_Sp,
        Check_Existing_Brand,
        Update_Brand_Image,

        //Roles
        Get_Roles_Sp,

        //Dealers

        Get_Dealer_By_Id_Sp,
        Get_Dealer_By_Name_Sp,
        Get_Dealer_Sp,
        Update_Dealer_Sp,
        Insert_Dealer_Sp,
        Check_Existing_Dealer,

        //State
        Get_State_Sp
        
	}
}
