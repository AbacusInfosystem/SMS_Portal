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
        Get_Category_Autocomplete_Sp,

        // Users
        Insert_Users_Sp,
        Get_Users_Sp,
        Get_Users_By_User_Name_Sp,
        Update_Users_Sp,
        Get_Users_By_Id_Sp,
        Get_Entity_By_Role_Sp,
        Check_Existing_User,
        Get_User_Autocomplete_Sp,

        //Sub Category
        Get_Sub_Category_Sp,
        Get_Sub_Category_By_Id_Sp,
        Insert_Sub_Category_Sp,
        Update_Sub_Category_Sp,
        Get_Subcateory_Autocomplete_Sp,
        Check_Existing_Sub_Category,
         Get_Sub_Category_By_Category_Sp,
        Get_Lookup_Sub_Category_By_Id_Sp,

        //Brands
        Delete_Brand_By_Id_Sp,
        Get_Brand_By_Name_Sp,
        Get_Brand_By_Id_Sp,
        Get_Brand_Sp,
        Update_Brand_Sp,
        Insert_Brand_Sp,
        Check_Existing_Brand,
        Update_Brand_Image,
        Get_Brand_Autocomplete_Sp,

        //Roles
        Get_Roles_Sp,

        //Dealers

        Get_Dealer_By_Id_Sp,
        Get_Dealer_By_Name_Sp,
        Get_Dealer_Sp,
        Update_Dealer_Sp,
        Insert_Dealer_Sp,
        Check_Existing_Dealer,
        Get_Dealer_Autocomplete_Sp,

        //State
        Get_State_Sp,

        //Product
        Check_Existing_Product,
        Get_Product_By_Name_Sp,
        Get_Product_By_Id_Sp,
        Get_Product_Sp,
        Update_Product_Sp,
        Insert_Product_Sp,
        Get_Product_Images_Sp,
        Insert_Product_Image_Sp,
        Delete_Product_Image_Sp,
        Get_Product_Autocomplete_Sp,
         
        
        //Vendor
        Insert_Vendor_Sp,
        Get_Vendor_Sp,
        Get_Vendor_By_Name_Sp,
        Update_Vendor_Sp,
        Get_Vendor_By_Id_Sp,
        Get_Productmapping,
        Get_Brands_Sp,
        Check_Existing_Vendor,
        Get_Vendor_Profile_Data_Sp,
        Insert_Vendor_Bank_Details_Sp,
        Get_Vendor_Bank_Details_By_Id_Sp,
        Insert_Vendor_Product_Mapping_Details,
        Get_Vendor_Mapped_Products_Sp,
        Delete_Vendor_Product_Mapping_By_Id_Sp,
        Get_Vendor_Autocomplete_Sp,

        //Cookies
        Insert_Token_In_User_Table_Sp,
        Get_User_Data_By_Token_sp,

        //Receivable
        Get_Receivable_By_Name_Sp,
        Get_Receivable_Sp,
        Get_InvoiceNo_AutoComplete_Sp,
        Get_InvoiceNo_Sp,

        //Purchase Order
        Get_Purchase_Order_By_Id_Sp,
        Get_Purchase_Order_Sp,
        Insert_Purchase_Order_Sp,
        Update_Purchase_Order_Sp,
        Get_Purchase_Order_Autocomplete_Sp,
        Get_Purchase_Order_Items_By_Id_Sp,
        Insert_Purchase_Order_Item_Sp,
        Update_Purchase_Order_Item_Sp,
        Delete_Purchase_Order_Item_By_Id_Sp,
         

        Get_InvoiceNo_Sp,
        Insert_Receivable_Sp
	}
}
