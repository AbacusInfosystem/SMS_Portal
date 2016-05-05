$(function ()
{
    //alert(1);

    InitializeAutoComplete($('#txtPurchase_Order_No'));

    Search_Payable();

    $("#btnSearch").click(function ()

    {
        Search_Payable();
    });

    $("#btnEdit").click(function ()

    {
        $("#hdnMode").val("Edit");

        $("#frmPayable").attr("action", "/Payable/edit-payable");

        $("#frmPayable").attr("method", "POST");

        $("#frmPayable").submit();

    });

});