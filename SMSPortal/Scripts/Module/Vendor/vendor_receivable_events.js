$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmvendorReceivable").attr("action", "/Vendor/VendorReceivables/");

        $("#frmvendorReceivable").attr("method", "POST");

        $("#frmvendorReceivable").submit();

    });

});