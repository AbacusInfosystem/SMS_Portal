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
    public class CategoryManager
    {
        CategoryRepo _categoryRepo;

        public CategoryManager()
        {
            _categoryRepo = new CategoryRepo();
        }

        public int Insert_Category(CategoryInfo category)
        {
            return _categoryRepo.Insert_Category(category);
        }

        public void Update_Category(CategoryInfo category)
        {
            _categoryRepo.Update_Category(category);
        }

        public List<CategoryInfo> Get_Categorys(ref PaginationInfo Pager)
        {
            return _categoryRepo.Get_Categorys(ref Pager);
        }

        public List<CategoryInfo> Get_Categorys_By_Id(int  Category_Id, ref PaginationInfo Pager)
        {
            return _categoryRepo.Get_Categorys_By_Id(Category_Id, ref Pager);
        }

        public CategoryInfo Get_Category_By_Id(int Category_Id)
        {
            return _categoryRepo.Get_Category_By_Id(Category_Id);
        }

        public void Delete_Category_By_Id(int Category_Id)
        {
            _categoryRepo.Delete_Category_By_Id(Category_Id);
        }

        public bool Check_Existing_Category(string Category_Name)
        {
            return _categoryRepo.Check_Existing_Category(Category_Name);
        }

        public List<AutocompleteInfo> Get_Category_Autocomplete(string Category)
        {
            return _categoryRepo.Get_Category_Autocomplete(Category);
        }

        public int Get_Category_Id_By_Name(string Category_Name)
        {
            return _categoryRepo.Get_Category_Id_By_Name(Category_Name);
        }
    }
}
