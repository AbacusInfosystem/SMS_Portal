$(document).ready(function () {

    $("[id^='trCartItemDetails_']").each(function (i) {

        $("[name='order.OrderItems[" + i + "].Product_Quantity']").rules("add", "required");
        $("[name='order.OrderItems[" + i + "].Product_Quantity']").rules("add", "number");
        $("[name='order.OrderItems[" + i + "].Product_Quantity']").rules("add", { noDecimal: true });

        i++;
    });

});