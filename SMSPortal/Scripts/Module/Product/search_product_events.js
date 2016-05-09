$(function () {

    $("#btnUpload").click(function (event) {

        var Product_Id = $('#hdnProduct_Id').val();
        $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", { Product_Id: Product_Id }, call_back);
    });

    $("#btnUploadProducts").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Excel", null, excel_call_back);
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

                if ($(".dd").is(":checked")) {

                    fileData.append('Is_Default', 'true');
                }
                else {
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
                        $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", { Product_Id: $('#hdProduct_Id').val() }, call_back());
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
        var Product_Image_Id = $(this).closest('li').find('.prod_img_id').val();
        var Product_Image_Name = $(this).closest('li').find('.prod_img_name').val();

        var param = { Product_Image_Id: Product_Image_Id, Product_Id: Product_Id, Product_Image_Name: Product_Image_Name }

        $.ajax({
            url: '/Product/Delete_Product_Image',
            type: "Post",
            data: param,
            success: function (response) {

                $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", { Product_Id: $('#hdProduct_Id').val() }, call_back);
            },
            error: function (err) {

                alert(err.statusText);
            }
        });

    });

    $('[name="productimage"]').on('ifChanged', function () {
        if ($(this).prop('checked'))
        {
            var Product_Id = $('#hdProduct_Id').val();
            var product_image_id = this.id.replace("r1_", "");
            var param = { Product_Image_Id: product_image_id, Product_Id: Product_Id }

            $.ajax({
                url: '/product/set-default-image',
                type: "Post",
                data: param,
                success: function (response) {

                    $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", { Product_Id: Product_Id }, call_back);
                },
                error: function (err) {

                    alert(err.statusText);
                }
            });
        }
    });




    InitializeAutoComplete($('#txtProduct_Name'));
    $('#hdfCurrentPage').val(0);
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

 