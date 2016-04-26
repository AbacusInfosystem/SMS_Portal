$(function () {

    $("#btnUploadLogo").click(function (event) {
        
        var BrandId=$('#hdnBrand_Id').val();         
        $("#div_Parent_Modal_Fade").find(".modal-body").load("/brand/Add_Brand_Logo", { Id: BrandId }, call_back);
    });

    


});

$(document).ready(function ()
{
    InitializeAutoComplete($('#txtBrand_Name'));

    $('#hdfCurrentPage').val(0);

    Search_Brands();

    GetBrandList();        

    $("#btnEdit").click(function () {
        $("#frmBrand").attr("action", "/brand/get-brand/");
        $("#frmBrand").attr("method", "post");
        $("#frmBrand").submit();
    });

    $("#btnSearch").click(function () {
        Search_Brands();
    });

    
});