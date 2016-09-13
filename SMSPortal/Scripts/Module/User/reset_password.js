$(function () {

    $("#btnLogin").click(function () {
        if ($('#frmForgotPass').valid()) {

            $("#frmForgotPass").attr("action", "/Login/Send_Reset_Password");
            $("#frmForgotPass").attr("method", "post");
            $("#frmForgotPass").submit();
        }

});

});