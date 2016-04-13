$(document).ready(function () {
    Search_Users();

    $("#btnEdit").click(function () {
        $("#frmUser").attr("action", "/User/Get_User_By_Id");
        $("#frmUser").attr("method", "post");
        $("#frmUser").submit();
    });

    $("#btnSearch").click(function () {
        Search_Users();
    });

});

$(document).ready(function () {

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});