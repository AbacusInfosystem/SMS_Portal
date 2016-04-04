$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmVendorMaster").attr("action", "/Vendor/Search/");

        $("#frmVendorMaster").attr("method", "POST");

        $("#frmVendorMaster").submit();

    });

});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});