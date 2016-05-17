
$(document).ready(function ()
{
    InitializeAutoComplete($('#txtBrandInvoice_Number'));

    $('#hdfCurrentPage').val(0);

    Search_Invoices();

    $("#btnDetails").click(function () {
        $("#frmInvoice").attr("action", "/invoice/get-invoice/");
        $("#frmInvoice").attr("method", "post");
        $("#frmInvoice").submit();
    });

    $("#btnSearch").click(function () {
        Search_Invoices();
    });

});