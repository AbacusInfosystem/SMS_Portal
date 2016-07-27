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

        $("form").validate().cancelSubmit = true;

        $("#frmReceivableMaster").attr("action", "/Receivable/Search/");

        $("#frmReceivableMaster").attr("method", "POST");

        $("#frmReceivableMaster").submit();

    });

    $("#drpTransaction").change(function () {

        $("#dvMain").html('');

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

    $("#btnYes").click(function () {

        if ($("#frmReceivableMaster").valid()) {
            Save_Receivable_Data();
        }

    });




});