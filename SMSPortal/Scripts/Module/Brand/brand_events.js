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
        if ($("#frmBrandMaster").valid())
        {
            $("#frmBrandMaster").attr("action", "/brand/Insert_Update_Brand/");
            $("#frmBrandMaster").attr("method", "POST");
            $("#frmBrandMaster").submit();
        }

    });

});

 