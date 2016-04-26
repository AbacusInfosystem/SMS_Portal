$(function () {

    InitializeAutoComplete($('#txtInvoice_No'));

    Search_Receivable();

    $("#btnSearch").click(function () {
        Search_Receivable(); 
    });

});