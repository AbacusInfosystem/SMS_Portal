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

        public List<SubCategoryInfo> Get_Subcategory_Modules()
        {
            return _subcategoryRepo.Get_Subcategory_Modules();
        }

        public List<CategoryInfo> Get_Categories()
        {
            return _subcategoryRepo.Get_Categories();
        }

        public List<SubCategoryInfo> Get_Subcategories(ref PaginationInfo pager)
        {
            return _subcategoryRepo.Get_SubCategories(ref pager);
        }

        public List<SubCategoryInfo> Get_Subcategories_By_Id(int module_Id, ref PaginationInfo pager)
        {
            return _subcategoryRepo.Get_Subcategory_By_Module_Id(module_Id, ref pager);
        }

        public void Insert_Sub_Category(SubCategoryInfo subcategory)
        {
            _subcategoryRepo.Insert_Sub_Category(subcategory);
        }

        public void Update_Sub_Category(SubCategoryInfo subcategory)
        {
            _subcategoryRepo.Update_Sub_Category(subcategory);
        }
    }
}
