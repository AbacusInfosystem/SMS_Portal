function Sales_Details(id) {

    $("#div_Parent_Modal_Fade").find(".modal-body").load("/Dashboard/Display_Sales_Deatils", { Id: id }, call_back);
}

function call_back(data) {

    $('#div_Parent_Modal_Fade').modal('show');
    $("#div_Parent_Modal_Fade").find(".modal-title").text("Order Details");

    //$('#btnUpload').click(function (event) {
    //    if ($('#frmUploadBrandLogo').valid()) {
    //        $('#frmUploadBrandLogo').attr("action", "/brand/brands-upload-logo");
    //        $('#frmUploadBrandLogo').attr("method", "post");
    //        $('#frmUploadBrandLogo').submit();
    //    }

   // })
}

 
 

