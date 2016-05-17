$(document).ready(function () {

    $("#frmCategoryMaster").validate({
        
        rules: {

            "Category.Category_Name":
               {
                   required: true,
                   validate_Category_Exist: true
               }

        },
        messages: {

            "Category.Category_Name":
               {
                   required: "Category Name is required."
               }
        },
        onfocusout: function (element, event) {
            if ($(element).name == "Category.Category_Name") return;
        },
        onkeyup: function (element, event) {
            if ($(element).name == "Category.Category_Name") return;
        }
    });
});
jQuery.validator.addMethod("validate_Category_Exist", function (value, element) {
    var result = true;

    if ($("#txtCategory_Name").val() != "" && $("#hdnCategoryName").val() != $("#txtCategory_Name").val()) {
        $.ajax({
            url: '/Category/Check_Existing_Category',
            data: { Category_Name: $("#txtCategory_Name").val() },
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

}, "Category Name already exists.");
