$(function () {

    
    $("#btnLogin").click(function ()
    {
        if ($('#frmForgotPassword').valid())
        {
            if ($("#hdfUserId").val() == 0)
            {
                $("#frmForgotPassword").attr("action", "/login/Update_Password/");
            }             
            $('#frmForgotPassword').attr("method", "POST");
            $('#frmForgotPassword').submit();
        }

    });
});



