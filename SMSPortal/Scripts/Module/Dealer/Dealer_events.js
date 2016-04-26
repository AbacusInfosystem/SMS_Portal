$(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    $(".fa-chevron-left").click(function () {

        $("#frmDealerMaster").validate().cancelSubmit = true;
        $("#frmDealerMaster").attr("action", "/Dealer/Search/");
        $("#frmDealerMaster").attr("method", "POST");
        $("#frmDealerMaster").submit();

    });

    $(".chkstatus").on("ifChanged", function () {

        if ($(this).parents().prop("class").indexOf("checked") != -1) {
            $("#hdnIs_Active").val(false);
        }
        else {
            $("#hdnIs_Active").val(true);
        }

    });

    $("#btnSave").click(function () {

        if ($('#frmDealerMaster').valid()) {
            if ($("#hdf_DealerId").val() == 0) {
                $("#frmDealerMaster").attr("action", "/dealer/insert-dealer/");
            }
            else {
                $("#frmDealerMaster").attr("action", "/dealer/update-dealer/");
            }
            $('#frmDealerMaster').attr("method", "POST");
            $('#frmDealerMaster').submit();
        }
    });

});
