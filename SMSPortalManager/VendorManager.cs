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
    public class VendorManager
    {
        VendorRepo _vendorRepo;

        public VendorManager()
        {
            _vendorRepo = new VendorRepo();
        }

        public void Insert_Vendor(VendorInfo Vendor)
        {
            _vendorRepo.Insert_Vendor(Vendor);
        }

        public void Update_Vendor(VendorInfo Vendor)
        {
            _vendorRepo.Update_Vendor(Vendor);
        }

        public VendorInfo Get_Vendor_By_Id(int Vendor_Id)
        {
            return _vendorRepo.Get_Vendor_By_Id(Vendor_Id);
        }

        public List<VendorInfo> Get_Vendors(ref PaginationInfo Pager)
        {
            return _vendorRepo.Get_Vendors(ref Pager);
        }

        public List<VendorInfo> Get_Vendor_By_Name(string Vendor_Name, ref PaginationInfo Pager)
        {
            return _vendorRepo.Get_Vendor_By_Name(Vendor_Name, ref Pager);
        }

        public bool Check_Existing_Vendor(string Vendor_Name)
        {
            return _vendorRepo.Check_Existing_Vendor(Vendor_Name);
        }

        public VendorInfo Get_Vendor_Profile_Data_By_User_Id(int user_Id)
        {
            return _vendorRepo.Get_Vendor_Profile_Data_By_User_Id(user_Id);
        }

        public void Insert_Vendor_Bank_Details(VendorInfo Vendor,int user_Id)
        {
            _vendorRepo.Insert_Vendor_Bank_Details(Vendor, user_Id);
        }

        public List<Bank_Details> Get_Vendor_Bank_Details(int vendor_Id)
        {
            return _vendorRepo.Get_Vendor_Bank_Details(vendor_Id);
        }

        public List<ProductInfo> Get_Productmapping(int Brand_Id)
        {
            return _vendorRepo.Get_Productmapping(Brand_Id);
        }

        public List<BrandInfo> Get_Brands()
        {
            return _vendorRepo.Get_Brands();
        }

        public void Insert_Vendor_Product_Mapping_Details(List<ProductInfo> product_List, int user_Id, int vendor_Id,int brand_Id)
        {
            _vendorRepo.Insert_Vendor_Product_Mapping_Details(product_List, user_Id, vendor_Id, brand_Id);
        }

        public List<ProductInfo> Get_Mapped_Product_List(int vendor_Id)
        {
            return _vendorRepo.Get_Vendor_Mapped_Product_List(vendor_Id);
        }

        public List<AutocompleteInfo> Get_Vendor_Autocomplete(string vendor)
        {
            return _vendorRepo.Get_Vendor_Autocomplete(vendor);
        }
        
    }
}
