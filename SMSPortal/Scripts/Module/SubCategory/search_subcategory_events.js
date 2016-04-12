$(function () {

    $('#hdfCurrentPage').val(0);

    Search_Subcategory();

    $("#btnEdit").click(function () {

        $("#frmSearchRole").attr("action", "/user-management/role/by-id");

        $("#frmSearchRole").attr("method", "POST");

        $("#frmSearchRole").submit();
    });

    $("#btnSearch").click(function () {
        $('#hdfCurrentPage').val(0);

        Search_Subcategory();

    });

    $("#btnDelete").click(function () {

        $("#DeleteRecord").find(".modal-body").load();

    });

    $("#btnYes").click(function () {

        var roleid = $("#hdnRole_Id").val();

        $("#frmSearchRole").attr("action", "/user-management/delete-User-Role-by-id/" + roleid + "/");

        $("#frmSearchRole").attr("method", "POST");

        $("#frmSearchRole").submit();

    });

});