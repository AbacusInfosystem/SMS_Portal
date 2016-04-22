
$(function () {
    

    $("#drpBrand").change(function () {

        Get_Product_Image_Data();

    });

    $("#btnSave").click(function () {

        //if ($('#frmVendorMaster').valid()) {
        //    if ($("#hdf_VendorId").val() == 0) {
        //        $("#frmVendorMaster").attr("action", "/Vendor/Insert_Vendor/");
        //    }
        //    else {
        //        $("#frmVendorMaster").attr("action", "/Vendor/Update_Vendor/");
        //    }
        var strStates = "";
        $('.checkresult:checked').each(function () {
            alert (1);
            strStates += $(this).val() + ",";        
        });

        $("#hdfProductState").val(strStates.trim(","));
        $("#frmAddProductMapping").attr("action", "/Vendor/Insert_Product/");
        $('#frmAddProductMapping').attr("method", "POST");
        $('#frmAddProductMapping').submit();
    });

    
    
});
 

