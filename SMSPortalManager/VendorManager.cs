using SMSPortalInfo;
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

        public void Insert_Vendor(VendorInfo vendor , int user_Id)
        {
            _vendorRepo.Insert_Vendor(vendor , user_Id);
        }

        public void Update_Vendor(VendorInfo vendor, int user_Id)
        {
            _vendorRepo.Update_Vendor(vendor, user_Id);
        }

        public VendorInfo Get_Vendor_By_Id(int vendor_Id)
        {
            return _vendorRepo.Get_Vendor_By_Id(vendor_Id);
        }

        public List<VendorInfo> Get_Vendors(ref PaginationInfo pager)
        {
            return _vendorRepo.Get_Vendors(ref pager);
        }

        public List<VendorInfo> Get_Vendor_By_Id_List(int vendor_Id, ref PaginationInfo pager)
        {
            return _vendorRepo.Get_Vendor_By_Id_List(vendor_Id, ref pager);
        }

        public bool Check_Existing_Vendor(string vendor_Name)
        {
            return _vendorRepo.Check_Existing_Vendor(vendor_Name);
        }

        public VendorInfo Get_Vendor_Profile_Data_By_User_Id(int user_Id)
        {
            return _vendorRepo.Get_Vendor_Profile_Data_By_User_Id(user_Id);
        }

        public void Insert_Vendor_Bank_Details(VendorInfo vendor,int user_Id)
        {
            _vendorRepo.Insert_Vendor_Bank_Details(vendor, user_Id);
        }

        public List<Bank_Details> Get_Vendor_Bank_Details(int vendor_Id)
        {
            return _vendorRepo.Get_Vendor_Bank_Details(vendor_Id);
        }

        public List<ProductInfo> Get_Productmapping(int brand_Id)
        {
            return _vendorRepo.Get_Productmapping(brand_Id);
        }

        public List<BrandInfo> Get_Brands()
        {
            return _vendorRepo.Get_Brands();
        }

        public void Insert_Vendor_Product_Mapping_Details(List<ProductInfo> product_List, int user_Id, int vendor_Id,int brand_Id)
        {
            _vendorRepo.Insert_Vendor_Product_Mapping_Details(product_List, user_Id, vendor_Id, brand_Id);
        }

        public List<ProductInfo> Get_Mapped_Product_List(int vendor_Id, int brand_Id)
        {
            return _vendorRepo.Get_Vendor_Mapped_Product_List(vendor_Id, brand_Id);
        }

        public List<AutocompleteInfo> Get_Vendor_Autocomplete(string vendor)
        {
            return _vendorRepo.Get_Vendor_Autocomplete(vendor);
        }
        
    }
}
