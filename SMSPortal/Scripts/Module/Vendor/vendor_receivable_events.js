$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmVendorReceivableMaster").attr("action", "/Vendor/VendorReceivables/");

        $("#frmVendorReceivableMaster").attr("method", "POST");

        $("#frmVendorReceivableMaster").submit();

    });

});