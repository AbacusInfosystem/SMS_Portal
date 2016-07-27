$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmOrderDetails").attr("action", "/ThirdPartyVendor/SearchOrders/");

        $("#frmOrderDetails").attr("method", "POST");

        $("#frmOrderDetails").submit();

    });

});