$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmOrderDetails").attr("action", "/Vendor/SearchOrders/");

        $("#frmOrderDetails").attr("method", "POST");

        $("#frmOrderDetails").submit();

    });

});