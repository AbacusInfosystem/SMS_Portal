﻿using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalManager
{
    public class DealerManager
    {
        DealerRepo _dealerRepo;

        public DealerManager()
        {
            _dealerRepo = new DealerRepo();
        }

        public void Insert_Dealer(DealerInfo dealer)
        {
            _dealerRepo.Insert_Dealer(dealer);
        }

        public void Update_Dealer(DealerInfo dealer)
        {
            _dealerRepo.Update_Dealer(dealer);
        }

        public List<DealerInfo> Get_Dealers(ref PaginationInfo Pager)
        {
            return _dealerRepo.Get_Dealers(ref Pager);
        }

        public DealerInfo Get_Dealer_By_Id(int Dealer_Id)
        {
            return _dealerRepo.Get_Dealer_By_Id(Dealer_Id);
        }

        public List<DealerInfo> Get_Dealer_By_Id(int Dealer_Id, ref PaginationInfo Pager)
        {
            return _dealerRepo.Get_Dealer_By_Id(Dealer_Id, ref Pager);
        }

        public bool Check_Existing_Dealer(string Dealer_Name)
        {
            return _dealerRepo.Check_Existing_Dealer(Dealer_Name);
        }

        public List<BrandInfo> Get_Brands()
        {
            return _dealerRepo.Get_Brands();
        }

        public List<AutocompleteInfo> Get_Dealer_Autocomplete(string DealerName)
        {
            return _dealerRepo.Get_Dealer_Autocomplete(DealerName);
        }
    }
}
