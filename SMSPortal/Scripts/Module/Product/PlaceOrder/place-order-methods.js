function changeProductPrice(rowIndex) {

    var newProductPrice = calculateProductPrice(rowIndex);
    $("#spanProduct_Price_" + rowIndex).html($.number(newProductPrice, 2));
    $("[name='order.OrderItems[" + rowIndex + "].Product_Price']").val(newProductPrice);
    changeGrossTaxNetAmount();
}

function calculateProductPrice(rowIndex) {
    var quantity = $("[name='order.OrderItems[" + rowIndex + "].Product_Quantity']").val();
    var singleProductPrice = $("#hdnSingle_Product_Price_" + rowIndex).val();
    var newProductPrice = 0;

    if (!isNaN(quantity)) {
        if (quantity > 0) {
            newProductPrice = quantity * singleProductPrice;
        }
    }

    return newProductPrice;
}

function changeGrossTaxNetAmount() {
    var amountPayable = 0, tax = 0, netAmount = 0;
    var noOfProducts = $("[id^='trCartItemDetails_']").size();
    for (var i = 0; i < noOfProducts; i++) {
        amountPayable += calculateProductPrice(i);
    }

    if ($("#hdnStateName").val() == "MAHARASHTRA") {
        tax = (amountPayable * $("#hdnLocalTax").val()) / 100;
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
    changeGrossTaxNetAmount();
}

function removeFromCookie(rowIndex) {
    var productId = $("[name='order.OrderItems[" + rowIndex + "].Product_Id']").val();
    $.cookie.json = true;
    var cart = $.cookie('cart');
    if (cart != undefined) {
        var index = cart.indexOf(parseInt(productId));
        if (index > -1) {
            cart.splice(index, 1);
            $.cookie('cart', cart, { expires: 2 });
        }
    }
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

    //alert(noOfProducts);

    if (noOfProducts > 0) {

        if ($("#frmPlaceOrder").valid()) {

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