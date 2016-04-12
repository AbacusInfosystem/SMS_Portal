$(function () {

    $(".icheck").iCheck({

        checkboxClass: 'icheckbox_square-green',

        increaseArea: '20%' // optional
    });

    $(".icheck").on("ifChanged", function () {

        if ($(this).parents().prop("class").indexOf("checked") != -1) {
            $(this).val(false);
        }
        else {
            $(this).val(true);
        }

    });

    $(".fa-chevron-left").click(function () {

        $("#frmSubCategory").attr("action", "/SubCategory/Search/");

        $("#frmSubCategory").attr("method", "POST");

        $("#frmSubCategory").submit();

    });

    $("#btnSave").click(function () {

        if ($("#frmSubCategory").valid()) {

            $("#frmSubCategory").attr("action", "/subcategory/insert-update-subcategories/");

            $("#frmSubCategory").attr("method", "POST");

            $("#frmSubCategory").submit();
        }

    });

});
