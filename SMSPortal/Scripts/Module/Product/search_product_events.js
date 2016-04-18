$(function ()
{

    $("#btnUpload").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", {}, call_back);
    });
});

$(document).ready(function () {
    Search_Products();

    $("#btnEdit").click(function () {
        $("#frmProduct").attr("action", "/Product/Get_Product_By_Id/");
        $("#frmProduct").attr("method", "post");
        $("#frmProduct").submit();
    });

    $("#btnSearch").click(function () {
        Search_Products();
    });

});