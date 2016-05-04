$(function () {

    InitializeAutoComplete($('#txtOrderNo'));

    $('#hdfCurrentPage').val(0);

    Search_Sales_Order();

    $("#btnOrderDetails").click(function () {

        $("#frmSalesOrder").attr("action", "/salesorder/edit-salesorder");

        $("#frmSalesOrder").attr("method", "POST");

        $("#frmSalesOrder").submit();
    });

    $("#btnSearch").click(function () {

        $('#hdfCurrentPage').val(0);

        Search_Sales_Order();

    });


});



 