﻿$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmDealerMaster").attr("action", "/Dealer/Search/");

        $("#frmDealerMaster").attr("method", "POST");

        $("#frmDealerMaster").submit();

    });

});
$(document).ready(function () {

    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});