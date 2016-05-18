$(document).ready(function () {

    $("#frmReceivableMaster").validate(
        {
        rules: {

            "Receivable.Receivable_Date":

               {
                   required: true
               },

            "Receivable.Transaction_Type":

               {
                   required: true,
                   number_validation: true
               },

            "Receivable.Receivable_Item_Amount":

               {
                   required: true,
                   check_balance_amount_validation: true
               }

        },

        messages:
            {

                "Receivable.Receivable_Date":

               {
                   required: "Receivable date is required."
               },

                "Receivable.Transaction_Type":

               {
                   required: "Transaction type is required."
               },

            "Receivable.Receivable_Item_Amount":
              {
                  required: "Amount is required."
              }

        },
    });
});

jQuery.validator.addMethod("check_balance_amount_validation", function (value, element)
{
    var result = true;

    if ($("#txtReceivable_Item_Amount").val() != "")
    {

        var Balance_Amount = $("#txtBalance_Amount").val();

        var Entered_Amount = $("#txtReceivable_Item_Amount").val();

        if (Balance_Amount == "0")
        {
            Balance_Amount = $("#txtInvoice_Amount").val();
        }

        if (Entered_Amount > Balance_Amount)
        {
            result = false;
        }

    }

    return result;

}, "Amount should not be greater than balance amount.");

jQuery.validator.addMethod("number_validation", function (value, element)
{
    var result = true;

    if ($("#drpTransaction").val() != "")
    {

        if ($("#drpTransaction").val() == "0")
        {
            result = false;
        }

    }
    return result;

}, "Transaction type is required..");
