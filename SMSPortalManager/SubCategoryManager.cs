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
    public class SubCategoryManager
    {
         SubCategoryRepo _subcategoryRepo;

        public SubCategoryManager()
        {
            _subcategoryRepo = new SubCategoryRepo();
        }

        public List<SubCategoryInfo> Get_Subcategories(ref PaginationInfo pager)
        {
            return _subcategoryRepo.Get_SubCategories(ref pager);
        }

        public List<SubCategoryInfo> Get_Subcategories_By_Id(int subcategory_Id, ref PaginationInfo pager)
        {
            return _subcategoryRepo.Get_Subcategories_By_Id(subcategory_Id, ref pager);
        }

        public List<SubCategoryInfo> Get_SubCategories_By_CategoryId(int Category_Id)
        {
            return _subcategoryRepo.Get_SubCategories_By_CategoryId(Category_Id);
        }
        public void Insert_Sub_Category(SubCategoryInfo subcategory,int user_Id)
        {
            _subcategoryRepo.Insert_Sub_Category(subcategory, user_Id);
        }

        public void Update_Sub_Category(SubCategoryInfo subcategory, int user_Id)
        {
            _subcategoryRepo.Update_Sub_Category(subcategory, user_Id);
        }

        public SubCategoryInfo Get_Subcategory_By_Id(int subcategory_Id)
        {
            return _subcategoryRepo.Get_Subcategory_By_Id(subcategory_Id);
        }

        public List<AutocompleteInfo> Get_Subcategory_Autocomplete(string subcategory)
        {
            return _subcategoryRepo.Get_Subcategory_Autocomplete(subcategory);
        }

        public bool Check_Existing_Sub_Category(string subcategory)
        {
            return _subcategoryRepo.Check_Existing_SubCategory(subcategory);
        }

         
    }
}
