$(document).ready(function () {

    $("#frmReceivableMaster").validate({
        errorClass: 'login-error',
        rules: {

        //    "SubCategory.Subcategory_Name":
        //       {
        //           required: true,
        //           validate_SubCategory_Exist: true
        //       }

        //},
        //messages: {

        //    "SubCategory.Subcategory_Name":
        //       {
        //           required: "SubCategory Name is required."
        //       }

        },
    });
});

jQuery.validator.addMethod("check_balance_amount_validation", function (value, element) {
    var result = true;

    if ($("#txtReceivable_Item_Amount").val() != "") {
       
        var Balance_Amount = $("#txtBalance_Amount").val();
        var Entered_Amount = $("#txtReceivable_Item_Amount").val();
        if(Entered_Amount>Balance_Amount)
        {
            result = false;
        }

    }
    return result;

}, "Please enter amount less than balance amount.");
