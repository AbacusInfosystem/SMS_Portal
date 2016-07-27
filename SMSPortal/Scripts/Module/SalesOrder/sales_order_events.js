$(function () {

    //$('input').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    increaseArea: '20%', // optional
    //});

    $(".fa-chevron-left").click(function () {

        $("form").validate().cancelSubmit = true;

        $("#frmOrderDetails").attr("action", "/SalesOrder/Search/");

        $("#frmOrderDetails").attr("method", "POST");

        $("#frmOrderDetails").submit();

    });

    $("#btnSave").click(function () {

        $("#frmOrderDetails").attr("action", "/salesorder/update-salesorder-status/");

        $("#frmOrderDetails").attr("method", "POST");

        $("#frmOrderDetails").submit();

    });


});
