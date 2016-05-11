function intialization() {
    $.cookie.json = true;
    var cart = $.cookie('cart');
    if (cart != undefined) {
        for (var i = 0; i < cart.length; i++) {
            if (parseInt($("#hdnPD_ProductId").val()) == cart[i])
                $("#btnPD_AddToCart").attr("disabled", "disabled");
        }
    }
}

function changeImage(obj) {
    $("#imgCurrentProductImage").attr("src", $(obj).attr("src"));
}