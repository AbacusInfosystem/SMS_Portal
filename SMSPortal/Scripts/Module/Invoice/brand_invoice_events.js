
$(function () {

    $("#txtInvoice_Date").datepicker({
        changeMonth: true,//this option for allowing user to select month
        changeYear: true //this option for allowing user to select from year range
    });


    $(".fa-chevron-left").click(function () {

        $("#frmBrandInvoice").attr("action", "/Receivable/Searches/");

        $("#frmBrandInvoice").attr("method", "POST");

        $("#frmBrandInvoice").submit();

    });
});