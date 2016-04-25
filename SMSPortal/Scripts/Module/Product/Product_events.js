$(function () {

    $('input').iCheck({
        checkboxClass: 'icheckbox_square-green',
        increaseArea: '20%', // optional
    });

    $(".chkstatus").on("ifChanged", function () {

        if ($(this).parents().prop("class").indexOf("checked") != -1)
        {
            $("#hdnIs_Active").val(false);
            $("#hdnIs_Biddable").val(false);             
        }
        else
        {
            $("#hdnIs_Active").val(true);
            $("#hdnIs_Biddable").val(true);
        }

    });

    $(".fa-chevron-left").click(function ()
    {
        $("#frmProductMaster").validate().cancelSubmit = true;

        $("#frmProductMaster").attr("action", "/Product/Search/");
        $("#frmProductMaster").attr("method", "POST");
        $("#frmProductMaster").submit();

    });

    $("#btnSave").click(function () {
        if ($('#frmProductMaster').valid())
        {
            if ($("#hdf_ProductId").val() == 0)
            {
                $("#frmProductMaster").attr("action", "/Product/Insert_Product/");
            }
            else
            {
                $("#frmProductMaster").attr("action", "/Product/Update_Product/");                
            }
            $('#frmProductMaster').attr("method", "POST");
            $('#frmProductMaster').submit();

            
        }
    });            
    
    $("#drpProduct_Category").change(function () 
    {
        var Category_Id = $("#drpProduct_Category").val();         
        $.ajax(
        {
            url: '/Product/Get_SubCategory_By_Category_Id',
            data: { Category_Id: Category_Id },
            method: 'GET',
            async: false,
            success: function (data) {
                
                if (data != null) {
                    Bind_SubCategories(data);
                }
            }
        });
    });

    if ($("#drpProduct_Category").val() != null)
    {         
        $("#drpProduct_Category").trigger("change");               
        $("#drpProduct_SubCategory").val($("#hdnSubCategory_Id").val());
    }
    

});

 
 
 