$(function () {
    alert(1);
    InitializeAutoComplete($('#txtPurchase_Order_No'));

    Search_Payable();

    $("#btnSearch").click(function () {
        Search_Payable();
    });

    $("#btnEdit").click(function () {

        $("#frmPayables").attr("action", "/Payables/Get_Payables_By_Id");

        $("#frmPayables").attr("method", "POST");

        $("#frmPayables").submit();
    });

});