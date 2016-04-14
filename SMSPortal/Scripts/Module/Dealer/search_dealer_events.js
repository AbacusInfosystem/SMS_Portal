
$(document).ready(function ()
{
    Search_Dealers();

    $("#btnEdit").click(function () {
        $("#frmDealer").attr("action", "/Dealer/Get_Dealer_By_Id/");
        $("#frmDealer").attr("method", "post");
        $("#frmDealer").submit();
    });

    $("#btnSearch").click(function () {
        Search_Dealers();
    });

});