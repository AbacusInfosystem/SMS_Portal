$(function () {

    $('[name="User"]').on('ifChanged', function (event) {

        if ($(this).prop('checked')) {
            $("#hdnUser_Id").val(this.id.replace("rdo_", ""));
            $("#btnEdit").show();
        }
    });

    $("#btnEdit").click(function () {

        $("#frmUser").attr("action", "/User/Get_User_By_Id");
        $("#frmUser").attr("method", "POST");
        $("#frmUser").submit();

    });
});