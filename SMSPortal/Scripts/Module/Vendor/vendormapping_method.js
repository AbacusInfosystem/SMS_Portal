function Get_Product_Image_Data() {

    var Brand_Id = $("#drpBrand").val();
    var CurrentPage = $("#hdfCurrentPage").val();

    $.ajax(
      {
          url: '/Vendor/Get_Product_By_Brand',
          data: { Brand_Id: Brand_Id, CurrentPage: CurrentPage },

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
    var htmlText = "";
    var count = 0;
    if (data.Products.length > 0) {

        htmlText += "<tr>";

        for (i = 0; i < data.Products.length; i++) {
            

            htmlText += "<td>";

            htmlText += "<img width='100' height='100' id='ProductImg1' src='~/UploadedFiles/='" + data.Products[i].Product_Image + "'/></br>";

            htmlText += "<label "+ data.Products[i].Product_Name == null ? "" : data.Products[i].Product_Name +" </br>";

            htmlText += "<input type='checkbox' class='chkstatus checkresult' style='align:center' id='CheckId'  value='" + data.Products[i].Product_Id + "'  /><br>";
            
            htmlText += "<input type='hidden' name='" + data.Products[i].Product_id + "' id=hd_Productid1' value='" + data.Products[i].Product_Id + "'>";
         
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
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
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