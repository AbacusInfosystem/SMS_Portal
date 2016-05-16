$(document).ready(function () {

    $("#frmBrandMaster").validate({
        errorClass: 'login-error',
        rules: {
            "Brand.Brand_Name":
               {
                   required: true,
                   validate_Brand_Exist: true
               },
            "Brand.Brand_Category":
                {
                    required: true,
                },
            "Brand.Website_Url":
                {
                    required: true,
                    Website_Url: true
                }

        },
        messages: {

            "Brand.Brand_Name":
            {
                required: "Brand Name is required."
            },
            "Brand.Brand_Category":
            {
                required: "Brand Category is required."
            },
            "Brand.Website_Url":
                {
                    required: "Website Url is required",
                    Website_Url: "Invalid url",
                }
        },
        onfocusout: function (element, event) {
            if ($(element).name == "Brand.Brand_Name") return;
        },
        onkeyup: function (element, event) {
            if ($(element).name == "Brand.Brand_Name") return;
        }
    });
});
jQuery.validator.addMethod("validate_Brand_Exist", function (value, element) {
    var result = true;

    if ($("#txtBrand_Name").val() != "" && $("#hdnBrandName").val() != $("#txtBrand_Name").val()) {
        $.ajax({
            url: '/brand/Check_Existing_Brand/',
            data: { Brand_Name: $("#txtBrand_Name").val() },
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

}, "Brand Name already exists.");
jQuery.validator.addMethod("Website_Url", function (val, elem) {
    // if no url, don't do anything
    if (val.length == 0) { return true; }

    // if user has not entered http:// https:// or ftp:// assume they mean http://
    if (!/^(https?|ftp):\/\//i.test(val)) {
        val = 'http://' + val; // set both the value
        $(elem).val(val); // also update the form element
    }
    // now check if valid url

    return /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(val);
});