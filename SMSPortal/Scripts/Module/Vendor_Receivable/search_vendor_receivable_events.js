$(function ()
{
    //alert(1);

    InitializeAutoComplete($('#txtPurchase_Order_No'));

    Search_Payable();

    $("#btnSearch").click(function ()
    {
        Search_Payable();
    });

    $("#btnView").click(function () {

        $("#hdnMode").val("View");

        $("#frmPayable").attr("action", "/Payable/edit-payable");

        $("#frmPayable").attr("method", "POST");

        $("#frmPayable").submit();

    });

    $(".fa-chevron-left").click(function () {

        $("form").validate().cancelSubmit = true;

        $("#frmvendorreceivable").attr("action", "/Payables/Searches/");

        $("#frmvendorreceivable").attr("method", "POST");

        $("#frmvendorreceivable").submit();

    });

});