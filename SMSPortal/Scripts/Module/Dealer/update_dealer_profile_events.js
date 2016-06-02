
$(function () {

    $("#txtContactNo1").mask("(99) 99999-99999");
    $("#txtContactNo2").mask("(999) 999-9999");

    $("#btnUpdate").click(function () {

        if ($('#frmUpdateDealerProfile').valid()) {

            $("#frmUpdateDealerProfile").attr("action", "/dealer/update-dealer-profile-details/");

            $("#frmUpdateDealerProfile").attr("method", "post");

            $("#frmUpdateDealerProfile").submit();
        }
    });

});