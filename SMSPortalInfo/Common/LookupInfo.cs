using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMSPortalInfo.Common
{
    public class LookupInfo
    {    
        public static Dictionary<int, string> Get_Gender_Types()
        {
            Dictionary<int, string> Get_Gender_Types = new Dictionary<int, string>();

            Get_Gender_Types.Add(1, GenderType.Male.ToString().Replace('_', ' ').ToString());

            Get_Gender_Types.Add(2, GenderType.Female.ToString().Replace('_', ' ').ToString());

            return Get_Gender_Types;
        }

        public static Dictionary<int, string> Get_Brand_Categories()
        {
            Dictionary<int, string> Get_Brand_Category = new Dictionary<int, string>();

            Get_Brand_Category.Add(1, BrandCategory.Elite.ToString());
            Get_Brand_Category.Add(2, BrandCategory.Volumn_Based.ToString().Replace('_', ' ').ToString());
            Get_Brand_Category.Add(3, BrandCategory.Beyond_Borders.ToString().Replace('_', ' ').ToString());

            return Get_Brand_Category;
        }

        public static Dictionary<int, string> Get_Transaction_Types()
        {
            Dictionary<int, string> Get_Transaction_Types = new Dictionary<int, string>();

            Get_Transaction_Types.Add(1, TransactionType.Cheque.ToString());
            Get_Transaction_Types.Add(2, TransactionType.NEFT.ToString().Replace('_', ' ').ToString());
            Get_Transaction_Types.Add(3, TransactionType.Credit_Debit.ToString().Replace('_', ' ').ToString());

            return Get_Transaction_Types;
        }

        public static Dictionary<int, string> Get_Order_Status()
        {
            Dictionary<int, string> Get_Order_Status = new Dictionary<int, string>();

            Get_Order_Status.Add(1, OrderStatus.Order_Received.ToString().Replace('_', ' ').ToString());
            Get_Order_Status.Add(2, OrderStatus.Order_Confirmed.ToString().Replace('_', ' ').ToString());
            Get_Order_Status.Add(3, OrderStatus.Order_Dispatched.ToString().Replace('_', ' ').ToString());
            Get_Order_Status.Add(4, OrderStatus.Order_Delivered.ToString().Replace('_', ' ').ToString());

            return Get_Order_Status;
        }

        public static Dictionary<int, string> Get_Roles()
        {
            Dictionary<int, string> Get_Roles = new Dictionary<int, string>();

            Get_Roles.Add(1, Roles.Admin.ToString());
            Get_Roles.Add(2, Roles.Brand.ToString());
            Get_Roles.Add(3, Roles.Dealer.ToString());
            Get_Roles.Add(4, Roles.Vendor.ToString());

            return Get_Roles;
        }
        
	}
}