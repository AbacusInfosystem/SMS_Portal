﻿$(function () {

    $("#btnUploadLogo").click(function (event) {

        var BrandId = $('#hdnBrand_Id').val();
        $("#div_Parent_Modal_Fade").find(".modal-body").load("/brand/Add_Brand_Logo", { Id: BrandId }, call_back);
    });

    InitializeAutoComplete($('#txtBrand_Name'));

    $('#hdfCurrentPage').val(0);

    Search_Brands();

    $("#btnEdit").click(function () {
        $("#frmBrand").attr("action", "/brand/get-brand/");
        $("#frmBrand").attr("method", "post");
        $("#frmBrand").submit();
    });

    $("#btnAddUser").click(function () {
        $("#frmBrand").attr("action", "/brand/add-brand-user/");
        $("#frmBrand").attr("method", "post");
        $("#frmBrand").submit();
    });

    $("#btnSearch").click(function () {
        Search_Brands();
    });

});

