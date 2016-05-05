
$(function () {

    $("#btnEdit").click(function () {

        $("#frmBrandProfileMaster").attr("action", "/brand/edit-brand-profile-details/");

        $("#frmBrandProfileMaster").attr("method", "post");

        $("#frmBrandProfileMaster").submit();
    });

});