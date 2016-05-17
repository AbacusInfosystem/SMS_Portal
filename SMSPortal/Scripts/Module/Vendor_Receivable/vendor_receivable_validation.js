$(document).ready(function ()
{
    

    $("#frmPayableMaster").validate(

        {

            errorClass: 'login-error',

            rules:

                {
                    "Payable.Payable_Date":

                        {
                            required: true,

                        },
                    "Payable.Transaction_Type":

                        {

                            required: true,

                            number_validation: true

                        },

                    "Payable.Payable_Item_Amount":

                        {

                            required: true,

                            check_balance_amount_validation: true
                        }
                },

            messages:
                {



                    "Payable.Payable_Date":

                        {

                            required: "Enter date.",


                        },

                    "Payable.Transaction_Type":

                        {

                            required: "select atleast one transaction"


                        },

                    "Payable.Payable_Item_Amount":

                        {

                            required: "Amount is required."

                        }
                },
        });
});


jQuery.validator.addMethod("check_balance_amount_validation", function (value, element) {
    var result = true;

    if ($("#txtPayable_Item_Amount").val() != "") {

        var Balance_Amount = $("#txtBalance_Amount").val();

        var Entered_Amount = $("#txtPayable_Item_Amount").val();

        if (Balance_Amount == "0")
        {
            Balance_Amount = $("#txtPurchase_Order_Amount").val();
        }

        if (Entered_Amount > Balance_Amount)
        {
            result = false;
        }

    }

    return result;

}, "Please enter amount less than balance amount.");

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
