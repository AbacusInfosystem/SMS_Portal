using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class TaxInfo
    
    {

        public int Tax_Id { get; set; }

        public decimal Local_Tax { get; set; }

        public decimal Export_Tax { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }
    }


}
