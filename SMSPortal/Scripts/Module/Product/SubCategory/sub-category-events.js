$(document).ready(function () {

    $("#SubCategoryPanel").find(".list-group-item").click(function () {

        $("#SubCategoryPanel").find(".list-group-item").each(function () {
            $(this).removeClass("active");
        })

        $(this).addClass("active");

        $('#ProductList').append().load("/Product/GetProductList?Category_Id=" + $(this).find(".Category_Id").val() + "&Sub_Category_Id=" + $(this).find(".Sub_Category_Id").val());

    });

});