$(function () {
    //alert(1);
    InitializeAutoComplete($('#txtPurchase_Order_No'));

    Search_Payable();

    $("#btnSearch").click(function () {
        Search_Payable();
    });

    $("#btnEdit").click(function () {

        $("#frmPayables").attr("action", "/Payable/edit-payable");

        $("#frmPayables").attr("method", "POST");

        $("#frmPayables").submit();
    });

});