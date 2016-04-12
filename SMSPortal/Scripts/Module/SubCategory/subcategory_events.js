$(function () {

    $('input').iCheck({
        checkboxClass: 'icheckbox_square-green',
        increaseArea: '20%', // optional
    });

    $(".chkstatus").on("ifChanged", function () {

        if ($(this).parents().prop("class").indexOf("checked") != -1) {
            $("#hdnIsActive").val(false);
        }
        else {
            $("#hdnIsActive").val(true);
        }

    });

    $(".fa-chevron-left").click(function () {

        $("form").validate().cancelSubmit = true;

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
