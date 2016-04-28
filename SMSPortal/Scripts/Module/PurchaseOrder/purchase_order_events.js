$(function () {

    $(".fa-chevron-left").click(function ()
    {
        $('#frmPurchaseOrderMaster').validate().cancelSubmit = true;

        $("#frmPurchaseOrderMaster").attr("action", "/PurchaseOrder/Search/");

        $("#frmPurchaseOrderMaster").attr("method", "POST");

        $("#frmPurchaseOrderMaster").submit();
    });

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });
});
