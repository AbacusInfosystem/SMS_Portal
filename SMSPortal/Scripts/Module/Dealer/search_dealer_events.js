
$(document).ready(function ()
{
    InitializeAutoComplete($('#txtDealer_Name'));

    $('#hdfCurrentPage').val(0);

    Search_Dealers();

    $("#btnEdit").click(function () {
        $("#frmDealer").attr("action", "/dealer/edit-dealer/");
        $("#frmDealer").attr("method", "post");
        $("#frmDealer").submit();
    });

    $("#btnAddUser").click(function () {
        $("#frmDealer").attr("action", "/dealer/add-dealer-user/");
        $("#frmDealer").attr("method", "post");
        $("#frmDealer").submit();
    });


    $("#btnSearch").click(function () {
        Search_Dealers();
    });

});