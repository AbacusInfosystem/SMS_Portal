$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmBrandMaster").attr("action", "/Brand/Search/");

        $("#frmBrandMaster").attr("method", "POST");

        $("#frmBrandMaster").submit();

    });

});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});