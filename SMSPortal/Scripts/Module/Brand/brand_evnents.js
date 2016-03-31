$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmBrandMaster").attr("action", "/Brand/Search/");

        $("#frmBrandMaster").attr("method", "POST");

        $("#frmBrandMaster").submit();

    });

});