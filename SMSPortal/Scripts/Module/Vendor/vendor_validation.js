$(document).ready(function () {

    $("#frmVendorMaster").validate({
        errorClass: 'login-error',
        rules: {
            "Vendor.Vendor_Name":
               {
                   required: true,
                   validate_Vendor_Exist: true
               },
            "Vendor.Address":
                {
                    required: true,
                },
            "Vendor.City":
                {
                    required: true,
                },
            "Vendor.State":
                {
                    required: true,
                },
            "Vendor.Pincode":
                {
                    required: true,
                },
            "Vendor.Contact_No_1":
                {
                    required: true,
                },
            "Vendor.Contact_No_2":
                {
                    required: true,
                },
            "Vendor.Email":
                {
                    required: true,
                    email: true
                }

        },
        messages: {

            "Vendor.Vendor_Name":
            {
                required: "Vendor Name is required."
            },
            "Vendor.Address":
                {
                    required: "Address is required",
                },
            "Vendor.City":
                {
                    required: "City is required",
                },
            "Vendor.State":
                {
                    required: "State is required",
                },
            "Vendor.Pincode":
                {
                    required: "Pincode is required",
                },
            "Vendor.Contact_No_1":
                {
                    required: "Contact no 1 is required",
                },
            "Vendor.Contact_No_2":
                {
                    required: "Contact no 2 required",
                },
            "Vendor.Email":
                {
                    required: "Email is required",
                    email: "Invalid e-mail"
                }

        },
    });
});
jQuery.validator.addMethod("validate_Vendor_Exist", function (value, element) {
    var result = true;

    if ($("#txtVendor_Name").val() != "" && $("#hdnVendorName").val() != $("#txtVendor_Name").val()) {
        $.ajax({
            url: '/Vendor/Check_Existing_Vendor',
            data: { Vendor_Name: $("#txtVendor_Name").val() },
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

}, "Vendor Name already exists.");
