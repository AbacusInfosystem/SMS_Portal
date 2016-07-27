$(function ()
{
    //$('input:not(.non-iCheck input:checkbox)').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    $(".chkstatus").on("change", function () {

        if (!$(this).is(':checked')) {
            $("#hdnIs_Active").val(false);
        }
        else {
            $("#hdnIs_Active").val(true);
        }
    });

    $(".fa-chevron-left").click(function () {

        $("#frmCategoryMaster").validate().cancelSubmit = true;

        $("#frmCategoryMaster").attr("action", "/Category/Search/");

        $("#frmCategoryMaster").attr("method", "POST");

        $("#frmCategoryMaster").submit();

    });

    $("#btnSave").click(function () {

        if ($('#frmCategoryMaster').valid()) {
            if ($("#hdf_CategoryId").val() == 0) {
                $("#frmCategoryMaster").attr("action", "/category/insert-category");
            }
            else {
                $("#frmCategoryMaster").attr("action", "/category/update-category/");
            }
            $('#frmCategoryMaster').attr("method", "POST");
            $('#frmCategoryMaster').submit();
        }
    });
});
