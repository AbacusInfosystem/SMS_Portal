$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmProductMaster").attr("action", "/Product/Search/");

        $("#frmProductMaster").attr("method", "POST");

        $("#frmProductMaster").submit();

    });

});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});