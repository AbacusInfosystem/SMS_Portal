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

    $(".fa-chevron-left").click(function () {
        $("#frmAddProductMapping").validate().cancelSubmit = true;

        $("#frmAddProductMapping").attr("action", "/Vendor/Search/");

        $("#frmAddProductMapping").attr("method", "POST");

        $("#frmAddProductMapping").submit();

    });
});
 

