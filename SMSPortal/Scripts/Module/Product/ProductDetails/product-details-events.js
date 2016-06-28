$(document).ready(function () {

    intialization();

    $("#btnPD_Proceed").click(function () {
        $("#btnViewCart").trigger("click");
    });

    //$(".thumbimage").mouseover(function () {

    //    $("#modalImage").attr('src', $(this).attr('id'));

    //});

    //$(".fullsizeimage").mouseover(function () {
    //    $(this).toggle(function () {
    //        $(this).width('70%');
    //    });
    //});

    //$('.thumbimage').hover(function () {
    //    $(this).addClass('transition');

    //}, function () {
    //    $(this).removeClass('transition');
    //});

});