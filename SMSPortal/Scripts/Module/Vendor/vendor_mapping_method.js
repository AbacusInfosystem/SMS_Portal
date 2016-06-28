function Get_Product_Image_Data() {

    var CurrentPage = $("#hdfCurrentPage").val();

    var Vendor_Id = $("#hdnVendor_Id").val();


    $.ajax(
      {
          url: '/Vendor/Get_Product_By_Brand',

          data: { CurrentPage: CurrentPage, Vendor_Id: Vendor_Id },

          method: 'GET',

          async: false,

          success: function (data) {
              if (data != null) {
                  Bind_Vendor_Product_Grid(data);
              }
          }
      });
}


function Bind_Vendor_Product_Grid(data) {

    //$('.form-control_new').hide(); // on default, hide textbox
    //$('.chkstatus').click(function () {
    //    var checked_status = this.checked;
    //    if (checked_status == true) {
    //        $(".form-control_new").show();
    //    }
    //});
    $('#tblVendorProductMappingMaster').html("");
    var product_Ids = "";
    var product_price = "";
    //if (data.Products.length > 0)

    //{
    //    for (i = 0; i < data.MappedProducts.length; i++)

    //    {
    //        product_Ids += data.MappedProducts[i].Product_Id + ",";
    //        product_price += data.MappedProducts[i].Product_Price + ",";
    //    }
    //}

    var htmlText = "";

    var count = 0;

    if (data.Products.length > 0) {

        var Brand_Id = "";

        for (i = 0; i < data.Brands.length; i++) {

            Brand_Id = data.Brands[i].Brand_Id

            htmlText += "<div class='col-md-12'>";

            htmlText += "<div class='form-group'>";

            htmlText += "<div class='box-header with-border'>";

            htmlText += "<label style='text-align:center'>" + data.Brands[i].Brand_Name + "</label>";

            htmlText += "</div>";

            htmlText += "</div>";

            htmlText += "</div>";


            for (j = 0; j < data.Products.length; j++) {

                if (Brand_Id == data.Products[j].Brand_Id)
                {
                    htmlText += "<div class='col-md-3'>";

                    htmlText += "<input type='hidden' id='hdn_MasterProductPrice' name='Products[" + j + "].MasterProductPrice' value='" + data.Products[j].MasterProductPrice + "' />";

                    htmlText += "<img width='100' height='100' id='ProductImg1' src='/UploadedFiles/" + data.Products[j].Product_Image + "'/></br>";

                    htmlText += "<label style='text-align:center'>" + data.Products[j].Product_Name == null ? "" : data.Products[j].Product_Name + "</label></br>";

                    if (data.Products[j].Is_Mapped == true) {
                        htmlText += "<input type='checkbox' name='Products[" + j + "].Check' class='chkstatus checkresult' checked  id='CheckId'  value=''  /><br>";
                    }
                    else {
                        htmlText += "<input type='checkbox' name='Products[" + j + "].Check' class='chkstatus checkresult'  id='CheckId'  value=''  /><br>";
                    }

                    htmlText += "<input type='text' class='form-control_new input-sm' name='Products[" + j + "].Product_Price' id='txtProduct_Price_" + j + "' placeholder='product price' value='" + (data.Products[j].Product_Price == 0 ? '' : data.Products[j].Product_Price) + "'></br>";

                    htmlText += "<input type='hidden' id='hd_Productid" + j + "' name='Products[" + j + "].Product_id' value='" + data.Products[j].Product_Id + "'>";

                    htmlText += "<input type='hidden' id='hd_Brand_Id" + j + "' name='Products[" + j + "].Brand_Id' value='" + data.Products[j].Brand_Id + "'>";

                    htmlText += "</br>";

                    htmlText += "</div>";
                }
                //else
                //{
                //    htmlText += "<div class='col-md-6'> No Products Mapped for this brand";

                //    htmlText += "</div>";

                //    break;
                //}
            }

        }

    }

    else {
 

        htmlText += "<div class='col-md-3'> No Product found.";

        htmlText += "</div>";

     
    }

    //$('#tblVendorProductMappingMaster').find("tr:gt(0)";

    //$('#tblVendorProductMappingMaster').after(htmlText);
    $('#tblVendorProductMappingMaster').html(htmlText);

    $('input:not(.non-iCheck input:checkbox)').iCheck(
        {
            checkboxClass: 'icheckbox_square-green_new',
            radioClass: 'iradio_square-green_new',
            increaseArea: '20%', // optional
        });



    if (data.Products.length > 0) {
        $('#hdfCurrentPage').val(data.Pager.CurrentPage);

        if (data.Pager.PageHtmlString != null || data.Pager.PageHtmlString != "") {
            $('.pagination').html(data.Pager.PageHtmlString);
        }
    }

    else {
        $('.pagination').html("");
    }

    $(".form-control_new").each(function () {
        $(this).rules("add", { validate_product_price: true });
    });

}

function PageMore(Id) {

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Get_Product_Image_Data();

}

function arrHasValue(A, B) {
    var i, j, n;
    n = A.length;

    for (i = 0; i < n; i++) {
        if (A[i] == B) return true;
    }
    return false;
}


