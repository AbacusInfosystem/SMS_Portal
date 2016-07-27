$(function () {

    //$('input').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    increaseArea: '20%', // optional
    //});

    $(".chkstatus").on("change", function () {

        if (!$(this).is(':checked')) {
            $("#hdnIs_Active").val(false);
        }
        else {
            $("#hdnIs_Active").val(true);
        }
    });

    $("#btnSave").click(function () {
        if ($('#frmBrandMaster').valid()) {
            if ($("#hdf_BrandId").val() == 0) {
                $("#frmBrandMaster").attr("action", "/brand/insert-brands/");
            }
            else {
                $("#frmBrandMaster").attr("action", "/brand/update-brands/");
            }
            $('#frmBrandMaster').attr("method", "POST");
            $('#frmBrandMaster').submit();
        }
    });

});

