$(function () {

    $("#btnUpload").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", {}, call_back);
    });

});