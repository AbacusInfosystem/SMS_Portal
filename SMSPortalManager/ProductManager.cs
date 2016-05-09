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
    public class ProductManager
    {
        ProductRepo _productRepo;

        public ProductManager()
        {
            _productRepo = new ProductRepo();
        }

        public void Insert_Product(ProductInfo product)
        {
            _productRepo.Insert_Product(product);
        }

        public void Update_Product(ProductInfo product)
        {
            _productRepo.Update_Product(product);
        }

        public List<ProductInfo> Get_Products(ref PaginationInfo Pager)
        {
            return _productRepo.Get_Products(ref Pager);
        }

        public List<ProductInfo> Get_Products_By_Dealer_Id(int Dealer_Id, int? Category_Id, int? Sub_Category_Id)
        {
            return _productRepo.Get_Products_By_Dealer_Id(Dealer_Id, Category_Id, Sub_Category_Id);
        }

        public List<ProductInfo> Get_Products_By_Id(int Product_Id,ref PaginationInfo Pager)
        {
            return _productRepo.Get_Products_By_Id(Product_Id, ref Pager);
        }

        public List<ProductInfo> Get_Products_By_Ids(string ProductIds)
        {
            return _productRepo.Get_Products_By_Ids(ProductIds);
        }

        public ProductInfo Get_Product_By_Id(int Product_Id)
        {
            return _productRepo.Get_Product_By_Id(Product_Id);
        }
        public bool Check_Existing_Product(string Product_Name)
        {
            return _productRepo.Check_Existing_Product(Product_Name);
        }

        public List<ProductImageInfo> Get_Product_Images(int Product_Id)
        {
            return _productRepo.Get_Product_Images(Product_Id);
        }

        public void Insert_Product_Image(ProductImageInfo productImageInfo)
        {
            _productRepo.Insert_Product_Image(productImageInfo);
        }

        public void Delete_Product_Image(int Product_Image_Id)
        {
            _productRepo.Delete_Product_Image(Product_Image_Id);
        }

        public List<AutocompleteInfo> Get_Product_Autocomplete(string ProductName)
        {
            return _productRepo.Get_Product_Autocomplete(ProductName);
        }

        public List<CategoryInfo> Get_Categories_With_Product_Count(int Dealer_Id)
        {
            return _productRepo.Get_Categories_With_Product_Count(Dealer_Id);
        }

        public List<SubCategoryInfo> Get_Sub_Categories_With_Product_Count(int Category_Id, int Dealer_Id)
        {
            return _productRepo.Get_Sub_Categories_With_Product_Count(Category_Id, Dealer_Id);
        }

    }
}
