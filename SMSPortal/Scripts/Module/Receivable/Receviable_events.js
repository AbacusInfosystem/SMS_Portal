$(function () {

    $("#txtRecDate").datepicker({
        autoclose: true,
        enddate: null,
    });

    $("#txtChequeDate").datepicker({
        autoclose: true,
        enddate: null,
    });

    if ($("#hdnReceivable_Id").val() != 0)
    {
        $("#dvInvoice").find(".autocomplete-text").trigger("focusout");
    }

    $(".fa-chevron-left").click(function () {

        $("#frmReceivableMaster").attr("action", "/Receivable/Search/");

        $("#frmReceivableMaster").attr("method", "POST");

        $("#frmReceivableMaster").submit();

    });

    $("#btnAdd").click(function () {

        AddReceivableDetailsData();

    });

    $("#drpTransaction").change(function () {

        $("#dvMain").html('');

        if ($("#drpTransaction").val() == 1)
        {
            var divHTML = $("#divCheque").html();
            $("#dvMain").html(divHTML);
        }
        else if ($("#drpTransaction").val() == 2)
        {
            var divHTML = $("#divNEFT").html();
            $("#dvMain").html(divHTML);
        }
        else
        {
            var divHTML = $("#divCredit_Debit").html();
            $("#dvMain").html(divHTML);
        }

    });

    $("#btnNEFTSave").click(function () {

        Save_Receivable_Data();

    });




});