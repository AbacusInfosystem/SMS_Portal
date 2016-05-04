
$(document).ready(function ()
{
    InitializeAutoComplete($('#txtDealer_Name'));

    $('#hdfCurrentPage').val(0);

    Search_Dealers();

    $("#btnEdit").click(function ()

    {
        $("#hdnMode").val("Edit");

        $("#frmDealer").attr("action", "/dealer/edit-dealer/");

        $("#frmDealer").attr("method", "post");

        $("#frmDealer").submit();

    });

    $("#btnView").click(function ()

    {

        $("#hdnMode").val("View");

        $("#frmDealer").attr("action", "/dealer/edit-dealer/");

        $("#frmDealer").attr("method", "POST");

        $("#frmDealer").submit();

    });

    $("#btnSearch").click(function ()
    {

        Search_Dealers();

    });

});