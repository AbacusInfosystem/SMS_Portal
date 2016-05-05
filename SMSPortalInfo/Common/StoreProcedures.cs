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
        Get_Category_Id_By_Name,

        // Users
        Insert_Users_Sp,
        Get_Users_Sp,
        Get_Users_By_User_Name_Sp,
        Update_Users_Sp,
        Get_Users_By_Id_Sp,
        Get_Entity_By_Role_Sp,
        Check_Existing_User,
        Get_User_Autocomplete_Sp,
        Get_Users_By_Entity_Id_Sp,
        Get_User_By_Password_Token,
        Reset_Password,
        Get_User_Password_By_Id,
        Get_User_By_Email,

        //Sub Category
        Get_Sub_Category_Sp,
        Get_Sub_Category_By_Id_Sp,
        Insert_Sub_Category_Sp,
        Update_Sub_Category_Sp,
        Get_Subcateory_Autocomplete_Sp,
        Check_Existing_Sub_Category,
         Get_Sub_Category_By_Category_Sp,
        Get_Lookup_Sub_Category_By_Id_Sp,
        Get_SubCategory_Id_By_Name,

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
        Get_Brand_Id_By_Name,
        Update_Brand_Profile_Sp,

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
        Update_Dealer_Profile_Sp,

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
        Update_Vendor_Profile_Sp,
        Get_Vendor_Autocomplete_Sp,

        //Cookies
        Insert_Token_In_User_Table_Sp,
        Get_User_Data_By_Token_sp,

        //Receivable
        Get_Receivable_By_Name_Sp,
        Get_Receivable_Sp,
        Get_InvoiceNo_AutoComplete_Sp,
        Insert_Receivable_Data_Sp,
        Get_InvoiceNo_Sp,
         Insert_Receivable_Sp,
        Insert_Receivable_Item_Data_Sp,
        Get_Receivable_Data_Item_By_Id_Sp,
        Get_Receivable_Data_By_Id_Sp,
        Delete_Receivable_Item_By_Id_Sp,
        Get_Receivable_Balance_Amount_By_Id_Sp,
        Get_Invoice_Amount_By_Id_Sp,
        Get_Invoice_No_Autocomplete_Sp,
        Get_Receivable_Data_By_Invoice_Id_Sp,
        Insert_Receivable_Receipt_Data_Sp,
        Get_Order_Id_By_Invoice_Id_Sp,
        Get_Receivable_Status_By_Id_Sp,
        Update_Sales_Order_Status_Sp,

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
        Check_DuplicateProduct_PurchaseOrder,  
        Update_Purchase_Order_Gross_Amount,
        Get_Vendor_Product_Price_Id_Sp,

        //Payables
       Insert_Payable_Item_Data_Sp,
       Insert_Payable_Data_Sp,
       Get_Payable_Data_Item_By_Id_Sp,
       Get_Payable_Data_By_Id_Sp,
        Get_Payable_By_Name_Sp,
        Get_Payable_Sp,
        Delete_Payable_Data_Item_By_Id,
        Get_Payable_Balance_Amount_By_Id_Sp,
        Get_Receivable_Data_By_Purchase_Order_By_Id_Sp,
        Get_Purchase_Order_Amount_By_Id_Sp,
        Get_Payable_Status_By_Id_Sp,

        //Invoice
        Insert_Invoice_Sp,
        Get_Invoice_Sp,
        Get_Invoice_By_Id_Sp,
        Get_Invoice_Autocomplete_Sp,
        
        //Orders
        Get_Order_By_Id,
        Get_Order_Items_By_Order_Id,
        Get_Orders,
        Get_Sales_Order_Sp,
        Get_Sales_OrderBy_Id_Sp,
        Get_Dealer_Data_Sp,
        Update_Order_Status_Sp,
        Get_Order_No_Autocomplete_Sp,
         

        //Tax

        Insert_Tax_Sp,
        Update_Tax_Sp,
        Get_Tax_By_Id_Sp,
      
	}
}
