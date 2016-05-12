$(document).ready(function () {

    $("#frmAddProductMapping").validate(
        {
            errorClass: 'login-error',

            rules:
                {
                //    "products.product_price":
                //{
                //    required: true,
                //    validate_product_price: true
                //}
          },

          
            messages:
                {

                    //"product.product_price":
                    //{
                    //    required: "price is required",
                    //    number: "numbers only"
                    //}
                },

        });

    //$("[name^=Product[0].Product_Price]").each(function () {
    //    $(this).rules("add", {
    //        required: true,
    //        validate_product_price: true
    //    });
    //});

   
});

//jQuery.validator.addMethod("validate_product_price", function (value, element) {

//    alert("hello");

//    var result = true;

//        if (isNaN($("#txtProduct_Price").val()) || $("#txtProduct_Price").val() <= 0)
//        {
//            return false;
//        }
//        else if ($("#hdn_MasterProductPrice").val() > $("#txtProduct_Price").val()) 
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }

//    return result;

//}, "Entered Product Price is Invalid.");




jQuery.validator.addMethod("validate_product_price", function (value, element)

{
    alert(element.id);

    var MasterProductPrice = parseInt($(element).closest("td").find("#hdn_MasterProductPrice").val());

    //alert($(element).val());

    //alert($(element).closest("td").find("#hdn_MasterProductPrice").val());

    //if ($(element).val() != "" )
    //{
    //    return false;
    //}

    if ($(element).val() < MasterProductPrice)
    {
        return true;
    }

    else
    {
        return false;

   }

    //return result;

}, "Invalid Product Price");


