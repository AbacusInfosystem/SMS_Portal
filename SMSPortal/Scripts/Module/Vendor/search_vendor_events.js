﻿$(function () {

    Search_Vendors();

    $("#btnEdit").click(function () {

        $("#frmVendor").attr("action", "/Vendor/Get_Vendor_By_Id/");

        $("#frmVendor").attr("method", "post");

        $("#frmVendor").submit();
    });

    $("#btnSearch").click(function () {

        Search_Vendors();

    });

    $("#btnAddProductMapping").click(function () {

        $("#frmVendor").attr("action", "/Vendor/Add_Product_Mapping/");

        $("#frmVendor").attr("method", "post");

        $("#frmVendor").submit();

    });
});
