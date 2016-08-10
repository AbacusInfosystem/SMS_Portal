function intialization() {
    $.cookie.json = true;
    var cart = $.cookie('cart');

    if (cart != undefined) {

        for (var i = 0; i <= cart.length - 1; i++) {
            $("#btnAddToCart_" + cart[i].Product_Id).attr("disabled", "disabled");

            var str = cart[i].Quantity;
            var quantity = str.substring(str.lastIndexOf("_") + 1, str.length);
            $("#hdnQuantity_" + cart[i].Product_Id).val(quantity);
            $("#hdnQuantity_" + cart[i].Product_Id).attr("disabled", "disabled");
        }
        $("#CartItemCount").html(cart.length);
    }
    else {

        $("#CartItemCount").html("0");
    }
}

function AddToCart(obj, Product_Id, bIsCalledByPopup) {


    $("[id^='hdnQuantity_" + Product_Id + "']").rules("add", "required");

    if ($("#frmProductIndex").valid())
    {
    $.cookie.json = true;
    var cart = $.cookie('cart');

    var produc_name = $("#hdnpName_" + Product_Id).val();

    var produc_quantity = "";

    if (bIsCalledByPopup==false)
    {
        produc_quantity = $("#hdnQuantity_" + Product_Id).val();
    }
    else
    {
        produc_quantity = $("#hdnQuantity").val();
    }
    
    
    if (cart == undefined) {
        cart = [{ 'Quantity': produc_name.trim() + "_" + produc_quantity, 'Product_Id': Product_Id }, ];
    }
    else {
        cart.push(
            { 'Quantity': produc_name.trim() + "_" + produc_quantity, 'Product_Id': Product_Id }
        );
    }

    $.cookie('cart', cart, { expires: 2 });

    $("#btnAddToCart_" + Product_Id).attr("disabled", "disabled"); // Disabling product list "Add To Cart" button
    $("#hdnQuantity_" + Product_Id).val(produc_quantity);
    $("#hdnQuantity_" + Product_Id).attr("disabled", "disabled");

    if (bIsCalledByPopup) {
        $(obj).attr("disabled", "disabled");    // Disabling product details (Popup) "Add To Cart" button
    }    
    $("#CartItemCount").html(cart.length);
    }


}

function viewMore(productId) {

    $("#hdnProductId").val(productId);

    $("#frmProductIndex").attr("action", "/Product/DisplayProductDetails/");
    $('#frmProductIndex').attr("method", "POST");
    $('#frmProductIndex').submit();

    //$("#div_Parent_Modal_Fade").find(".modal-dialog").addClass("modal-lg");
    //$('#div_Parent_Modal_Fade').find(".modal-body").load('/Product/GetProductDetails', { Product_Id: productId }, function () {
    //    $('#div_Parent_Modal_Fade').find(".modal-title").html("Product Details");
    //    $('#div_Parent_Modal_Fade').modal("show");
    //});
}