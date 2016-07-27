$(function () {

    //$('input').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    increaseArea: '20%', // optional
    //});

    $(".chkstatus").on("change", function () {

        var Id = $(this).attr('id');

        if (!$(this).is(':checked')) {

            $("#Status" + Id).val(false);
        }
        else {
            $("#Status" + Id).val(true);
        }
    });

    $("#btnYes").click(function () {
        if ($('#frmConsolidatedOrder').valid()) {
            $("#frmConsolidatedOrder").attr("action", "/Consolidated/Update_Order_Status/");
            $('#frmConsolidatedOrder').attr("method", "POST");
            $('#frmConsolidatedOrder').submit();
        }
    });

});

