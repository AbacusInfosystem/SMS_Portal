$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmPayablesMaster").attr("action", "/Payables/Search/");

        $("#frmPayablesMaster").attr("method", "POST");

        $("#frmPayablesMaster").submit();

    });

});