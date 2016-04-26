$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmUserMaster").validate().cancelSubmit = true;

        $("#frmUserMaster").attr("action", "/User/Search/");

        $("#frmUserMaster").attr("method", "POST");

        $("#frmUserMaster").submit();

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
    //$("#btnSave").click(function () {
    //    if ($("#hdf_UserId").val() != "" && $("#hdf_UserId").val() != null && $("#hdf_UserId").val() > 0) {

    //        $('#frmUserMaster').attr("action", "/User/Update_User");
    //    }
    //    else {

    //        //if (('#frmUserMaster').valid())
    //        //{
    //        $('#frmUserMaster').attr("action", "/User/Insert/");
    //        //}
    //    }


    //    $("#frmUserMaster").attr("method", "POST");

    //    $("#frmUserMaster").submit();

    //});

});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });
    $("#drpRole").change(function () {

        var Role_Id = $("#drpRole").val();
     
        $.ajax(
        {
            url: '/User/Get_Entity_By_Role',
            data: { Role_Id: Role_Id },
            method: 'GET',
            async: false,
            success: function (data) {
                if (data != null) {
                    Bind_Entity(data);
                }
            }
        });
    });

});


//$(document).ready(function () {

//    $("#txtLastlogin_Date").datepicker({
//        changeMonth: true,//this option for allowing user to select month
//        changeYear: true //this option for allowing user to select from year range
//    });
//});