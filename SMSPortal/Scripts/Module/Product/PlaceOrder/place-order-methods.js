function changeProductPrice(rowIndex) {

    var newProductPrice = calculateProductPrice(rowIndex);
    $("#spanProduct_Price_" + rowIndex).html($.number(newProductPrice, 2));
    $("[name='order.OrderItems[" + rowIndex + "].Product_Price']").val(newProductPrice);
    changeGrossTaxNetAmount(rowIndex);
}

function calculateProductPrice(rowIndex) {
    var quantity = $("[name='order.OrderItems[" + rowIndex + "].Product_Quantity']").val();
    var singleProductPrice = $("#hdnSingle_Product_Price_" + rowIndex).val();

   // singleProductPrice = singleProductPrice / quantity;

    var newProductPrice = 0;

    if (!isNaN(quantity)) {
        if (quantity > 0) {
            newProductPrice = quantity * singleProductPrice;
        }
    }

    return newProductPrice;
}

function changeGrossTaxNetAmount(rowIndex) {

    var quantity = $("[name='order.OrderItems[" + rowIndex + "].Product_Quantity']").val();

    var amountPayable = 0, tax = 0, netAmount = 0;
    var noOfProducts = $("[id^='trCartItemDetails_']").size();
    for (var i = 0; i < noOfProducts; i++) {
        amountPayable += calculateProductPrice(i);
    }

    if ($("#hdnStateName").val() == "MAHARASHTRA") {
        tax = (amountPayable * $("#hdnTax_" + rowIndex).val()) / 100;
    }
    else
    {
        tax = (amountPayable * $("#hdnTax_" + rowIndex).val()) / 100;
    }

    netAmount = amountPayable + tax;

    $("#spanAmountPayable").html($.number(amountPayable, 2));
    $("[name='order.Gross_Amount']").val(amountPayable);

    $("#spanTaxes").html($.number(tax, 2));
    $("[name='order.Service_Tax']").val(tax);

    $("#spanNetPayableAmount").html($.number(netAmount, 2));
    $("[name='order.Net_Amount']").val(netAmount);
}

function deleteCartItem(rowIndex) {

    removeFromCookie(rowIndex);
    $("#trCartItemDetails_" + rowIndex).remove();
    changeElementsId();

    var rowCount = $('#tblCart tr').length;
    for (var i = 1; i <= rowCount-4; i++) {
        changeGrossTaxNetAmount(i-1);
    }

}

function removeFromCookie(rowIndex) {

    var productId = $("[name='order.OrderItems[" + rowIndex + "].Product_Id']").val();

    $.cookie.json = true;

    var cart = $.cookie('cart');

    var productid = productId;

    var current_objs = $.cookie("cart");

    for (var i = 0; i <= current_objs.length - 1; i++) {

        if (current_objs[i].Product_Id == productid)
        {
            current_objs.splice(i, 1);

            $.cookie('cart', current_objs, { expires: 2 });
        }

    }

    for (var i = 0; i <= current_objs.length - 1; i++) {

        $("#btnAddToCart_" + current_objs[i].Product_Id).attr("disabled", "disabled");

    }

   

    $("#CartItemCount").html(current_objs.length);

    if(current_objs.length==0)
    {
        $("#spanAmountPayable").html($.number(0, 2));
        $("[name='order.Gross_Amount']").val(0);

        $("#spanTaxes").html($.number(0, 2));
        $("[name='order.Service_Tax']").val(0);

        $("#spanNetPayableAmount").html($.number(0, 2));
        $("[name='order.Net_Amount']").val(0);
    }
    //if (cart != undefined) {
    //    var index = cart.indexOf(parseInt(productId));
    //    if (index > -1) {
    //        cart.splice(index, 1);
    //        $.cookie('cart', cart, { expires: 2 });
    //    }
    //}
}

function changeElementsId() {
    $("[id^='trCartItemDetails_']").each(function (i) {
        currentRowIndex = $(this).attr("id").replace("trCartItemDetails_", "");

        $("#trCartItemDetails_" + currentRowIndex).attr("id", "trCartItemDetails_" + i);

        $("[name='order.OrderItems[" + currentRowIndex + "].Product_Id']").attr("name", "order.OrderItems[" + i + "].Product_Id");

        $("[name='order.OrderItems[" + currentRowIndex + "].Product_Quantity']").attr("onchange", "changeProductPrice(" + i + ")");
        $("[name='order.OrderItems[" + currentRowIndex + "].Product_Quantity']").attr("name", "order.OrderItems[" + i + "].Product_Quantity");

        $("#spanProduct_Price_" + currentRowIndex).attr("id", "spanProduct_Price_" + i);
        $("[name='order.OrderItems[" + currentRowIndex + "].Product_Price']").attr("name", "order.OrderItems[" + i + "].Product_Price");
        $("#hdnSingle_Product_Price_" + currentRowIndex).attr("id", "hdnSingle_Product_Price_" + i);

        $("#btnDelete_Cart_Item_" + currentRowIndex).attr("onclick", "deleteCartItem(" + i + ")");
        $("#btnDelete_Cart_Item_" + currentRowIndex).attr("id", "btnDelete_Cart_Item_" + i);

        i++;
    });
}

function placeOrder() {

    var noOfProducts = $("[id^='trCartItemDetails_']").size();

    if (noOfProducts > 0) {

        if ($("#frmPlaceOrder").valid()) {

            $.removeCookie('cart', { path: '/' });

            $("#frmPlaceOrder").attr("action", "/Product/SaveOrder");

            $("#frmPlaceOrder").attr("method", "POST");

            $("#frmPlaceOrder").submit();
        }

    }
}

function continueShopping() {

    $("#frmPlaceOrder").attr("action", "/Dashboard/Index");

    $("#frmPlaceOrder").attr("method", "POST");

    $("#frmPlaceOrder").submit();
}