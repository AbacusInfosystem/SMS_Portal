$(function () {

    InitializeAutoComplete($('#txtSubcategory'));

    $('#hdfCurrentPage').val(0);

    Search_Subcategory();

    GetSubcategoryList();

    $("#btnEdit").click(function () {

        $("#frmSubCategory").attr("action", "/subcategory/edit-subcategories");

        $("#frmSubCategory").attr("method", "POST");

        $("#frmSubCategory").submit();
    });

    $("#btnSearch").click(function () {

        $('#hdfCurrentPage').val(0);

        Search_Subcategory();

    });

    $("#hrefSubcategory").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/SubCategory/Get_Subcategory_Popup", {}, call_back);
    });

});
