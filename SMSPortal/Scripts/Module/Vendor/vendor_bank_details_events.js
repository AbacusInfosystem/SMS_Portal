
$(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    $(".chkstatus").on("ifChanged", function () {
   
        if ($(this).parents().prop("class").indexOf("checked") != -1) {
            $("#hdnIsActive").val(false);
        }
        else {
            $("#hdnIsActive").val(true);
        }

    });

    $("#btnAdd").click(function () {

        AddBankDetailsData();

    });

    $("#btnSave").click(function () {

        $("#frmBankDetails").attr("action", "/vendor/add-vendor-bank-details/");

        $("#frmBankDetails").attr("method", "POST");

        $("#frmBankDetails").submit();

    });

});