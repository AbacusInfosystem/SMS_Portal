$(function () {

    $("#btnUploadLogo").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/brand/Add_Brand_Logo", {}, call_back);
    });

});