$(function () {
    

    $("#drpBrand").change(function () {

        Get_Product_Image_Data();

    });

    $("#btnSave").click(function () {

        var strStates = "";
        $('.checkresult:checked').each(function () {
            $(this).val(true);        
        });



         

        $("#hdfProductState").val(strStates.trim(","));

        $("#frmAddProductMapping").attr("action", "/Vendor/insert-vendor-product-mapping-details/");
        $('#frmAddProductMapping').attr("method", "POST");
        $('#frmAddProductMapping').submit();
    });  
});
 

