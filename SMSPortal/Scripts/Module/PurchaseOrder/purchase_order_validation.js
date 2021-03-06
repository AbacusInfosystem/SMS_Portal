﻿$(document).ready(function () {

    var validator=$("#frmPurchaseOrderMaster").validate({
       
        ignore: [],
        rules: {
            "PurchaseOrderItem.Product_Price":
               {
                   required: true,
                   number: true
               },
            "PurchaseOrderItem.Product_Quantity":
                {
                    required: true,
                    digits: true 
                },
            "PurchaseOrderItem.Shipping_Address":
               {
                   required: true
               },
            "PurchaseOrder.Vendor_Id":
             {
                 required: true
             },
            "Product.Product_Id":
                {
                    required: true
                    //,
                    //validate_Product_Exist:true
                },
            "PurchaseOrderItem.Shipping_Date":
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
               },
            "PurchaseOrder.Vendor_Id":
             {
                 required: "Vendor is required."
             },
            "Product.Product_Id":
            {
                required: "Product is required"
            },
            "PurchaseOrderItem.Shipping_Date":
                {
                    required: "Shipping date is required"
                }
            
        }
        //,
        //errorPlacement: function (error, element)
        //{
        //    if (element.attr("name") == "PurchaseOrder.Vendor_Id") {
        //        error.insertAfter("#vendor_look");
        //    }
        //    if (element.attr("name") == "Product.Product_Id") {
        //        error.insertAfter("#product_look");
        //    }
        //}
    });
});
jQuery.validator.addMethod("validate_Product_Exist", function (value, element) {
    var result = true;
    var purchase_order_id = $('#hdnPurchase_Order_Id').val();
    var product_id = $('#hdnProductId').val();     
    if ($("#hdnProductId").val() != "") {
        $.ajax({
            url: '/purchaseorder/check-duplicate-products-purchase-order/',
            data: { Product_Id: product_id, Purchase_Order_Id: purchase_order_id },
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

}, "Product already added in purchase order.");
