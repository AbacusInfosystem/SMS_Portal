﻿$(function () {
    Search_Vendors();
    InitializeAutoComplete($('#txtVendorName'));
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

    $("#btnAddUser").click(function () {

        $("#frmVendor").attr("action", "/vendor/add-vendor-user/");

        $("#frmVendor").attr("method", "post");

        $("#frmVendor").submit();
    });
});
