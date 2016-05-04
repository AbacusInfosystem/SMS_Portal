$(function () {

    $('input').iCheck({
        checkboxClass: 'icheckbox_square-green',
        increaseArea: '20%', // optional
    });

    $(".fa-chevron-left").click(function () {

        $("form").validate().cancelSubmit = true;

        $("#frmSubCategory").attr("action", "/SubCategory/Search/");

        $("#frmSubCategory").attr("method", "POST");

        $("#frmSubCategory").submit();

    });

    $("#btnSave").click(function () {

        $("#frmOrderDetails").attr("action", "/salesorder/update-salesorder-status/");

        $("#frmOrderDetails").attr("method", "POST");

        $("#frmOrderDetails").submit();

    });


});
