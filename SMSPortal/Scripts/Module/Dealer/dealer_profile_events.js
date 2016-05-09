
$(function () {

    $("#btnEdit").click(function () {

        $("#frmDealerProfileMaster").attr("action", "/dealer/edit-dealer-profile-details/");

        $("#frmDealerProfileMaster").attr("method", "post");

        $("#frmDealerProfileMaster").submit();
    });

});