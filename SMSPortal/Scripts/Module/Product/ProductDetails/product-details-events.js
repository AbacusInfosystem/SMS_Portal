$(document).ready(function () {

    $("#frmProductDetails").validate({

        rules: {
            "productQuantity":
               {
                   required: true
               }

        },
        messages: {

            "productQuantity":
            {
                required: "Product quantity is required."
            }
        }
    });

    intialization();

    $("#btnPD_Proceed").click(function () {

        $.cookie.json = true;

        var id = "";

        var quantity = "";

        var cart = $.cookie('cart');

        if (cart != undefined) {

            for (var i = 0; i <= cart.length - 1; i++) {
                id = id + cart[i].Product_Id + ",";

                quantity = quantity + cart[i].Quantity + ",";
            }


            $("#hdnProductIds").val(id);

            $("#hdnProductQuantities").val(quantity);

            $("#frmProductDetails").attr("action", "/Product/PlaceOrder");

            $("#frmProductDetails").attr("method", "POST");

            $("#frmProductDetails").submit();

        }
    });

    $("#btnViewCart").click(function () {

            $("#btnPD_Proceed").trigger("click");      
    });

    image = new Image();
    image.onload = function () {
    }

    var name = $("#hdn_ProductImage").val();

    image.src = '/UploadedFiles/' + name + '';
    // creating canvas object
    canvas = document.getElementById('panel');
    ctx = canvas.getContext('2d');

    $('#panel').mousemove(function (e) { // mouse move handler
        var canvasOffset = $(canvas).offset();
        iMouseX = Math.floor(e.pageX - canvasOffset.left);
        iMouseY = Math.floor(e.pageY - canvasOffset.top);
    });

    //$('#panel').mousedown(function (e) { // binding mousedown event
    //    bMouseDown = true;
    //});

    $('#panel').mouseover(function (e) { // binding mousedown event
        bMouseDown = true;
    });

    $('#panel').mouseout(function (e) { // binding mousedown event
        bMouseDown = false;
    });

    //$('#panel').mouseup(function (e) { // binding mouseup event
    //    bMouseDown = false;
    //});

    setInterval(drawScene, 30); // loop drawScene

});