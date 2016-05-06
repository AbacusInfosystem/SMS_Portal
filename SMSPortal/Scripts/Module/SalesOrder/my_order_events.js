$(function () {

    $('input').iCheck({
        checkboxClass: 'icheckbox_square-green',
        increaseArea: '20%', // optional
    });

    $(".fa-chevron-left").click(function () {

        $("form").validate().cancelSubmit = true;

        $("#frmOrderDetails").attr("action", "/Dealer/My_Orders/");

        $("#frmOrderDetails").attr("method", "POST");

        $("#frmOrderDetails").submit();

    });

});
