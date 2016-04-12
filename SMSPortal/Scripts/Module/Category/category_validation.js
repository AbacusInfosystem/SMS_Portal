$(document).ready(function () {

    $("#frmCategoryMaster").validate({
        errorClass: 'login-error',
        rules: {

            "Category.Category_Name":
               {
                   required: true
               }

        },
        messages: {

            "Category.Category_Name":
               {
                   required: "Category Name is required."
               }
        },
    });
});
