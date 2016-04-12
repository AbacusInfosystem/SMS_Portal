﻿$(function ()
{
    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    $(".iCheck").on("ifChanged", function ()
    {
        if ($(this).parents().prop("class").indexOf("checked") != -1)
        {
            $(this).val(false);          
        }
        else
        {
            $(this).val(true);            
        }
    });

    $('[name="chkStatus"]').on('ifChanged', function (event)
    {
        if ($(this).prop('checked'))
        {             
            $("#hdnIs_Active").val(true);
            
        }
        else
        {
            $("#hdnIs_Active").val(false);
           
        }
    });

    $(".fa-chevron-left").click(function () {

        $("#frmCategoryMaster").validate().cancelSubmit = true;

        $("#frmCategoryMaster").attr("action", "/Category/Search/");

        $("#frmCategoryMaster").attr("method", "POST");

        $("#frmCategoryMaster").submit();

    });

    $("#btnSave").click(function () {

        if ($('#frmCategoryMaster').valid()) {
            if ($("#hdf_CategoryId").val() == 0) {
                $("#frmCategoryMaster").attr("action", "/Category/Insert_Category/");
            }
            else {
                $("#frmCategoryMaster").attr("action", "/Category/Update_Category/");
            }
            $('#frmCategoryMaster').attr("method", "POST");
            $('#frmCategoryMaster').submit();
        }


    });

   



});
