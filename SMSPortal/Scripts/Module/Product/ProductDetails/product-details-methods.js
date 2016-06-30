function intialization() {
    $.cookie.json = true;
    var cart = $.cookie('cart');
    if (cart != undefined) {
        for (var i = 0; i < cart.length; i++) {
            if (parseInt($("#hdnPD_ProductId").val()) == cart[i].Product_Id)
                $("#btnPD_AddToCart").attr("disabled", "disabled");
        }
    }
}

function changeImage(obj) {
    $("#imgCurrentProductImage").attr("src", $(obj).attr("src"));
}

function bigImg(x) {


    $(".thumbimage").mouseover(function () {

        $("#imgCurrentProductImage").attr('src', $(this).attr('id'));

    });
    //x.style.height = "500px";
    //x.style.width = "500px";
}

function normalImg(x) {


    $(".imgCurrentProductImage").mouseover(function () {

        $("#thumbimage").attr('src', $(this).attr('id'));

    });
    //x.style.height = "250px";
    //x.style.width = "400px";
}

