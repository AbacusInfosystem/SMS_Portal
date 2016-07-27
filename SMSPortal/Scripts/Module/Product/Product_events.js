$(function () {

    //$('input').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    increaseArea: '20%', // optional
    //});

    $(".chkstatus").on("change", function () {

        if (!$(this).is(':checked')) {
            $("#hdnIs_Active").val(false);
        }
        else {
            $("#hdnIs_Active").val(true);
        }
    });

    $(".chkBiddable").on("change", function () {

        if (!$(this).is(':checked')) {
            $("#hdnIs_Biddable").val(false);
        }
        else {
            $("#hdnIs_Biddable").val(true);
        }
    });

    $(".fa-chevron-left").click(function () {
        $("#frmProductMaster").validate().cancelSubmit = true;

        $("#frmProductMaster").attr("action", "/Product/Search/");
        $("#frmProductMaster").attr("method", "POST");
        $("#frmProductMaster").submit();

    });

    $("#btnSave").click(function () {
        if ($('#frmProductMaster').valid()) {
            if ($("#hdf_ProductId").val() == 0) {
                $("#frmProductMaster").attr("action", "/product/insert-product/");
            }
            else {
                $("#frmProductMaster").attr("action", "/product/update-product/");
            }
            $('#frmProductMaster').attr("method", "POST");
            $('#frmProductMaster').submit();


        }
    });

    $("#drpProduct_Category").change(function () {
        var Category_Id = $("#drpProduct_Category").val();
        $.ajax(
        {
            url: '/product/Get-SubCategory-By-Category-Id/',
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

    if ($("#drpProduct_Category").val() != null) {
        $("#drpProduct_Category").trigger("change");
        $("#drpProduct_SubCategory").val($("#hdnSubCategory_Id").val());
    }

    //$("#drpBrand_Category").change(function () {

    //    $.ajax({
    //        url: '/product/get-third-party-vendor',
    //        data: {
    //            brand_Id: $("#drpBrand_Category").val(),
    //        },
    //        method: 'GET',
    //        async: false,
    //        success: function (data) {
    //            if (data != null) {
    //                Bind_Vendor_Drpdwn(data);
    //            }
    //        }
    //    });
    //});

    //if ($("#hdnBrand_Id").val() != "") {
    //    $("#drpBrand_Category").trigger("change");
    //    $("#drpVendor").val($("#hdnBrand_Id").val());
    //}

    $("#btnAdd").bind("click", function () {
        AddBankDetailsData();
    });


});



