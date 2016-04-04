$(function () {

    $("#btnAddBankDetails").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/vendor/Add_Bank_Details", {}, call_back);
    });

    $("#btnAddProductMapping").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/vendor/Add_Product_Mapping", {}, prod_map_call_back);
    });

});