$(document).ready(function () {

    $("#frmPurchaseOrderMaster").validate({
        errorClass: 'login-error',
        rules: {
            "PurchaseOrderItem.Product_Price":
               {
                   required: true,
                   number:true
               },
            "PurchaseOrderItem.Product_Quantity":
                {
                    required: true,
                    digits:true,
                },
            "PurchaseOrderItem.Shipping_Address":
               {
                   required: true 
               }

        },
        messages: {

            "PurchaseOrderItem.Product_Price":
            {
                required: "Price is required.",
                number: "Only numbers"
            },
            "PurchaseOrderItem.Product_Quantity":
            {
                required: "Quantity is required.",
                digits: "Only numbers "
            },
            "PurchaseOrderItem.Shipping_Address":
               {
                   required: "Shipping address is required."
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
