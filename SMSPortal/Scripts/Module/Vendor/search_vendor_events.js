$(function () {

    $("#btnAddBankDetails").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/vendor/Add_Bank_Details", {}, call_back);
    });

    $("#btnAddProductMapping").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/vendor/Add_Product_Mapping", {}, prod_map_call_back);
    });

});
$(document).ready(function () {
    Search_Vendors();

    $("#btnEdit").click(function () {
        $("#frmVendor").attr("action", "/Vendor/Get_Vendor_By_Id/");
        $("#frmVendor").attr("method", "post");
        $("#frmVendor").submit();
    });

    $("#btnSearch").click(function () {
        Search_Vendors();
    });

});