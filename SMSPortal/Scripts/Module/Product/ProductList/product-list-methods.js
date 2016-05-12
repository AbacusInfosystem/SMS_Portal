function intialization() {
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
}

function AddToCart(obj, Product_Id, bIsCalledByPopup) {
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

    $("#btnAddToCart_" + Product_Id).attr("disabled", "disabled"); // Disabling product list "Add To Cart" button
    if (bIsCalledByPopup) {
        $(obj).attr("disabled", "disabled");    // Disabling product details (Popup) "Add To Cart" button
    }    
    $("#CartItemCount").html(cart.length);
}

function viewMore(productId) {

    $("#div_Parent_Modal_Fade").find(".modal-dialog").addClass("modal-lg");
    
    $('#div_Parent_Modal_Fade').find(".modal-body").load('/Product/GetProductDetails', { Product_Id: productId }, function () {
        $('#div_Parent_Modal_Fade').find(".modal-title").html("Product Details");
        $('#div_Parent_Modal_Fade').modal("show");
    });
}