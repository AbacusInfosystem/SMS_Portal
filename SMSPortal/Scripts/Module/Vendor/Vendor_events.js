﻿$(function () {

    $(".fa-chevron-left").click(function () {
        $("#frmVendorMaster").validate().cancelSubmit = true;

        $("#frmVendorMaster").attr("action", "/Vendor/Search/");

        $("#frmVendorMaster").attr("method", "POST");

        $("#frmVendorMaster").submit();

    });
    $(".chkstatus").on("ifChanged", function () {

        if ($(this).parents().prop("class").indexOf("checked") != -1) {
            $("#hdnIs_Active").val(false);
        }
        else {
            $("#hdnIs_Active").val(true);
        }

    });
    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });
    $("#btnSave").click(function () {

        if ($('#frmVendorMaster').valid()) {
            if ($("#hdf_VendorId").val() == 0) {
                $("#frmVendorMaster").attr("action", "/Vendor/Insert_Vendor/");
            }
            else {
                $("#frmVendorMaster").attr("action", "/Vendor/Update_Vendor/");
            }
            $('#frmVendorMaster').attr("method", "POST");
            $('#frmVendorMaster').submit();
        }


    });

});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});