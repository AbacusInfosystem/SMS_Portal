$(document).ready(function () {

    $("#frmPayableMaster").validate({
        errorClass: 'login-error',
        rules: {
          
            "Payable.Purchase_Order_Amount":
                {
                    required: true,
                    digits : true,
                },
            "Payable.Transaction_Type":
                {
                    required: true,
                }
        },
        messages: {

        
            "Payable.Purchase_Order_Amount":
            {
                required: "Enter Amount.",
                digits: "Enter only digits"
            },
            "Dealer.Transaction_Type":
            {
                required: "select atleast one transaction"
              
            }
        },
    });
});
