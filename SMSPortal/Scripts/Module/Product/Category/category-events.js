$(document).ready(function () {

    $("#CategoryPanel").find(".list-group-item").click(function () {
        $("#CategoryPanel").find(".list-group-item").each(function () {
            $(this).removeClass("active");
        })
        $(this).addClass("active");
        $('#ProductList').append().load("/Product/GetProductList?Category_Id=" + $(this).find("input:hidden").val());
        $('#SubCategories').append().load("/Product/GetSubCategories?Category_Id=" + $(this).find("input:hidden").val());
    });

});