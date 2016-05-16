$(function () {

    InitializeAutoComplete($('#txtDealerOrderNo'));

    $('#hdfCurrentPage').val(0);

    Search_Sales_Order();

    $("#btnDetails").click(function () {

        $("#frmMyOrders").attr("action", "/salesorder/display-dealer-salesorder");

        $("#frmMyOrders").attr("method", "POST");

        $("#frmMyOrders").submit();
    });

    $("#btnSearch").click(function () {

        $('#hdfCurrentPage').val(0);

        Search_Sales_Order();

    });


});



