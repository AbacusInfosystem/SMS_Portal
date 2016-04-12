$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmUserMaster").attr("action", "/User/Search/");

        $("#frmUserMaster").attr("method", "POST");

        $("#frmUserMaster").submit();

    });
    $("#btnSave").click(function () {
        if ($("#hdf_UserId").val() != "" && $("#hdf_UserId").val() != null && $("#hdf_UserId").val() > 0) {

            $('#frmUserMaster').attr("action", "/User/Update_User");
        }
        else {

            //if (('#frmUserMaster').valid())
            //{
            $('#frmUserMaster').attr("action", "/User/Insert/");
            //}
        }


        $("#frmUserMaster").attr("method", "POST");

        $("#frmUserMaster").submit();

    });

});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});

//$(document).ready(function () {

//    $("#txtLastlogin_Date").datepicker({
//        changeMonth: true,//this option for allowing user to select month
//        changeYear: true //this option for allowing user to select from year range
//    });
//});