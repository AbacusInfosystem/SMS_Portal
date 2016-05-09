function AddToCart(Product_Id) {
    $.cookie.json = true;

    var cart = $.cookie('cart');

    if (cart == undefined) 
    {
        // Initializing array
        cart = [Product_Id];
    }
    else 
    {
        // Adding element into array
        cart.push(Product_Id);
    }
    
    $.cookie('cart', cart, { expires: 2 });
    $("#btnAddToCart_" + Product_Id).attr("disabled", "disabled");
    $("#CartItemCount").html(cart.length);
}