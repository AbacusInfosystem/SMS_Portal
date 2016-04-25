$(document).ready(function () {

    $("#frmBrandMaster").validate({
        errorClass: 'login-error',
        rules: {
            "Brand.Brand_Name":
               {
                   required: true,
                   validate_Brand_Exist: true
               },
            "Brand.Brand_Category":
                {
                    required: true,
                }

        },
        messages: {

            "Brand.Brand_Name":
            {
                required: "Brand Name is required."
            },
            "Brand.Brand_Category":
            {
               required: "Brand Category is required."
            }

        },
    });
});
jQuery.validator.addMethod("validate_Brand_Exist", function (value, element) {
    var result = true;

    if ($("#txtBrand_Name").val() != "" && $("#hdnBrandName").val() != $("#txtBrand_Name").val()) {
        $.ajax({
            url: '/brand/Check_Existing_Brand/',
            data: { Brand_Name: $("#txtBrand_Name").val() },
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

}, "Brand Name already exists.");
