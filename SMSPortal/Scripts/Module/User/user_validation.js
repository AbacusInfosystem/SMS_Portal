$(document).ready(function () {

    $("#frmUserMaster").validate({
        errorClass: 'login-error',
        rules: {

            "User.User_Name":
               {
                   required: true,
                   validate_User_Exist: true
               },
            "User.First_Name":
                {
                    required: true
                },
            "User.Last_Name":
                {
                    required: true
                },
            "User.Entity_Id":
                {
                    required: true
                },
            "User.Gender":
                {
                    required: true
                },
            "User.Contact_No_1":
                {
                    required: true,
                    digits:true

                },
            "User.Contact_No_2":
              {
                  required: true,
                  digits : true

              },
            "User.Email_Id":
                {
                    required: true,
                    email: true,
                    is_value_already_exist: true
                }
        },
        messages: {

            "User.User_Name":
               {
                   required: "User Name is required."
               },
            "User.First_Name":
              {
                  required: "First Name is required."
              },
            "User.Last_Name":
              {
                  required: "Last Name is required."
              },
            "User.Role_Id":
              {
                  required: "Role is required."
              },
            "User.Entity_Id":
              {
                  required: "Entity is required."
              },
            "User.Gender":
              {
                  required: "Gender is required."
              },
            "User.Contact_No_1":
              {
                  required: "Enter Contact.",
                  digits : "Enter digits"
              },
            "User.Contact_No_2":
                {
                    required: "Enter contact",
                    digits: "Enter digits"
                },       
            "User.Email_Id":
             {
                 required: "Enter Email.",
                 email: "Invalid Email"
             }
        },
    });
});

jQuery.validator.addMethod("validate_User_Exist", function (value, element) {
    var result = true;
    if ($("#txtUser_Name").val() != "" && $("#hdnUserName").val() != $("#txtUser_Name").val()) {
        $.ajax({
            url: '/User/Check_Existing_User',
            data: { User_Name: $("#txtUser_Name").val() },
            method: 'GET',
            async: false,
            success: function (data) {
                if (data == true) {
                    result = false;
                }
            }
        });
    }
    return result;

}, "User Name already exists.");

jQuery.validator.addMethod("is_value_already_exist", function (value, element) {
    var result = true;
    $.ajax({
        url: '/Common/Is_Value_Already_Exist/',
        data: { Tbl_Name: "Users", Fld_Name: "Email_Id", Value: value },
        method: 'GET',
        async: false,
        success: function (data) {
            if (data == true) {
                result = false;
            }
        }
    });

    return result;

}, "This email-id already used by another user.");