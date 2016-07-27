$(function () {

    $("#btnAdd").click(function ()
    {
       
        if ($("#frmPurchaseOrderMaster").valid())
        {
            Save_Purchase_Order_Items();
        }
    });

    $("#txtShippingDate").datepicker({
        autoclose: true,
        enddate: null,
    });

    if ($("#hdnPurchase_Order_Id").val() != 0) {
        $("#dvSubc").find(".autocomplete-text").trigger("focusout");
        $("#dvSubc").addClass('disabled-div ');        
    }
    
    $(".fa-chevron-left").click(function () {
        $('#frmPurchaseOrderMaster').validate().cancelSubmit = true;

        $("#frmPurchaseOrderMaster").attr("action", "/PurchaseOrder/Search/");

        $("#frmPurchaseOrderMaster").attr("method", "POST");

        $("#frmPurchaseOrderMaster").submit();
    });

    //$('input:not(.non-iCheck input:checkbox)').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    $("#btnSendEmail").click(function ()
    {
        $('#frmPurchaseOrderMaster').validate().cancelSubmit = true;

        $("#frmPurchaseOrderMaster").attr("action", "/purchaseorder/send-purchase-order-email/");

        $("#frmPurchaseOrderMaster").attr("method", "POST");

        $("#frmPurchaseOrderMaster").submit();
    });

});
