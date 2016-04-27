$(function () {

    InitializeAutoComplete($('#txtPurchase_Order_No'));

    $('#hdfCurrentPage').val(0);

    Search_Purchase_Orders();

    $("#btnEdit").click(function () {

        $("#frmPurchaseOrder").attr("action", "/subcategory/edit-subcategories");

        $("#frmPurchaseOrder").attr("method", "POST");

        $("#frmPurchaseOrder").submit();
    });

    $("#btnSearch").click(function () {

        $('#hdfCurrentPage').val(0);

        Search_Purchase_Orders();

    });

});
