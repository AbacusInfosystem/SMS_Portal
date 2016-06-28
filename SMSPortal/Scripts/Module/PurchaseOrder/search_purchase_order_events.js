$(function () {

    InitializeAutoComplete($('#txtAdminPurchase_Order_No'));

    $('#hdfCurrentPage').val(0);

    Search_Purchase_Orders();

   

    $("#btnEdit").click(function () {

        $("#frmPurchaseOrder").attr("action", "/purchaseorder/edit-purchase-order");

        $("#frmPurchaseOrder").attr("method", "POST");

        $("#frmPurchaseOrder").submit();

    });



    $("#btnSearch").click(function () {

        $('#hdfCurrentPage').val(0);

        Search_Purchase_Orders();

    });

});
