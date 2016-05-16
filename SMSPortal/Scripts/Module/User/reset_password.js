$(function () {

$("#btnLogin").click(function () {
    $("#frmForgotPass").attr("action", "/Login/Send_Reset_Password");
    $("#frmForgotPass").attr("method", "post");
    $("#frmForgotPass").submit();
});

});