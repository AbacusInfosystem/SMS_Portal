$(function () {

    InitializeAutoComplete($('#txtUser_Name'));

    Search_Users();

    $("#btnEdit").click(function () {
        $("#frmUser").attr("action", "/User/edit-users");
        $("#frmUser").attr("method", "post");
        $("#frmUser").submit();
    });

    $("#btnSearch").click(function () {
        $('#hdfCurrentPage').val(0);
        Search_Users();

    });

});

$(document).ready(function () {

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});