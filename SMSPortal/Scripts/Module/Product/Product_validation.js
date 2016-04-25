﻿$(document).ready(function () {

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
                required: "Product Percentage Share is required"
                 
            },
            "Product.Brand_Id":
            {
                required: "Brand  is required" 
            },
            "Product.Product_Price":
            {
                required: "Price is required",
                number: "Numbers only"
            }
        },
    });
});
jQuery.validator.addMethod("validate_Product_Exist", function (value, element) {
    var result = true;

    if ($("#txtProduct_Name").val() != "" && $("#hdnProductName").val() != $("#txtProduct_Name").val()) {
        $.ajax({
            url: '/Product/Check_Existing_Product',
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