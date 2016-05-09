$(document).ready(function () {

    $('#Categories').append().load("/Product/GetCategories");
    $('#ProductList').append().load("/Product/GetProductList");

    $("#btnViewCart").click(function () {
        $.cookie.json = true;
        var cart = $.cookie('cart');

        //alert(cart);

        if (cart != undefined) {

            $("#hdnProductIds").val(getProductIds(cart));

            //alert($("#hdnProductIds").val());

            //window.location.href = "/Product/PlaceOrder?ProductIds=" + $("#hdnProductIds").val();

            $("#frmProductIndex").attr("action", "/Product/PlaceOrder");

            $("#frmProductIndex").attr("method", "POST");

            $("#frmProductIndex").submit();
               
        }        

    });

});