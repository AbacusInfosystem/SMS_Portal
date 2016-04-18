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
        public List<ProductInfo> Get_Products_By_Name(string Product_Name,ref PaginationInfo Pager)
        {
            return _productRepo.Get_Products_By_Name( Product_Name,ref Pager);
        }

        public ProductInfo Get_Product_By_Id(int Product_Id)
        {
            return _productRepo.Get_Product_By_Id(Product_Id);
        }
        public bool Check_Existing_Product(string Product_Name)
        {
            return _productRepo.Check_Existing_Product(Product_Name);
        }
    }
}
