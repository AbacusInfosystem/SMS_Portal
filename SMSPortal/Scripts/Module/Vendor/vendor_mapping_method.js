function Get_Product_Image_Data() {

    var Brand_Id = $("#drpBrand").val();
    var CurrentPage = $("#hdfCurrentPage").val();
    var Vendor_Id = $("#hdnVendor_Id").val();

    $.ajax(
      {
          url: '/Vendor/Get_Product_By_Brand',
          data: { Brand_Id: Brand_Id, CurrentPage: CurrentPage, Vendor_Id: Vendor_Id },

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

    $('.form-control_new').hide(); // on default, hide textbox
    $('.chkstatus').click(function () {
        var checked_status = this.checked;
        if (checked_status == true) {
            $(".form-control_new").show();
        }
    });

    var product_Ids = "";
    if (data.Products.length > 0)
    {
        for (i = 0; i < data.MappedProducts.length; i++)
        {
            product_Ids+= data.MappedProducts[i].Product_Id + ",";
        }
    }

    var htmlText = "";
    var count = 0;
    if (data.Products.length > 0) {

        htmlText += "<tr>";

        for (i = 0; i < data.Products.length; i++) {
            

            htmlText += "<td style='width:1px'>";

            htmlText += "<img width='100' height='100' id='ProductImg1' src='/UploadedFiles/" + data.Products[i].Product_Image + "'/></br>";

            //htmlText += "< label  " + data.Products[i].Product_Name == null ? "" : data.Products[i].Product_Name + " </br>";

            htmlText += "<label style='text-align:center'>" + data.Products[i].Product_Name == null ? "" : data.Products[i].Product_Name + "</label></br>";

            //htmlText += "< label  " + data.Products[i].Product_Price == null ? "" : data.Products[i].Product_Price + " </br>";

            htmlText += "<input type='text' class='form-control_new input-sm' name='Products[" + i + "].Product_Price' id='txtProduct_Price' placeholder='product price' value='" + (data.Products[i].Product_Price == 0 ? '' : data.Products[i].Product_Price) + "'>";
        
            
            var id = data.Products[i].Product_Id;

            if (product_Ids.indexOf(id) >= 0)
            {
                htmlText += "<input type='checkbox' name='Products[" + i + "].Check' class='chkstatus checkresult' checked  id='CheckId'  value=''  /><br>";
            }
            else
            {
                htmlText += "<input type='checkbox' name='Products[" + i + "].Check' class='chkstatus checkresult'  id='CheckId'  value=''  /><br>";
            }
            
            htmlText += "<input type='hidden' id='hd_Productid" + i + "' name='Products[" + i + "].Product_id' value='" + data.Products[i].Product_Id + "'>";
           

           
            htmlText += "</td>";
 
            count++;
            if (count == 5)
            {
                count = 0;
                htmlText += '</tr>';
                htmlText += '<tr>';
            }

           
        }
        htmlText += "</tr>";
    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='3'> No Product found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#tblVendorProductMappingMaster').find("tr:gt(0)").remove();
    $('#tblVendorProductMappingMaster tr:first').after(htmlText);

    $('input:not(.non-iCheck input:checkbox)').iCheck({
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

}

function PageMore(Id) {

    $('#hdfCurrentPage').val((parseInt(Id) - 1));
    Get_Product_Image_Data();
  

}