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
	}
}