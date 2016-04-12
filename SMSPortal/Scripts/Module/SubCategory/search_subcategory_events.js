$(function () {

    $('#hdfCurrentPage').val(0);

    Search_Subcategory();

    $("#btnEdit").click(function () {

        $("#frmSubCategory").attr("action", "/subcategory/edit-subcategories");

        $("#frmSubCategory").attr("method", "POST");

        $("#frmSubCategory").submit();
    });

    $("#btnSearch").click(function () {

        $('#hdfCurrentPage').val(0);

        Search_Subcategory();

    });

});