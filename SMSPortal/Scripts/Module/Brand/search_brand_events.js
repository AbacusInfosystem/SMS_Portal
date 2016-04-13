$(function () {

    $("#btnUploadLogo").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/brand/Add_Brand_Logo", {}, call_back);
    });
});

$(document).ready(function ()
{
    Search_Brands();

    $("#btnEdit").click(function () {
        $("#frmBrand").attr("action", "/Brand/Get_Category_By_Id");
        $("#frmBrand").attr("method", "post");
        $("#frmBrand").submit();
    });

    $("#btnSearch").click(function () {
        Search_Brands();
    });

});