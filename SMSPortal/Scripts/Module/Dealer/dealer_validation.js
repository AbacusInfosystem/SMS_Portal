$(document).ready(function () {

    $("#frmDealerMaster").validate({
         
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
            "Dealer.Dealer_Percentage":
                {
                    required: true,
                    number: true
                },
            "Dealer.Brand_Percentage":
                {
                    required: true,
                    number: true
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
                    digits: true
                },
            "Dealer.Contact_No_1":
                {
                    required: true,
                    digits: true
                },
            //"Dealer.Contact_No_2":
            //    {
            //        required: true,
            //        digits:true
            //    },
            "Dealer.Email":
                {
                    required: true,
                    email: true,
                    is_value_already_exist: true
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
            "Dealer.Dealer_Percentage":
            {
                required: "Dealer Percentage Share is required",
                number: "Enter only digits"
            },
            "Dealer.Brand_Percentage":
            {
                required: "Brand Percentage Share is required",
                number: "Enter only digits"
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
                digits: "Enter only digits"
            },
        "Dealer.Contact_No_1":
            {
                required: "Contact no 1 is required",
                digits: "Enter only digits"
            },
        //"Dealer.Contact_No_2":
        //    {
        //        required: "Contact no 2 required",
        //        digits: "Enter only digits"
        //    },
        "Dealer.Email":
            {
                required: "Email is required",
                email: "Invalide e-mail"
            }

        },
        onfocusout: function (element, event) {
            if ($(element).name == "Dealer.Dealer_Name") return;
        },
        onkeyup: function (element, event) {
            if ($(element).name == "Dealer.Dealer_Name") return;
        }
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

jQuery.validator.addMethod("is_value_already_exist", function (value, element) {
    var result = true;

    $.ajax({
        url: '/Common/Is_Value_Already_Exist/',
        data: { Tbl_Name: "Dealer", Fld_Name: "Email", Value: value, Primary_Field_Name: "Dealer_Id", Id: parseInt($("#hdf_DealerId").val()) },
        method: 'GET',
        async: false,
        success: function (data) {
            if (data == true) {
                result = false;
            }
        }
    });

    return result;

}, "This email-id already used by another dealer.");
