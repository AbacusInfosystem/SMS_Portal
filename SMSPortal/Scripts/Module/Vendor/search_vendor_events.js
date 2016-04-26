$(function () {

    InitializeAutoComplete($('#txtVendorName'));

    Search_Vendors();

    $("#btnEdit").click(function () {

        $("#frmVendor").attr("action", "/Vendor/edit-vendor-details/");

        $("#frmVendor").attr("method", "post");

        $("#frmVendor").submit();
    });

    $("#btnSearch").click(function () {

        Search_Vendors();

    });

    $("#btnAddProductMapping").click(function () {

        $("#frmVendor").attr("action", "/Vendor/get-product-list-omchange-brands/");

        $("#frmVendor").attr("method", "post");

        $("#frmVendor").submit();

    });
});
