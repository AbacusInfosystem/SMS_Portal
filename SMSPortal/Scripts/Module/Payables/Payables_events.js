$(function () {
    $("#txtPayDate").datepicker({
        autoclose: true,
        enddate: null,
    });

    $("#txtChequeDate").datepicker({
        autoclose: true,
        enddate: null,
    });

    //$("#datemask").inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy" });


    if ($("#hdnPayable_Id").val() != 0)
    {
        $("#dvPurchase_Order").find(".autocomplete-text").trigger("focusout");
    }

    $(".fa-chevron-left").click(function () {

        $("#frmPayableMaster").attr("action", "/Payables/Search/");

        $("#frmPayableMaster").attr("method", "POST");

        $("#frmPayableMaster").submit();

    });
    //$("#btnAdd").click(function () {

    //    AddReceivableDetailsData();

    //});

    $("#drpTransaction").change(function () {
        if ($("#drpTransaction").val() == 1) {
            $("#divCheque").show();
            $("#divNEFT").hide();
            $("#divCredit_Debit").hide();
        }
        else if ($("#drpTransaction").val() == 2) {
            $("#divCheque").hide();
            $("#divNEFT").show();
            $("#divCredit_Debit").hide();
        }
        else {
            $("#divCheque").hide();
            $("#divNEFT").hide();
            $("#divCredit_Debit").show();
        }

    });
    $("#btnNEFTSave").click(function () {

        Save_Payable_Data();

    });
    $("#btnSave1").click(function () {
        alert("click1")
        Save_Payable_Data();

    });
    $("#btnSave2").click(function () {
        alert("click2")
        Save_Payable_Data();

    });

});