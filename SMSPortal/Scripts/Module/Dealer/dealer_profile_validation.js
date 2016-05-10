$(document).ready(function () {

    $("#frmUpdateDealerProfile").validate({
        errorClass: 'login-error',
        rules: {
            "Dealer.Dealer_Name":
               {
                   required: true,
                   validate_Dealer_Exist: true
               },
            "Dealer.Brand_Id":
                {
                    required: true,
                },
            "Dealer.Dealer_Percentage_Share":
                {
                    required: true,
                    digits:true
                },
            "Dealer.Brand_Percentage_Share":
                {
                    required: true,
                    digits: true
                },
            "Dealer.Address":
                {
                    required: true,
                },
            "Dealer.City":
                {
                    required: true,
                },
            "Dealer.State":
                {
                    required: true,
                },
            "Dealer.Pincode":
                {
                    required: true,
                    digits : true
                },
            "Dealer.Contact_No_1":
                {
                    required: true,
                    digits : true
                },
            "Dealer.Contact_No_2":
                {
                    required: true,
                    digits : true
                },
            "Dealer.Email":
                {
                    required: true,
                    email:true
                }

        },
        messages: {

            "Dealer.Dealer_Name":
            {
                required: "Dealer Name is required."
            },
            "Dealer.Brand_Id":
            {
                required: "Brand is required."
            },
            "Dealer.Dealer_Percentage_Share":
            {
                required: "Dealer Percentage Share is required",
                digits: "Enter only digits"
            },
        "Dealer.Brand_Percentage_Share":
            {
                required: "Brand Percentage Share is required",
                digits: "Enter only digits"
            },
        "Dealer.Address":
            {
                required: "Address is required",
            },
        "Dealer.City":
            {
                required: "City is required",
            },
        "Dealer.State":
            {
                required: "State is required",
            },
        "Dealer.Pincode":
            {
                required: "Pincode is required",
                digits : "Enter digits"
            },
        "Dealer.Contact_No_1":
            {
                required: "Contact no 1 is required",
                digits : "Enter digits"
            },
        "Dealer.Contact_No_2":
            {
                required: "Contact no 2 required",
                digits : "Enter digits"
            },
        "Dealer.Email":
            {
                required: "Email is required",
                email: "Invalide e-mail"
            }

        },
    });
});
jQuery.validator.addMethod("validate_Dealer_Exist", function (value, element) {
    var result = true;

    if ($("#txtDealer_Name").val() != "" && $("#hdnDealerName").val() != $("#txtDealer_Name").val()) {
        $.ajax({
            url: '/dealer/Check_Existing_Dealer/',
            data: { Dealer_Name: $("#txtDealer_Name").val() },
            method: 'GET',
            async: false,
            success: function (data) {
                if (data == true) {
                    result = false;
                }
            }
        });
    }
    return result;

}, "Dealer Name already exists.");
