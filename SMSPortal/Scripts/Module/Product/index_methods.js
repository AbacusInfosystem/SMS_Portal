function getProductIds(cart) {
    var ProductIds = "";
    for (var i = 0; i < cart.length; i++) {
        ProductIds = ProductIds + cart[i] + ",";
    }
    ProductIds = ProductIds.substring(0, ProductIds.length - 1);

    return ProductIds;
}