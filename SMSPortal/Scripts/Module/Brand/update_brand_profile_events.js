
$(function () {

    $("#btnUpdate").click(function () {

        if ($('#frmUpdateBrandProfile').valid()) {

            $("#frmUpdateBrandProfile").attr("action", "/brand/update-brand-profile-details/");

            $("#frmUpdateBrandProfile").attr("method", "post");

            $("#frmUpdateBrandProfile").submit();
        }
    });

});