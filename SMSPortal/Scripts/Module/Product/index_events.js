$(document).ready(function () {

    document.querySelector('form').onkeypress = checkEnter;

    $('#Categories').append().load("/Product/GetCategories");
    $('#ProductList').append().load("/Product/GetProductList");

    $("#btnViewCart").click(function () {

        $.cookie.json = true;

        var id = "";

        var quantity = "";

        var cart = $.cookie('cart');

        if (cart != undefined) {

            for (var i = 0; i <= cart.length-1; i++)
            {
                id = id + cart[i].Product_Id + ",";

                quantity = quantity + cart[i].Quantity + ",";
            }
           

            $("#hdnProductIds").val(id);

            $("#hdnProductQuantities").val(quantity);

            $("#frmProductIndex").attr("action", "/Product/PlaceOrder");

            $("#frmProductIndex").attr("method", "POST");

            $("#frmProductIndex").submit();
               
        }        

    });

    $("#btnSearchProductByName").click(function () {
        $('#ProductList').append().load("/Product/GetProductList?Product_Name=" + $.trim($("#txtProductName").val()));
    });

});