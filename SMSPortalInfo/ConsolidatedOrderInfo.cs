using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class ConsolidatedOrderInfo
    {
        public int Vendor_Item_Id { get; set; }

        public int Vendor_Id { get; set; }

        public int Dealer_Id { get; set; }

        public int Product_Id{ get; set; }

        public string Product_Name { get; set; }

        public int Product_Quantity { get; set; }

        public bool Is_Po_Generated { get; set; }

        public DateTime From_Date { get; set; }

        public DateTime To_Date { get; set; }

    }
}
