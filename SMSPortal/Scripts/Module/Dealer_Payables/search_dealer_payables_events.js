$(function () {

    InitializeAutoComplete($('#txtInvoice_No'));

    Search_Receivable();

    $("#btnSearch").click(function ()

    {
        Search_Receivable();
    });

    $("#btnView").click(function ()

    {
        $("#hdnMode").val("View");

        $("#frmReceivable").attr("action", "/receivable/edit-receivable");

        $("#frmReceivable").attr("method", "POST");

        $("#frmReceivable").submit();

    });

});