$(function () {

    //$('input:not(.non-iCheck input:checkbox)').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    $(".fa-chevron-left").click(function () {
        $("#frmVendorMaster").validate().cancelSubmit = true;

        $("#frmVendorMaster").attr("action", "/ThirdPartyVendor/Search/");

        $("#frmVendorMaster").attr("method", "POST");

        $("#frmVendorMaster").submit();

    });


    $(".chkstatus").on("change", function () {

        if (!$(this).is(':checked')) {
            $("#hdnIs_Active").val(false);
        }
        else {
            $("#hdnIs_Active").val(true);
        }
    });
  
    $("#btnSave").click(function () {

        if ($('#frmVendorMaster').valid()) {
            if ($("#hdf_VendorId").val() == 0) {
                $("#frmVendorMaster").attr("action", "/Vendor/insert-vendors/");
            }
            else {
                $("#frmVendorMaster").attr("action", "/Vendor/update-vendors/");
            }
            $('#frmVendorMaster').attr("method", "POST");
            $('#frmVendorMaster').submit();
        }


    });

});
