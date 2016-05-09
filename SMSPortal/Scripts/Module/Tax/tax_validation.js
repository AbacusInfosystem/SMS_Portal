$(document).ready(function () {

    $("#frmTax").validate({
        errorClass: 'login-error',
        rules: {

            "Tax.Local_Tax":
               {
                   required: true,
                   number:true
               },
            "Tax.Export_Tax":
               {
                   required: true,
                   number: true
               }

        },
        messages: {

            "Tax.Local_Tax":
               {
                   required: "Local tax is required",
                   number: "only number allowed"
               },
            "Tax.Export_Tax":
                {
                    required: "Export Tax is required",
                    number: "only number allowed"
                }
        },
    });
});

