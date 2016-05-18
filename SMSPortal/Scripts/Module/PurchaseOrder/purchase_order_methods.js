

function Search_Purchase_Order_Items() {
    var pViewModel =
        {
            PurchaseOrder:
                {
                    Purchase_Order_Id: $('#hdnPurchase_Order_Id').val(),
                }
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/PurchaseOrder/Get_Purchase_Orders_Items/", "json", JSON.stringify(pViewModel), "POST", "application/json", false, Bind_Purchase_Order_Items, "", null);
}

function Bind_Purchase_Order_Items(data) {
    $("#tblPurchaseOrderItems").html("");
    var htmlText = "";

    $("#hdnPurchase_Order_Id").val(data.PurchaseOrder.Purchase_Order_Id);

    var htmlText = "";
    if (data.PurchaseOrderItems.length > 0) {
        htmlText += "<tr>";
        htmlText += "<th>Shipping Date</th>";
        htmlText += "<th>Address</th>";
        htmlText += "<th>Product Name</th>";
        htmlText += "<th>Quantity</th>";
        htmlText += "<th>Balance Qty</th>";
        htmlText += "<th>Price</th>";
        htmlText += "<th>Status</th>";
        htmlText += "<th> </th>";        
        htmlText += "</tr>";

        for (i = 0; i < data.PurchaseOrderItems.length; i++) {
            htmlText += "<tr id='PItem" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + "' >";

            htmlText += "<td>";

            var shipDate = new Date(parseInt(data.PurchaseOrderItems[i].Shipping_Date.replace('/Date(', '')));
            shipDate = (shipDate.getMonth() + 1).toString() + "/" + (shipDate.getDate().toString() + "/" + shipDate.getFullYear());

            htmlText += shipDate == null ? "" : shipDate ;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Shipping_Address == null ? "" : data.PurchaseOrderItems[i].Shipping_Address;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Name == null ? "" : data.PurchaseOrderItems[i].Product_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Quantity == null ? "" : data.PurchaseOrderItems[i].Product_Quantity;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Balance_Quantity == null ? "" : data.PurchaseOrderItems[i].Balance_Quantity;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Price == null ? "" : data.PurchaseOrderItems[i].Product_Price;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Status_Text == null ? "" : data.PurchaseOrderItems[i].Status_Text;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += "<button type='button' id='btnEdit' class='btn btn-box-tool btn-tel-edit' onclick='Edit_Purchase_Order_Item(" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + ")'><i class='fa fa-pencil'></i></button>";
            htmlText += "<button type='button' id='btnRemove' class='btn btn-box-tool btn-tel-delete' onclick='Delete_Purchase_Order_Item(" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + ")'><i class='fa fa-remove'></i></button>";

            htmlText += "</td>";

            htmlText += "<input type='hidden' class='ItmId' value='" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + "'>";
            htmlText += "<input type='hidden' class='ItmPOrderId' value='" + data.PurchaseOrderItems[i].Purchase_Order_Id + "'>";
            htmlText += "<input type='hidden' class='ItmProductId' value='" + data.PurchaseOrderItems[i].Product_Id + "'>";
            htmlText += "<input type='hidden' class='ItmProductPrice' value='" + data.PurchaseOrderItems[i].Product_Price + "'>";
            htmlText += "<input type='hidden' class='ItmProductQty' value='" + data.PurchaseOrderItems[i].Product_Quantity + "'>";
            htmlText += "<input type='hidden' class='ItmShipAdd' value='" + data.PurchaseOrderItems[i].Shipping_Address + "'>";

            htmlText += "<input type='hidden' class='ItmShipDate' value='" + shipDate + "' > ";
            
            htmlText += "<input type='hidden' class='ItmRecQty' value='" + data.PurchaseOrderItems[i].Received_Quantity + "'>";

            htmlText += "</tr>";


        }
        htmlText += "<tr style='background-color:#eee'>";
        htmlText += "<td colspan='5'><b>Total :</b></td> ";
        htmlText += "<td colspan='3'><input type='text' class='form-control input-sm valid' name='PurchaseOrder.Gross_Amount' id='txtTotalAmount' value='" + data.PurchaseOrder.Gross_Amount + "' maxlength='20' readonly='readonly'>";
        htmlText += "</td>";
        htmlText += "</tr>";


    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='6'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#OrdNo').text(data.PurchaseOrder.Purchase_Order_No);

    $('#tblPurchaseOrderItems').html(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    $("#dvSubc").addClass('disabled-div ');

    Reset_Purchase_Order();
    Friendly_Message(data);

    $('[name="r1"]').on('ifChanged', function () {
        if ($(this).prop('checked')) {
            $("#hdnProduct_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
            $("#btnUpload").show();
            $("#btnDelete").show();

        }
    });
}

function Set_Purchase_Order_Items() {

    var pViewModel =
        {
            PurchaseOrder:
                {
                    Vendor_Id: $('#hdnVendorId').val(),
                    Purchase_Order_Id: $('#hdnPurchase_Order_Id').val(),
                },
            PurchaseOrderItem:
                {
                    Purchase_Order_Item_Id: $('#hdnPurchase_Order_Item_Id').val(),
                    Purchase_Order_Id: $('#hdnPurchase_Order_Id').val(),
                    Product_Name: $('#txtProductName').val(),
                    Product_Id: $('#hdnProductId').val(),
                    Product_Quantity: $('#txtProductQuantity').val(),
                    Product_Price: $('#txtProductPrice').val(),
                    Received_Quantity: $('#txtReceived_Quantity').val(),
                    Shipping_Address: $('#txtShipping_Address').val(),
                    Shipping_Date: $('#txtShippingDate').val()
                }
        }
    return pViewModel;
}

function Save_Purchase_Order_Items() {

    var pViewModel = Set_Purchase_Order_Items();
   // $("[name='" + Product.Product_Id + "']").rules("remove", "validate_Product_Exist");
    CallAjax("/purchaseorder/insert-update-purchase-order/", "json", JSON.stringify(pViewModel), "POST", "application/json", false, Bind_Purchase_Order_Items, "", null);

}

function Edit_Purchase_Order_Item(id) {
    var product_total_price = $("#PItem" + id).find(".ItmProductPrice").val();
    var product_qty = $("#PItem" + id).find(".ItmProductQty").val();

    if (product_qty != 0) {
        var product_unit_price = (product_total_price / product_qty);
    }

    $("#txtProductPrice").val($("#PItem" + id).find(".ItmProductPrice").val());
    $("#txtUnitPrice").val(product_unit_price);    
    $("#txtProductQuantity").val($("#PItem" + id).find(".ItmProductQty").val());
    $("#txtShipping_Address").val($("#PItem" + id).find(".ItmShipAdd").val());
    $("#txtShippingDate").val($("#PItem" + id).find(".ItmShipDate").val());
    $("#txtProductName").val($("#PItem" + id).find(".ItmProductId").val());

    $("#txtReceived_Quantity").attr("readonly", false);
    $("#txtReceived_Quantity").val($("#PItem" + id).find(".ItmRecQty").val());
     

    if ($('#txtProductName').val() != 0)
        $("#divProduct").find(".autocomplete-text").trigger("focusout");

    $("#hdnPurchase_Order_Id").val($("#PItem" + id).find(".ItmPOrderId").val());
    $("#hdnPurchase_Order_Item_Id").val($("#PItem" + id).find(".ItmId").val());
}

function Delete_Purchase_Order_Item(id) {

    var purchase_order_item_id = id;   
    $("#div_Parent_Modal_Fade").find(".modal-body").load("/purchaseOrder/Confirm_Delete", { id: purchase_order_item_id }, Mycallback);    
}

function Mycallback()
{
    $('#div_Parent_Modal_Fade').modal('show');
    $("#div_Parent_Modal_Fade").find(".modal-title").text("Delete Confirmation");
    $('#btnYes').click(function () {
        alert('clicked');
        var purchase_order_item_id = $("#hdn_Id").val();
        var Purchase_Order_Id = $("#hdnPurchase_Order_Id").val();         
        $.ajax({
            url: '/purchaseorder/delete-purchase-order',
            data: { Purchase_Order_Item_Id: purchase_order_item_id, Purchase_Order_Id: Purchase_Order_Id },
            method: 'GET',
            async: false,
            success: function (data) {

                Bind_Purchase_Order_Items(data);
                Friendly_Message(data);

            }
        });

    });
} 

function Reset_Purchase_Order() {
    $("#txtProductPrice").val("0");
    $("#txtUnitPrice").val("0");
    $("#txtProductQuantity").val("");
    $("#txtReceived_Quantity").attr("readonly", true);
    $("#txtReceived_Quantity").val("0");
    $("#txtShipping_Address").val("");
    $("#txtShippingDate").val("");
    $("#hdnProductId").val("0");
    $("#hdnPurchase_Order_Item_Id").val("0");
    $("#divProduct").find(".autocomplete-text").trigger("focusout");
}

function Get_Date(date) {

    var dateString = date.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = day + "/" + month + "/" + year;
    return date;
}

function Calculate_Total(obj)
{
    var qty = $('#txtProductQuantity').val();
    var unit_price = $('#txtUnitPrice').val();
    $('#txtProductPrice').val(qty * unit_price);
}