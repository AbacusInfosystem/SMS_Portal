$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmCategoryMaster").attr("action", "/Category/Search/");

        $("#frmCategoryMaster").attr("method", "POST");

        $("#frmCategoryMaster").submit();

    });
    
    $(document).ready(function () {

        $("#btnSave").click(function () {
           
            if ($("#hdf_CategoryId").val() != "" && $("#hdf_CategoryId").val() != null && $("#hdf_CategoryId").val() > 0)
            {
                
                $('#frmCategoryMaster').attr("action", "/Category/Update_Category");
            }
            else
            {
               
                //if (('#frmCategoryMaster').valid())
                //{
                    $('#frmCategoryMaster').attr("action", "/Category/Insert_Category");
                //}
            }

            //$('#frmCategoryMaster').attr("action", "/Category/Insert_Category");
            $('#frmCategoryMaster').attr("method", "POST");
            $('#frmCategoryMaster').submit();

        });
    });

   
});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});