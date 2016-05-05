
$(function () {

    $("#btnUpdate").click(function () {

        if ($('#frmUpdateVendorProfile').valid()) {

            $("#frmUpdateVendorProfile").attr("action", "/vendor/update-vendor-profile-details/");

            $("#frmUpdateVendorProfile").attr("method", "post");

            $("#frmUpdateVendorProfile").submit();
        }
    });

});