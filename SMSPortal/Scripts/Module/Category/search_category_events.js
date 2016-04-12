
$(function () {

    $('[name="Category"]').on('ifChanged', function (event) {
         
        if ($(this).prop('checked')) {             
            $("#hdnCategory_Id").val(this.id.replace("rdo_", ""));           
            $("#btnEdit").show();
        }
    });

    $("#btnEdit").click(function () {

        $("#frmCategory").attr("action", "/Category/Get_Category_By_Id");
        $("#frmCategory").attr("method", "POST");
        $("#frmCategory").submit();
        
    });
});

$(document).ready(function () {

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });   

});