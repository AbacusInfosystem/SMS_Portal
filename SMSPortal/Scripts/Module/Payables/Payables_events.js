$(function () {
    $("#txtPayDate").datepicker({
        autoclose: true,
        startDate: '-6m',
        enddate: null,
    });

    $("#datemask").inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy" });

    $(".fa-chevron-left").click(function () {

        $("#frmPayableMaster").attr("action", "/Payables/Search/");

        $("#frmPayableMaster").attr("method", "POST");

        $("#frmPayableMaster").submit();

    });
    $("#btnAdd").click(function () {

        AddReceivableDetailsData();

    });

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
    $("#btnSave").click(function () {
        alert(1);
        Save_Payable_Data();

    });
    $("#btnSave1").click(function () {
        alert(1);
        Save_Payable_Data();

    });
    $("#btnSave2").click(function () {
        alert(1);
        Save_Payable_Data();

    });

});