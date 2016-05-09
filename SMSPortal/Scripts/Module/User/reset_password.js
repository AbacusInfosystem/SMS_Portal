//$(function () {

    
//    $("#btnLogin").click(function ()
//    {
//        if ($('#frmForgotPassword').valid())
//        {
//            //if ($("#hdfUserId").val() == 0)
//            //{
//            //    $("#frmForgotPassword").attr("action", "/login/Send_Reset_Password/");
//            //}
//            $("#frmForgotPassword").attr("action", "/login/Send_Reset_Password/");
//            $('#frmForgotPassword').attr("method", "POST");
//            $('#frmForgotPassword').submit();
//        }

//    });
//});

$("#btnLogin").click(function () {
    $("#frmForgotPassword").attr("action", "/Login/Send_Reset_Password");
    $("#frmForgotPassword").attr("method", "post");
    $("#frmForgotPassword").submit();
});

