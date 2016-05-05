$(function () {

    $("#txtRecDate").datepicker(
        {
            autoclose: true,

            enddate: null,
    });

    if ($("#hdnReceivable_Id").val() != 0)
    {
        $("#dvInvoice").find(".autocomplete-text").trigger("focusout");
    }

    $(".fa-chevron-left").click(function () {

        $("form").validate().cancelSubmit = true;

        $("#frmReceivable").attr("action", "/Receivable/Searches/");

        $("#frmReceivable").attr("method", "POST");

        $("#frmReceivable").submit();

    });

    $("#drpTransaction").change(function ()
    {

        $("#dvMain").html('');

        if ($("#drpTransaction").val() == 1)
        {
            var divHTML = $("#divCheque").html();

            $("#dvMain").html(divHTML);

            $("#txtChequeDate").datepicker(
                {

                    autoclose: true,

                    enddate: null,
            });
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

    $("#btnYes").click(function ()
    {

        if ($("#frmReceivable").valid())
        {
            Save_Receivable_Data();
        }

    });




});