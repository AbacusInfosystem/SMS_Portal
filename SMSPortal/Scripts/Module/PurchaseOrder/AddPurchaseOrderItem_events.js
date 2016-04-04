$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmPurchaseOrderItem").attr("action", "/PurchaseOrder/Search/");

        $("#frmPurchaseOrderItem").attr("method", "POST");

        $("#frmPurchaseOrderItem").submit();

    });

});