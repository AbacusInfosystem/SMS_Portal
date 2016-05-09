using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalRepo
{
    public class ProductRepo
    {
        SQLHelper _sqlRepo;

        public ProductRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public void Insert_Product(ProductInfo product)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Product(product), StoreProcedures.Insert_Product_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Product(ProductInfo product)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Product(product), StoreProcedures.Update_Product_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Product(ProductInfo product)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            if (product.Product_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Product_Id", product.Product_Id));
            }
            sqlParams.Add(new SqlParameter("@Product_Name", product.Product_Name));
            sqlParams.Add(new SqlParameter("@Product_Description", product.Product_Description));
            sqlParams.Add(new SqlParameter("@Product_Price", product.Product_Price));
            sqlParams.Add(new SqlParameter("@Brand_Id", product.Brand_Id));
            sqlParams.Add(new SqlParameter("@Category_Id", product.Category_Id));
            sqlParams.Add(new SqlParameter("@SubCategory_Id", product.SubCategory_Id));
            sqlParams.Add(new SqlParameter("@Is_Biddable", product.Is_Biddable));
            sqlParams.Add(new SqlParameter("@Is_Active", product.Is_Active));
            if (product.Product_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", product.Created_On));
                sqlParams.Add(new SqlParameter("@Created_By", product.Created_By));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", product.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", product.Updated_By));
            return sqlParams;
        }

        public List<ProductInfo> Get_Products(ref PaginationInfo Pager)
        {
            List<ProductInfo> products = new List<ProductInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Product_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                 products.Add(Get_Product_Values(dr));
            }
            return products;
        }

        public List<ProductInfo> Get_Products_By_Dealer_Id(int Dealer_Id, int? Category_Id, int? Sub_Category_Id)
        {
            List<ProductInfo> products = new List<ProductInfo>();
            List<SqlParameter> sqlParamList = new List<SqlParameter>();

            sqlParamList.Add(new SqlParameter("@Dealer_Id", Dealer_Id));
            sqlParamList.Add(new SqlParameter("@Category_Id", Category_Id));
            sqlParamList.Add(new SqlParameter("@Sub_Category_Id", Sub_Category_Id));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Products_By_Dealer_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                products.Add(Get_Products_By_Dealer_Id_Values(dr));
            }
            return products;
        }

        private ProductInfo Get_Products_By_Dealer_Id_Values(DataRow dr)
        {
            ProductInfo product = new ProductInfo();

            product.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            product.Product_Name = Convert.ToString(dr["Product_Name"]);
            product.Product_Description = Convert.ToString(dr["Product_Description"]);
            product.Product_Price = Convert.ToDecimal(dr["Product_Price"]);
            product.Product_Image = Convert.ToString(dr["Image_Code"]);
            product.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);
            product.Brand_Name = Convert.ToString(dr["Brand_Name"]);
            product.Category_Id = Convert.ToInt32(dr["Category_Id"]);
            product.Category_Name = Convert.ToString(dr["Category_Name"]);
            product.SubCategory_Id = Convert.ToInt32(dr["SubCategory_Id"]);
            product.SubCategory_Name = Convert.ToString(dr["SubCategory_Name"]);
            product.Is_Biddable = Convert.ToBoolean(dr["Is_Biddable"]);
            product.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            product.Created_On = Convert.ToDateTime(dr["Created_On"]);
            product.Created_By = Convert.ToInt32(dr["Created_By"]);
            product.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            product.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return product;
        }

        public List<ProductInfo> Get_Products_By_Id(int Product_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Product_Id", Product_Id));

            List<ProductInfo> products = new List<ProductInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Product_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                products.Add(Get_Product_Values(dr));
            }
            return products;
        }

        public List<ProductInfo> Get_Products_By_Ids(string ProductIds)
        {
            List<ProductInfo> products = new List<ProductInfo>();
            List<SqlParameter> sqlParamList = new List<SqlParameter>();

            sqlParamList.Add(new SqlParameter("@ProductIds", ProductIds));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Products_By_Ids_sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                products.Add(Get_Products_By_Ids_Values(dr));
            }
            return products;
        }

        private ProductInfo Get_Products_By_Ids_Values(DataRow dr)
        {
            ProductInfo product = new ProductInfo();

            product.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            product.Product_Name = Convert.ToString(dr["Product_Name"]);
            product.Product_Description = Convert.ToString(dr["Product_Description"]);
            product.Product_Price = Convert.ToDecimal(dr["Product_Price"]);
            product.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);
            product.Category_Id = Convert.ToInt32(dr["Category_Id"]);
            product.SubCategory_Id = Convert.ToInt32(dr["SubCategory_Id"]);
            product.Is_Biddable = Convert.ToBoolean(dr["Is_Biddable"]);
            product.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            product.Created_On = Convert.ToDateTime(dr["Created_On"]);
            product.Created_By = Convert.ToInt32(dr["Created_By"]);
            product.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            product.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return product;
        }

        public ProductInfo Get_Product_By_Id(int Product_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Product_Id", Product_Id));

            ProductInfo product = new ProductInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Product_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            
            foreach (DataRow dr in dt.Rows)
            {
                product = Get_Product_Values(dr);
            }
            return product;
        }

        private ProductInfo Get_Product_Values(DataRow dr)
        {
            ProductInfo product = new ProductInfo();

            product.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            product.Product_Name = Convert.ToString(dr["Product_Name"]);
            product.Product_Description = Convert.ToString(dr["Product_Description"]);
            product.Product_Price = Convert.ToDecimal(dr["Product_Price"]);
            product.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);
            product.Brand_Name = Convert.ToString(dr["Brand_Name"]);
            product.Category_Id = Convert.ToInt32(dr["Category_Id"]);
            product.Category_Name = Convert.ToString(dr["Category_Name"]);
            product.SubCategory_Id = Convert.ToInt32(dr["SubCategory_Id"]);
            product.SubCategory_Name = Convert.ToString(dr["SubCategory_Name"]);
            product.Is_Biddable = Convert.ToBoolean(dr["Is_Biddable"]);
            product.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            product.Created_On = Convert.ToDateTime(dr["Created_On"]);
            product.Created_By = Convert.ToInt32(dr["Created_By"]);
            product.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            product.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return product;
        }

        public bool Check_Existing_Product(string Product_Name)
        {
            bool check = false;
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Product_Name", Product_Name));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_Product.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    check = Convert.ToBoolean(dr["Check_Product"]);
                }
            }
            return check;
        }

        private ProductImageInfo Get_Product_Image_Values(DataRow dr)
        {
            ProductImageInfo productImage = new ProductImageInfo();

            productImage.Product_Image_Id = Convert.ToInt32(dr["Product_Image_Id"]);
            productImage.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            productImage.Image_Code = Convert.ToString(dr["Image_Code"]);
            productImage.Is_Default = Convert.ToBoolean(dr["Is_Default"]);            
            productImage.Created_On = Convert.ToDateTime(dr["Created_On"]);
            productImage.Created_By = Convert.ToInt32(dr["Created_By"]);
            productImage.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            productImage.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return productImage;
        }

        public List<ProductImageInfo> Get_Product_Images(int Product_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Product_Id", Product_Id));

            List<ProductImageInfo> productImages = new List<ProductImageInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Product_Images_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                productImages.Add(Get_Product_Image_Values(dr));
            }
            return productImages;
        }

        private List<SqlParameter> Set_Values_In_Product_Image(ProductImageInfo productImageInfo)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            if (productImageInfo.Product_Image_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Product_Image_Id", productImageInfo.Product_Image_Id));
            }
            sqlParams.Add(new SqlParameter("@Product_Id", productImageInfo.Product_Id));
            sqlParams.Add(new SqlParameter("@Image_Code", productImageInfo.Image_Code));
            sqlParams.Add(new SqlParameter("@Is_Default", productImageInfo.Is_Default));
            if (productImageInfo.Product_Image_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", productImageInfo.Created_On));
                sqlParams.Add(new SqlParameter("@Created_By", productImageInfo.Created_By));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", productImageInfo.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", productImageInfo.Updated_By));
            return sqlParams;
        }

        public void Insert_Product_Image(ProductImageInfo productImageInfo )
        {             
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Product_Image(productImageInfo), StoreProcedures.Insert_Product_Image_Sp.ToString(), CommandType.StoredProcedure);

        }

        public List<AutocompleteInfo> Get_Product_Autocomplete(string ProductName)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", ProductName == null ? System.String.Empty : ProductName.Trim()));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Product_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AutocompleteInfo auto = new AutocompleteInfo();
                    auto.Label = Convert.ToString(dr["Label"]);
                    auto.Value = Convert.ToInt32(dr["Value"]);
                    autoList.Add(auto);
                }
            }
            return autoList;
        }

        public void Delete_Product_Image(int Product_Image_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Product_Image_Id", Product_Image_Id));
            _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Delete_Product_Image_Sp.ToString(), CommandType.StoredProcedure);
        }

        public List<CategoryInfo> Get_Categories_With_Product_Count(int Dealer_Id)
        {
            List<CategoryInfo> Categories = new List<CategoryInfo>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Dealer_Id", Dealer_Id));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_Categories_With_Product_Count_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                Categories.Add(Get_Categories_With_Product_Count_Values(dr));
            }
            return Categories;
        }

        private CategoryInfo Get_Categories_With_Product_Count_Values(DataRow dr)
        {
            CategoryInfo category = new CategoryInfo();

            category.Category_Id = Convert.ToInt32(dr["Category_Id"]);

            if (!dr.IsNull("Category_Name"))
                category.Category_Name = Convert.ToString(dr["Category_Name"]);
            category.IsActive = Convert.ToBoolean(dr["IsActive"]);
            category.Created_On = Convert.ToDateTime(dr["Created_On"]);
            category.Created_By = Convert.ToInt32(dr["Created_By"]);
            category.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            category.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            category.Product_Count = Convert.ToInt32(dr["Product_Count"]);
            return category;
        }

        public List<SubCategoryInfo> Get_Sub_Categories_With_Product_Count(int Category_Id, int Dealer_Id)
        {
            List<SubCategoryInfo> subcategories = new List<SubCategoryInfo>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Category_Id", Category_Id));
            sqlParams.Add(new SqlParameter("@Dealer_Id", Dealer_Id));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_Sub_Categories_With_Product_Count_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                subcategories.Add(Get_Product_Sub_Categories_With_Product_Count_Values(dr));
            }

            return subcategories;
        }

        private SubCategoryInfo Get_Product_Sub_Categories_With_Product_Count_Values(DataRow dr)
        {
            SubCategoryInfo subcategory = new SubCategoryInfo();

            subcategory.Subcategory_Id = Convert.ToInt32(dr["Sub_Category_Id"]);
            subcategory.Subcategory_Name = Convert.ToString(dr["Sub_Category_Name"]);
            subcategory.Category_Id = Convert.ToInt32(dr["Category_Id"]);
            subcategory.IsActive = Convert.ToBoolean(dr["IsActive"]);
            if (subcategory.IsActive == true)
            {
                subcategory.Status = "Active";
            }
            else
            {
                subcategory.Status = "InActive";
            }
            subcategory.Created_Date = Convert.ToDateTime(dr["Created_On"]);
            subcategory.Created_By = Convert.ToInt32(dr["Created_By"]);
            subcategory.Updated_Date = Convert.ToDateTime(dr["Updated_On"]);
            subcategory.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            subcategory.Product_Count = Convert.ToInt32(dr["Product_Count"]);

            return subcategory;
        }
    }
}
