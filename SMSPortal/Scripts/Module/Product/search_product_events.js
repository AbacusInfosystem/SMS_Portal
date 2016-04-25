﻿$(function () {

    $("#btnUpload").click(function (event) {

        var Product_Id = $('#hdnProduct_Id').val();
        $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", { Product_Id: Product_Id }, call_back);
    });

    $('#btnUploadImage').click(function (event) {
        if ($('#frmUploadProductImages').valid()) {
            

            if (window.FormData !== undefined) {

                var fileUpload = $("#productImage").get(0);
                var files = fileUpload.files;

                // Create FormData object  
                var fileData = new FormData();

                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                // Adding one more key to FormData object 
                fileData.append('Product_Id', $('#hdProduct_Id').val());

                if ($(".dd").is(":checked"))
                {
                    
                    fileData.append('Is_Default', 'true');
                }
                else
                {
                    fileData.append('Is_Default', 'false');
                }


                $.ajax({
                    url: '/Product/Product_Image_Upload',
                    type: "post",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (result)
                    {
                        $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", { Product_Id: $('#hdProduct_Id').val() }, call_back);
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            } else {
                alert("FormData is not supported.");
            }

        }

    })


    $('.remove-image-attachment').click(function (event) {

        var Product_Id = $('#hdProduct_Id').val();
        var Product_Image_Id = $(this).closest('td').find('.prod_img_id').val();
        var Product_Image_Name = $(this).closest('td').find('.prod_img_name').val();

        var param = { Product_Image_Id: Product_Image_Id, Product_Id: Product_Id, Product_Image_Name: Product_Image_Name }      
         

        $.ajax({
            url: "/product/Delete-Product-Image/",
            type: "Post",  
            data: param,
            success: function (response)
            {                 
                $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", { Product_Id: $('#hdProduct_Id').val() }, call_back);
            },
            error: function (xhr) {
                alert(xhr);
            }
        });

    });

});

$(document).ready(function () {
    Search_Products();

    $("#btnEdit").click(function () {
        $("#frmProduct").attr("action", "/product/edit-product/");
        $("#frmProduct").attr("method", "post");
        $("#frmProduct").submit();
    });

    $("#btnSearch").click(function () {
        Search_Products();
    });

});