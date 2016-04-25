$(function () {

    $('input').iCheck({
        checkboxClass: 'icheckbox_square-green',
        increaseArea: '20%', // optional
    });

    $(".chkstatus").on("ifChanged", function () {

        if ($(this).parents().prop("class").indexOf("checked") != -1)
        {
            $("#hdnIs_Active").val(false);
        }
        else {
            $("#hdnIs_Active").val(true);
        }

    });


    $(".fa-chevron-left").click(function ()
    {
        $("#frmBrandMaster").validate().cancelSubmit = true;
        $("#frmBrandMaster").attr("action", "/Brand/Search/");
        $("#frmBrandMaster").attr("method", "POST");
        $("#frmBrandMaster").submit();

    });

    $("#btnSave").click(function ()
    {
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

 