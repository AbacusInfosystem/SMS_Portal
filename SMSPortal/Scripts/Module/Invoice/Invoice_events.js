
$(function () {

    $("#txtInvoice_Date").datepicker({
        changeMonth: true,//this option for allowing user to select month
        changeYear: true //this option for allowing user to select from year range
    });


    $(".fa-chevron-left").click(function () {

        if ($("#hdf_Role_Id").val() == 1)
        {
            $("#frmCreateInvoice").attr("action", "/Invoice/Search/");
        }
        if ($("#hdf_Role_Id").val() == 3)
        {
            $("#frmCreateInvoice").attr("action", "/Receivable/Searches/");
        }
        else
        {
            $("#frmCreateInvoice").attr("action", "/Invoice/Search/");
        }

        $("#frmCreateInvoice").attr("method", "POST");

        $("#frmCreateInvoice").submit();

    });

    $("#btnSendEmail").click(function () {

        $("#frmCreateInvoice").attr("action", "/Invoice/Send_Mail/");

        $("#frmCreateInvoice").attr("method", "POST");

        $("#frmCreateInvoice").submit();

    });

});