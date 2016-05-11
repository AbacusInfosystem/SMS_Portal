function getProductIds(cart) {
    var ProductIds = "";
    for (var i = 0; i < cart.length; i++) {
        ProductIds = ProductIds + cart[i] + ",";
    }
    ProductIds = ProductIds.substring(0, ProductIds.length - 1);

    return ProductIds;
}

function checkEnter(e) {
    e = e || event;
    var txtArea = /textarea/i.test((e.target || e.srcElement).tagName);
    return txtArea || (e.keyCode || e.which || e.charCode || 0) !== 13;
}