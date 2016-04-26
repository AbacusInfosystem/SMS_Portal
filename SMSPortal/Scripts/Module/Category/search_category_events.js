
$(document).ready(function ()
{
    InitializeAutoComplete($('#txtCategory_Name'));

    $('#hdfCurrentPage').val(0);

    Search_Categories();

    $("#btnEdit").click(function ()
    {
        $("#frmCategory").attr("action", "/category/edit-category/");
        $("#frmCategory").attr("method", "post");
        $("#frmCategory").submit();        
    });

    $("#btnSearch").click(function ()
    {
        Search_Categories();
    });

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});

 