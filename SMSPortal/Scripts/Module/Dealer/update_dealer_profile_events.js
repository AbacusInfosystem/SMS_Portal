
$(function () {

    $("#btnUpdate").click(function () {

        if ($('#frmUpdateDealerProfile').valid()) {

            $("#frmUpdateDealerProfile").attr("action", "/dealer/update-dealer-profile-details/");

            $("#frmUpdateDealerProfile").attr("method", "post");

            $("#frmUpdateDealerProfile").submit();
        }
    });

});