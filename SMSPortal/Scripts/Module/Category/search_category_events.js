
$(document).ready(function ()
{
    Search_Categories();

    $("#btnEdit").click(function ()
    {
        $("#frmCategory").attr("action", "/Category/Get_Category_By_Id");
        $("#frmCategory").attr("method", "post");
        $("#frmCategory").submit();        
    });

    $("#btnSearch").click(function ()
    {
        Search_Categories();
    });

});

$(document).ready(function () {

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });   

});