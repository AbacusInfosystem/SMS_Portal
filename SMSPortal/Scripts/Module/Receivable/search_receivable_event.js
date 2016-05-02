$(function () {

    InitializeAutoComplete($('#txtInvoice_No'));

    Search_Receivable();

    $("#btnSearch").click(function () {
        Search_Receivable(); 
    });

    $("#btnEdit").click(function () {

        $("#frmReceivable").attr("action", "/receivable/edit-receivable");

        $("#frmReceivable").attr("method", "POST");

        $("#frmReceivable").submit();
    });

});