$(document).ready(function () {

    $("#frmSubCategory").validate({
        errorClass: 'login-error',
        rules: {

            "SubCategory.Subcategory_Name":
               {
                   required: true
               },
            "SubCategory.Category_Id":
               {
                   required: true
               },
            "SubCategory.IsActive":
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
               },
            "SubCategory.IsActive":
               {
                   required: "Status is required."
               }
        },
    });
});
