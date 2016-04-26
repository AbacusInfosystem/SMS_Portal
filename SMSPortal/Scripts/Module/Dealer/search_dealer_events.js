
$(document).ready(function ()
{
    Search_Dealers();

    $("#btnEdit").click(function () {
        $("#frmDealer").attr("action", "/dealer/edit-dealer/");
        $("#frmDealer").attr("method", "post");
        $("#frmDealer").submit();
    });

    $("#btnSearch").click(function () {
        Search_Dealers();
    });

});