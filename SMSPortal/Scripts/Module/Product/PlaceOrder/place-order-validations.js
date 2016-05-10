$(function () {
    $("#frmPlaceOrder").validate({

    });

    $.validator.addMethod("noDecimal", function (value, element) {
        return !(value % 1);
    }, "No decimal numbers.");
});