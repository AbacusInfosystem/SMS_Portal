
$(function () {

    $("#txtInvoice_Date").datepicker({
        changeMonth: true,//this option for allowing user to select month
        changeYear: true //this option for allowing user to select from year range
    });


    $(".fa-chevron-left").click(function () {

        $("#frmBrandInvoice").attr("action", "/Invoice/Search_Brand_Invoice/");

        $("#frmBrandInvoice").attr("method", "POST");

        $("#frmBrandInvoice").submit();

    });
});