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
        if ($("#drpTransaction").val() == 1)
        {
            $("#divCheque").show();
            $("#divNEFT").hide();
            $("#divCredit_Debit").hide();
        }
        else if ($("#drpTransaction").val() == 2)
        {
            $("#divCheque").hide();
            $("#divNEFT").show();
            $("#divCredit_Debit").hide();
        }
        else
        {
            $("#divCheque").hide();
            $("#divNEFT").hide();
            $("#divCredit_Debit").show();
        }

    });

    $("#btnNEFTSave").click(function () {

        Save_Receivable_Data();

    });

});