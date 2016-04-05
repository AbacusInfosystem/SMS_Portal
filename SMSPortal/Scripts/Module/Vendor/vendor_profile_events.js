
$(function () {

    $("#btnEdit").click(function (event) {
        alert(1)
        $("#div_Parent_Modal_Fade").find(".modal-body").load("/vendor/Add_Bank_Details", {}, call_back);
    });

});