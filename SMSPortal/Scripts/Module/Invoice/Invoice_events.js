
$(document).ready(function () {

    $("#txtInvoice_Date").datepicker({
        changeMonth: true,//this option for allowing user to select month
        changeYear: true //this option for allowing user to select from year range
    });
});
$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmInvoiceMaster").attr("action", "/Invoice/Search/");

        $("#frmInvoiceMaster").attr("method", "POST");

        $("#frmInvoiceMaster").submit();

    });

});