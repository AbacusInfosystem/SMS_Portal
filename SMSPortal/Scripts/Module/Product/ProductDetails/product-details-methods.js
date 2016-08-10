function intialization() {

    $.cookie.json = true;
    var cart = $.cookie('cart');

    if (cart != undefined) {

        for (var i = 0; i <= cart.length - 1; i++) {
            if (parseInt($("#hdnPD_ProductId").val()) == cart[i].Product_Id) {
                $("#btnPD_AddToCart").attr("disabled", "disabled");

                var str = cart[i].Quantity;
                var quantity = str.substring(str.lastIndexOf("_") + 1, str.length);
                $(hdnQuantity).val(quantity);
                $(hdnQuantity).attr("disabled", "disabled");
            }

            $("#CartItemCount").html(cart.length);
        }
        
    }
    else {

        $("#CartItemCount").html("0");
    }
}

function changeImage(obj) {

    $("#imgCurrentProductImage").attr("src", $(obj).attr("src"));
    $("#globalsearchstr").attr("src", $(obj).attr("src"));
    var url = $(obj).attr("src");

    //$('#panel').css({
    //    'background-image': 'url(' + url + ')',
    //    'background-repeat': 'no-repeat'
    //});

    image = new Image();
    image.onload = function () {
    }
    image.src = url;

    // creating canvas object
    canvas = document.getElementById('panel');
    ctx = canvas.getContext('2d');

    $('#panel').mousemove(function (e) { // mouse move handler
        var canvasOffset = $(canvas).offset();
        iMouseX = Math.floor(e.pageX - canvasOffset.left);
        iMouseY = Math.floor(e.pageY - canvasOffset.top);
    });

    $('#panel').mousedown(function (e) { // binding mousedown event
        bMouseDown = true;
    });

    $('#panel').mouseup(function (e) { // binding mouseup event
        bMouseDown = false;
    });
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

function AddToCart1(obj, Product_Id, bIsCalledByPopup) {
    
    if ($("#frmProductDetails").valid()) {

        $.cookie.json = true;
        var cart = $.cookie('cart');

        var produc_name = $("#hdnPD_ProductName").val();

        var produc_quantity = "";

        if (bIsCalledByPopup == false) {
            produc_quantity = $("#hdnQuantity_" + Product_Id).val();
        }
        else {
            produc_quantity = $("#hdnQuantity").val();
        }


        if (cart == undefined) {
            cart = [{ 'Quantity': produc_name.trim() + "_" + produc_quantity, 'Product_Id': Product_Id }, ];
        }
        else {
            cart.push(
                { 'Quantity': produc_name.trim() + "_" + produc_quantity, 'Product_Id': Product_Id }
            );
        }

        $.cookie('cart', cart, { expires: 2 });

        $("#btnAddToCart_" + Product_Id).attr("disabled", "disabled"); // Disabling product list "Add To Cart" button
        $("#hdnQuantity_" + Product_Id).val(produc_quantity);
        $("#hdnQuantity_" + Product_Id).attr("disabled", "disabled");

        if (bIsCalledByPopup) {
            $(obj).attr("disabled", "disabled");    // Disabling product details (Popup) "Add To Cart" button
        }
        $("#CartItemCount").html(cart.length);


    }
    

 
}

