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

        public List<VendorInfo> Get_Vendors(ref PaginationInfo pager,int brand_Id)
        {
            return _vendorRepo.Get_Vendors(ref pager, brand_Id);
        }

        public List<VendorInfo> Get_Vendor_By_Id_List(int vendor_Id, ref PaginationInfo pager)
        {
            return _vendorRepo.Get_Vendor_By_Id_List(vendor_Id, ref pager);
        }

        public bool Check_Existing_Vendor(string vendor_Name)
        {
            return _vendorRepo.Check_Existing_Vendor(vendor_Name);
        }

        public List<AutocompleteInfo> Get_Vendor_Autocomplete(string vendor)
        {
            return _vendorRepo.Get_Vendor_Autocomplete(vendor);
        }

        public void Send_Registration_Email(string vendor_Email_Id, string vendor_Name)
        {
            _vendorRepo.Send_Registration_Email(vendor_Email_Id, vendor_Name);
        }

        public VendorInfo Get_Vendor_Profile_Data_By_User_Id(int vendor_Id)
        {
            return _vendorRepo.Get_Vendor_Profile_Data_By_User_Id(vendor_Id);
        }

        public void Update_Vendor_Profile(VendorInfo vendor, int user_Id)
        {
            _vendorRepo.Update_Vendor_Profile(vendor, user_Id);
        }


        public void Update_Vendor_FileName(int vendor_Id, string vendor_Name)
        {
            _vendorRepo.Update_Vendor_FileName(vendor_Id, vendor_Name);
        }


        public VendorInfo Get_Vendor_Logo_By_Id(int vendor_Id)
        {
            return _vendorRepo.Get_Vendor_Logo_By_Id(vendor_Id);
        }

        public List<BrandInfo> Get_Brands()
        {
            return _vendorRepo.Get_BrandS();
        }
    }
}
