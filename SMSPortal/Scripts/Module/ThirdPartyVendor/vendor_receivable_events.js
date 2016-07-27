$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmVendorReceivableMaster").attr("action", "/Third_Party_Vendor/VendorReceivables/");

        $("#frmVendorReceivableMaster").attr("method", "POST");

        $("#frmVendorReceivableMaster").submit();

    });

});