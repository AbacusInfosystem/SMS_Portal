$(function () {
    //alert(1);
    $(".fa-chevron-left").click(function () {

        $("#frmUserMaster").validate().cancelSubmit = true;
        if ($("#hdf_RoleId").val() == 1) {

            $("#frmUserMaster").attr("action", "/User/Search/");
        }

        else if ($("#hdf_RoleId").val() == 2) {
            $("#frmUserMaster").attr("action", "/Brand/Search/");
        }
        else if ($("#hdf_RoleId").val() == 3) {
            $("#frmUserMaster").attr("action", "/Dealer/Search/");
        }
        else  {
            $("#frmUserMaster").attr("action", "/Vendor/Search/");
        }

            
     
        $("#frmUserMaster").attr("method", "POST");

        $("#frmUserMaster").submit();

    });

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
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

        if ($('#frmUserMaster').valid()) {
            if ($("#hdf_UserId").val() == 0) {
                $("#frmUserMaster").attr("action", "/User/insert-users/");
            }
            else {
                $("#frmUserMaster").attr("action", "/User/update-users/");
            }
            $('#frmUserMaster').attr("method", "POST");
            $('#frmUserMaster').submit();
        }

    });
});



