﻿using SMSPortalHelper;
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

        ExcelReader _excelRepo = null;

        public ProductRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public void Insert_Product(ProductInfo product,int user_id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Product(product,user_id), StoreProcedures.Insert_Product_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Product(ProductInfo product,int user_id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Product(product,user_id), StoreProcedures.Update_Product_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Product(ProductInfo product, int user_id)
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
                sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlParams.Add(new SqlParameter("@Created_By", user_id));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_id));
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

        public void Insert_Product_Image(ProductImageInfo productImageInfo)
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

        public List<BrandInfo> Get_Brands()
        {
            List<BrandInfo> brandList = new List<BrandInfo>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Brand_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BrandInfo list = new BrandInfo();

                    if (!dr.IsNull("Brand_Id"))

                        list.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);

                    if (!dr.IsNull("Brand_Name"))

                        list.Brand_Name = Convert.ToString(dr["Brand_Name"]);

                    brandList.Add(list);
                }
            }

            return brandList;
        }

        public List<CategoryInfo> Get_Categorys()
        {
            List<CategoryInfo> categorys = new List<CategoryInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Category_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                categorys.Add(Get_Category_Values(dr));
            }
            return categorys;
        }

        private CategoryInfo Get_Category_Values(DataRow dr)
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
            return category;
        }

        public List<SubCategoryInfo> Get_SubCategories_By_CategoryId(int category_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();

            sqlParam.Add(new SqlParameter("@Category_Id", category_Id));

            List<SubCategoryInfo> subcategories = new List<SubCategoryInfo>();

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Sub_Category_By_Category_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                subcategories.Add(Get_SubCategory_Values(dr));
            }

            return subcategories;
        }

        private SubCategoryInfo Get_SubCategory_Values(DataRow dr)
        {
            SubCategoryInfo subcategory = new SubCategoryInfo();

            subcategory.Subcategory_Id = Convert.ToInt32(dr["Sub_Category_Id"]);
            subcategory.Subcategory_Name = Convert.ToString(dr["Sub_Category_Name"]);
            subcategory.Category_Id = Convert.ToInt32(dr["Sub_Category_Id"]);
            subcategory.Category_Name = Convert.ToString(dr["Category_Name"]);
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

            return subcategory;
        }

        public void Delete_Product_Image(int Product_Image_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Product_Image_Id", Product_Image_Id));
            _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Delete_Product_Image_Sp.ToString(), CommandType.StoredProcedure);
        }

        public bool Bulk_Excel_Upload_Default(DataTable dt,int user_id)
        {

            bool Is_Error = false;

            int i = 1;

            List<ProductInfo> products = new List<ProductInfo>();
            List<SqlParameter> sqlParam = new List<SqlParameter>();

            List<ExceptionInfo> exceptionList = new List<ExceptionInfo>();

            PaginationInfo pager = new PaginationInfo();

            pager.IsPagingRequired = false;

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductInfo _product = new ProductInfo();

                        _product.Product_Name = Convert.ToString(dr["Product Name"]);

                        _product.Product_Description = Convert.ToString(dr["Product Description"]);

                        _product.Product_Price = Convert.ToDecimal(dr["Product Price"]);

                        _product.Brand_Id = Get_Brand_Id_By_Name(dr["Brand"].ToString());

                        _product.Category_Id = Get_Category_Id_By_Name(dr["Category"].ToString());

                        _product.SubCategory_Id = Get_SubCategory_Id_By_Name(_product.Category_Id, dr["SubCategory"].ToString());

                        if (dr["Is Biddable"].ToString().ToLower() == "yes")
                        {
                            _product.Is_Biddable = true;
                        }
                        else 
                        {
                            _product.Is_Biddable = false;
                        }                        

                        if (Valid_Default_Row(_product)) // To check if row get all ids 
                        {

                                    _product.Is_Active = true;

                                    _product.Created_By = 1;

                                    _product.Updated_By = 1;

                                    _product.Created_On = DateTime.Now;

                                    _product.Updated_On = DateTime.Now;

                            if (Check_Existing_Product(_product.Product_Name))
                            {
                                Update_Product(_product,user_id);
                            }
                            else 
                            {
                                    Insert_Product(_product,user_id);
                            }
                        }
                        else
                        {
                            // return Error
                            Is_Error = true;

                            ExceptionInfo exception = new ExceptionInfo();

                            exception.FileName = "Default Validation";

                            exception.UploadedDate = DateTime.Now;

                            exception.RowNo = i;

                            exception.ErrorMessage = exception.FileName + " : " + exception.UploadedDate + " : " + "Invalid Entry at row " + i;

                            exceptionList.Add(exception);
                        }

                        i++;
                    }
                }
            }
            catch (Exception ex)
            {

                Is_Error = true;

                ExceptionInfo exception = new ExceptionInfo();

                exception.UploadedDate = DateTime.Now;

                exception.ErrorMessage = "Replacebale Validation : " + exception.UploadedDate + " : " + ex.Message;
            }

            _excelRepo.LogExceptions(exceptionList);

            return Is_Error;
        }

        public bool Valid_Default_Row(ProductInfo _product)
        {
            bool valid = true;

            if (_product.Brand_Id == 0)
            {
                valid = false;
            }

            if (_product.Category_Id == 0)
            {
                valid = false;
            }

            if (_product.SubCategory_Id == 0)
            {
                valid = false;
            }

            return valid;
        }

        public int Get_SubCategory_Id_By_Name(int Category_Id, string SubCategory_Name)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@CategoryId", Category_Id));
            sqlparam.Add(new SqlParameter("@SubCategoryName", SubCategory_Name));

            return Convert.ToInt32(_sqlRepo.ExecuteScalerObj(sqlparam, StoreProcedures.Get_SubCategory_Id_By_Name.ToString(), CommandType.StoredProcedure));
        }

        public int Get_Category_Id_By_Name(string Category_Name)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@CategoryName", Category_Name));

            return Convert.ToInt32(_sqlRepo.ExecuteScalerObj(sqlparam, StoreProcedures.Get_Category_Id_By_Name.ToString(), CommandType.StoredProcedure));
        }

        public int Get_Brand_Id_By_Name(string Brand_Name)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@BrandName", Brand_Name));

            return Convert.ToInt32(_sqlRepo.ExecuteScalerObj(sqlparam, StoreProcedures.Get_Brand_Id_By_Name.ToString(), CommandType.StoredProcedure));
        }

         
    }
}
