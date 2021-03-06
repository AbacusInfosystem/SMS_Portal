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
    public class BrandManager
    {
        BrandRepo _brandRepo;

        public BrandManager()
        {
            _brandRepo = new BrandRepo();
        }

        public void Insert_Brand(BrandInfo brand, int user_id)
        {
            _brandRepo.Insert_Brand(brand,user_id);
        }

        public void Update_Brand(BrandInfo brand,int user_id)
        {
            _brandRepo.Update_Brand(brand, user_id);
        }

        public List<BrandInfo> Get_Brands(ref PaginationInfo Pager)
        {
            return _brandRepo.Get_Brands(ref Pager);
        }

        public BrandInfo Get_Brand_By_Id(int Brand_Id)
        {
            return _brandRepo.Get_Brand_By_Id(Brand_Id);
        }

        public List<BrandInfo> Get_Brand_By_Id(int Brand_Id,ref PaginationInfo Pager)
        {
            return _brandRepo.Get_Brand_By_Id(Brand_Id, ref Pager);
        }

        public void Delete_Brand_By_Id(int Brand_Id)
        {
            _brandRepo.Delete_Brand_By_Id(Brand_Id);
        }

        public bool Check_Brand(string Brand_Name)
        {
            return _brandRepo.Check_Existing_Brand(Brand_Name);
        }

        public void Update_Brand_FileName(int Brand_Id, string Brand_Name)
        {
             _brandRepo.Update_Brand_FileName(Brand_Id, Brand_Name);
        }

        public List<AutocompleteInfo> Get_Brand_Autocomplete(string brandName)
        {
            return _brandRepo.Get_Brand_Autocomplete(brandName);
        }

        public void Update_Brand_Profile(BrandInfo brand,int user_Id)
        {
            _brandRepo.Update_Brand_Profile(brand, user_Id);
        }

       
    }
}
