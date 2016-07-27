
$(function () {

    //$('input:not(.non-iCheck input:checkbox)').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    $(".chkstatus").on("change", function () {
   
        if (!$(this).is(':checked')) {
            $("#hdnIsActive").val(false);
            alert($("#hdnIsActive").val());
        }
        else {
            $("#hdnIsActive").val(true);
            alert($("#hdnIsActive").val());
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