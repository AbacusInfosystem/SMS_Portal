$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmSubCategory").attr("action", "/SubCategory/Search/");

        $("#frmSubCategory").attr("method", "POST");

        $("#frmSubCategory").submit();

    });

});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});