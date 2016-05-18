$(document).ready(function () {

    $("#frmSubCategory").validate({        
        rules: {

            "SubCategory.Subcategory_Name":
               {
                   required: true,
                   validate_SubCategory_Exist: true
               },
            "SubCategory.Category_Id":
               {
                   required: true
               }

        },
        messages: {

            "SubCategory.Subcategory_Name":
               {
                   required: "SubCategory Name is required."
               },
            "SubCategory.Category_Id":
               {
                   required: "Category is required."
               }

        },
        onfocusout: function (element, event) {
            if ($(element).name == "SubCategory.Subcategory_Name") return;
        },
        onkeyup: function (element, event) {
            if ($(element).name == "SubCategory.Subcategory_Name") return;
        }
    });
});

jQuery.validator.addMethod("validate_SubCategory_Exist", function (value, element) {
    var result = true;

    if ($("#txtSubCategory_Name").val() != "" && $("#hdnSubCategory_Name").val() != $("#txtSubCategory_Name").val()) {
        $.ajax({
            url: '/SubCategory/Check_Existing_Sub_Category',
            data: { subcategory: $("#txtSubCategory_Name").val() },
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

}, "Subcategory name already exists.");
