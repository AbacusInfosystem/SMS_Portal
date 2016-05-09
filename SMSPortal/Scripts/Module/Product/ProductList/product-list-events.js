$(document).ready(function () {
    $.cookie.json = true;

    var cart = $.cookie('cart');
    if (cart != undefined) {

        $("#CartItemCount").html(cart.length);

        for (var i = 0; i < cart.length; i++) {
            $("#btnAddToCart_" + cart[i]).attr("disabled", "disabled");
        }
    }
    else {
        $("#CartItemCount").html("0");
    }
});