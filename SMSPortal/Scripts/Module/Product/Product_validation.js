$(document).ready(function () {

    $("#frmProductMaster").validate({
        errorClass: 'login-error',
        rules: {
            "Product.Product_Name":
               {
                   required: true,
                   validate_Product_Exist: true
               },
            "Product.Category_Id":
                {
                    required: true,
                },
            "Product.SubCategory_Id":
                {
                    required: true
                },
            "Product.Brand_Id":
                {
                    required: true
                },
            "Product.Local_Tax":
               {
                   number: true
               },
            "Product.Export_Tax":
             {
                 number: true
             },
            "Product.Product_Price":
                {
                    required: true,
                    number:true
                }
        },
        messages: {

            "Product.Product_Name":
            {
                required: "Product Name is required."
            },
            "Product.Category_Id":
            {
                required: "Category is required."
            },
            "Product.SubCategory_Id":
            {
                required: "SubCategory is required"

            },
            "Product.Brand_Id":
            {
                required: "Brand  is required"
            },
            "Product.Local_Tax":
          {
              number: "Numbers only"
          },
            "Product.Export_Tax":
             {
                 number: "Numbers only"
             },
            "Product.Product_Price":
            {
                required: "Price is required",
                number: "Numbers only"
            }
        },
        onfocusout: function (element, event) {
            if ($(element).name == "Product.Product_Name") return;
        },
        onkeyup: function (element, event) {
            if ($(element).name == "Product.Product_Name") return;
        }
    });
});

jQuery.validator.addMethod("validate_Product_Exist", function (value, element) {
    var result = true;

    if ($("#txtProduct_Name").val() != "" && $("#hdnProductName").val() != $("#txtProduct_Name").val()) {
        $.ajax({
            url: '/product/check-existing-product',
            data: { Product_Name: $("#txtProduct_Name").val() },
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

}, "Product Name already exists.");


