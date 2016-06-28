$(function () {

    InitializeAutoComplete($('#txtDealerInvoice'));

    Search_Receivable();

    $("#btnSearch").click(function () {
        Search_Receivable();
    });

    $("#btnView").click(function () {
        $("#hdnMode").val("View");

        $("#frmReceivable").attr("action", "/receivable/edit-receivable");

        $("#frmReceivable").attr("method", "POST");

        $("#frmReceivable").submit();

    });

    $("#btnViewInvoice").click(function () {

        $("#frmReceivable").attr("action", "/receivable/show-invoice-details/" + $('#hdnInvoice_Id').val());

        $("#frmReceivable").attr("method", "POST");

        $("#frmReceivable").submit();

    });

    $(".fa-chevron-left").click(function () {

        $("form").validate().cancelSubmit = true;

        $("#frmdealerpayable").attr("action", "/Receivable/Searches/");

        $("#frmdealerpayable").attr("method", "POST");

        $("#frmdealerpayable").submit();

    });

});