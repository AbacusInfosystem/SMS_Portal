using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;

namespace SMSPortalManager
{
    public class TaxManager
    {
        TaxRepo _taxRepo;

        public TaxManager()
        {
            _taxRepo = new TaxRepo();
        }

        public int Insert_Tax(TaxInfo tax)
        {
            return _taxRepo.Insert_Tax(tax);
        }

        public void Update_Tax(TaxInfo tax)
        {
            _taxRepo.Update_Tax(tax);
        }

        public TaxInfo Get_Tax_By_Id()
        {
            return _taxRepo.Get_Tax_By_Id();
        }
    }
}
