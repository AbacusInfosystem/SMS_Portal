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

        $('#hdnOrderSlot').val("");
        $('#hdfCurrentPage').val(0);

        Search_Sales_Order();

    });

    $('#drpOrderStatus').change(function () {

        $('#hdnStatus').val($('#drpOrderStatus').val());
    });

    $("#btnFirstSlot").click(function ()
    {
        $('#drpOrderStatus').val("");
        $('#hdnStatus').val("");
        $('#hdnOrderSlot').val("FirstSlot");         
        Search_Sales_Order();

    });
    $("#btnSecondSlot").click(function () {
        $('#drpOrderStatus').val("");
        $('#hdnStatus').val("");
        $('#hdnOrderSlot').val("SecondSlot");

        Search_Sales_Order();
    });



});



 