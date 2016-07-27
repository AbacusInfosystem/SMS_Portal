$(function () {

    InitializeAutoComplete($('#txtVendorSales_Order_No'));

    $('#hdfCurrentPage').val(0);

    Search_Sales_Orders();



    $("#btnDetails").click(function () {

        $("#frmVendorSalesOrder").attr("action", "/vendor/view-vendor-sales_orders");

        $("#frmVendorSalesOrder").attr("method", "POST");

        $("#frmVendorSalesOrder").submit();

    });



    $("#btnSearch").click(function () {

        $('#hdfCurrentPage').val(0);

        Search_Sales_Orders();

    });

});
