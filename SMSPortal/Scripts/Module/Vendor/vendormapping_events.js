
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
            $(this).val(true);        
        });

        alert(strStates);

        $("#hdfProductState").val(strStates.trim(","));

        $("#frmAddProductMapping").attr("action", "/Vendor/insert-vendor-product-mapping-details/");
        $('#frmAddProductMapping').attr("method", "POST");
        $('#frmAddProductMapping').submit();
    });

    
    
});
 

