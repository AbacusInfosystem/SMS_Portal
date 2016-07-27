$(function () {

    //$('input:not(.non-iCheck input:checkbox)').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    $(".fa-chevron-left").click(function () {
        $("#frmNewVendorMaster").validate().cancelSubmit = true;

        $("#frmNewVendorMaster").attr("action", "/Vendor/Search/");

        $("#frmNewVendorMaster").attr("method", "POST");

        $("#frmNewVendorMaster").submit();

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

        if ($('#frmNewVendorMaster').valid()) {
            if ($("#hdf_VendorId").val() == 0) {
                $("#frmNewVendorMaster").attr("action", "/newvendor/insert-vendors/");
            }
            else {
                $("#frmNewVendorMaster").attr("action", "/newvendor/update-vendors/");
            }
            $('#frmNewVendorMaster').attr("method", "POST");
            $('#frmNewVendorMaster').submit();
        }


    });

});
