
//function Search_Products_Images() {
//    var productViewModel =
//        {
//            Filter:
//                {
//                    Product_Name: $('#txtProduct_Name').val(),
//                },
//            Pager:
//                {
//                    CurrentPage: $('#hdfCurrentPage').val(),
//                },
//        }

//    $('#divSearchGridOverlay').show();

    //CallAjax("/Product/Upload_Product_Image/", "json", JSON.stringify(productViewModel), "POST", "application/json", false, Bind_Products_Image_Grid, "", null);

    //$.ajax({  
    //    url: '/Product/Upload_Product_Image',
    //    datatype: "json",  
    //    type: "post",  
    //    contenttype: 'application/json; charset=utf-8',  
    //    async: false,
    //    success: Bind_Products_Image_Grid,
    //    error: function (xhr) {  
    //        alert(xhr.responseText);
    //    }  
    //});  
 
//}

//function Bind_Products_Image_Grid(data) {
//    var htmlText = "";
//    alert(data.toString());
//    if (data.ImagesList.length > 0) {
//        for (i = 0; i < data.ImagesList.length; i++) {
//            htmlText += "<div class='col-md-2'>";

//            htmlText += "<div class='row'><div class='form-group'>";

//            htmlText += "<img  id='" + data.ImagesList[i].Product_Image_Id + "' src='/UploadedFiles/" + data.ImagesList[i].Image_Code + "' width='100' height='100' />";

//            htmlText += "</div>";             

//            htmlText += "</tr>";
//        }
//    }
//    //else {
//    //    htmlText += "<tr>";

//    //    htmlText += "<td colspan='1'> No Record found.";

//    //    htmlText += "</td>";

//    //    htmlText += "</tr>";
//    //}

//    //$('#tblProductImage').find("tr:gt(0)").remove();
//    //$('#tblProductImage tr:first').after(htmlText);
//    $('#dvImageAttachments').after(htmlText);
    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

//    //if (data.Products.length > 0) {
//    //    $('#hdfCurrentPage').val(data.Pager.CurrentPage);
//    //    if (data.Pager.PageHtmlString != null || data.Pager.PageHtmlString != "") {
//    //        $('.pagination').html(data.Pager.PageHtmlString);
//    //    }
//    //}
//    //else {
//    //    $('.pagination').html("");
//    //}

//    //$("#divSearchGridOverlay").hide();

//    //$('[name="r1"]').on('ifChanged', function (event) {
//    //    if ($(this).prop('checked')) {
//    //        $("#hdnProduct_Id").val(this.id.replace("r1_", ""));
//    //        $("#btnEdit").show();
//    //        $("#btnUpload").show();
//    //        $("#btnDelete").show();

//    //    }
//    //});

//}

function PageMore(Id) {

    $("#btnEdit").hide();
    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    //Search_Products_Images();

}

