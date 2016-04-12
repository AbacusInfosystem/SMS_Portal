$(document).ready(function () {

    $("#frmUserMaster").validate({
        errorClass: 'login-error',
        rules: {

            "User.User_Name":
               {
                   required: true
               }

        },
        messages: {

            "User.User_Name":
               {
                   required: "User Name is required."
               }
        },
    });
});
