$(function () {

    $(".fa-chevron-left").click(function () {
        
        $("#frmOrderDetails").attr("action", "/SalesOrder/Search/");

        $("#frmOrderDetails").attr("method", "POST");

        $("#frmOrderDetails").submit();

    });
});

$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});


 