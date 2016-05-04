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


        public bool Bulk_Excel_Upload_Default(DataTable dt)
        {

            bool Is_Error = false;

            int i = 1;

            List<ProductInfo> products = new List<ProductInfo>();

            List<SqlParameter> sqlParam = new List<SqlParameter>();

            //List<ExceptionInfo> exceptionList = new List<ExceptionInfo>();

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

                        //_product.Default_Frame_Id = _vRepo.Get_Frame_Id_By_Object(_product.Object_Id, _product.Default_Frame_Name);

                        //_product.Default_Field_Name = Convert.ToString(dr["Default Field Name"]);

                        //_product.Default_Field_Id = _vRepo.Get_Field_Id_By_Object_Frame(_product.Object_Id, _product.Default_Frame_Id, _product.Default_Field_Name);

                        //_product.Default_Dependent_Frame_Name = Convert.ToString(dr["Default Dependent Frame Name"]);

                        //_product.Default_Dependent_Frame_Id = _vRepo.Get_Frame_Id_By_Object(_product.Object_Id, _product.Default_Dependent_Frame_Name);

                        //_product.Default_Dependent_Field_Name = Convert.ToString(dr["Default Dependent Field Name"]);

                        //_product.Default_Dependent_Field_Id = _vRepo.Get_Field_Id_By_Object_Frame(_product.Object_Id, _product.Default_Dependent_Frame_Id, _product.Default_Dependent_Field_Name);

                        //_product.Default_Dependent_Field_Value = Convert.ToString(dr["Default Dependent Field Value"]);

                        //// _default.Default_Value = Convert.ToString(dr["Default Value");
                        //_product.Default_Value = Convert.ToString(dr["Default Value"]);

                        //_product.Is_Mandetory = Convert.ToString(dr["Is Mandetory"]);


                        //if (Valid_Default_Row(_product)) // To check if row get all ids 
                        //{
                        //    if (Get_Default_Validations_By_Objcet_Frame_Field(_product.Object_Id, _product.Default_Frame_Id, _product.Default_Field_Id, ref pager).Count > 0)
                        //    {

                        //        if (Validation_Default_Fields(_product))
                        //        {
                                    _product.Created_By = 1;

                                    _product.Updated_By = 1;

                                    _product.Created_On = DateTime.Now;

                                    _product.Updated_On = DateTime.Now;

                                    Insert_Product(_product);
                        //         }
                        //         else
                        //         {
                        //             Is_Error = true;

                        //            ExceptionInfo exceptionInfo = new ExceptionInfo();

                        //            exceptionInfo.FileName = "Product Upload";

                        //            exceptionInfo.UploadedDate = DateTime.Now;

                        //            exceptionInfo.RowNo = i;

                        //            exceptionInfo.ErrorMessage = exceptionInfo.FileName + " : " + exceptionInfo.UploadedDate + " : " + "Validation Error at row " + i;

                        //            exceptionList.Add(exceptionInfo);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        _product.Created_By = 1;

                        //        _product.Updated_By = 1;

                        //        _product.Created_On = DateTime.Now;

                        //        _product.Updated_On = DateTime.Now;

                        //        Insert_Default_Validation(_product);
                        //    }

                        //}
                        //else
                        //{
                        //    // return Error

                        //    Is_Error = true;

                        //    SAPExceptionInfo sapException = new SAPExceptionInfo();

                        //    sapException.FileName = "Default Validation";

                        //    sapException.UploadedDate = DateTime.Now;

                        //    sapException.RowNo = i;

                        //    sapException.ErrorMessage = sapException.FileName + " : " + sapException.UploadedDate + " : " + "Invalid Entry at row " + i;

                        //    sapExceptionList.Add(sapException);
                        //}

                        i++;
                    }
                }
            }
            catch (Exception ex)
            {

                //Is_Error = true;

                //ExceptionInfo exception = new ExceptionInfo();

                //exception.UploadedDate = DateTime.Now;

                //exception.ErrorMessage = "Replacebale Validation : " + exception.UploadedDate + " : " + ex.Message;
            }


            //_excelRepo.LogExceptions(exceptionList);

            return Is_Error;
        }

         
    }
}
