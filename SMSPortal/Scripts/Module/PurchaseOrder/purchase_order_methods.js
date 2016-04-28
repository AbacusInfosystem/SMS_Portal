

function Search_Purchase_Order_Items() {
    var pViewModel =
        {
            Filter:
                {
                    Purchase_Order_Id: $('#hdnPurchase_Order_Id').val(),
                }
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/PurchaseOrder/Get_Purchase_Orders_Items/", "json", JSON.stringify(pViewModel), "POST", "application/json", false, Bind_Purchase_Order_Items, "", null);
}

function Bind_Purchase_Order_Items(data)
{
    $("#tblPurchaseOrderItems").html("");
    var htmlText = "";

    $("#hdnPurchase_Order_Id").val(data.PurchaseOrder.Purchase_Order_Id);

    var htmlText = "";
    if (data.PurchaseOrderItems.length > 0)
    {
        htmlText += "<tr>";
        htmlText += "<th>Product Name</th>"
        htmlText += "<th>Product Quantity</th>"
        htmlText += "<th>Product Price</th>"
        htmlText += "<th>Shipping Date</th>"
        htmlText += "</tr>";

        for (i = 0; i < data.PurchaseOrderItems.length; i++)
        {
            htmlText += "<tr id='PItem" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + "' >";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Name == null ? "" : data.PurchaseOrderItems[i].Product_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Quantity == null ? "" : data.PurchaseOrderItems[i].Product_Quantity;

            htmlText += "</td>";           
            
            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Price == null ? "" : data.PurchaseOrderItems[i].Product_Price;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Shipping_Date == null ? "" : new Date(parseInt(data.PurchaseOrderItems[i].Shipping_Date.replace('/Date(', ''))).toLocaleDateString();

            htmlText += "</td>";

            htmlText += "<td>" + "<button type='button' id='btnEdit' class='btn btn-info btn-sm btn-Edit-SubDept fa fa-pencil-square-o' onclick='Edit_Purchase_Order_Item(" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + ")'></button>" + "</td>";

            htmlText += "<td>" + "<button type='button' id='btnRemove' class='btn btn-danger btn-Edit-SubRemove btn-sm fa fa-times' onclick='Delete_Purchase_Order_Item(" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + ")'></button>" + "</td>";

            htmlText += "<input type='hidden' class='ItmId' value='" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + "'>";
            htmlText += "<input type='hidden' class='ItmPOrderId' value='" + data.PurchaseOrderItems[i].Purchase_Order_Id + "'>";
            htmlText += "<input type='hidden' class='ItmProductId' value='" + data.PurchaseOrderItems[i].Product_Id + "'>";
            htmlText += "<input type='hidden' class='ItmProductPrice' value='" + data.PurchaseOrderItems[i].Product_Price + "'>";
            htmlText += "<input type='hidden' class='ItmProductQty' value='" + data.PurchaseOrderItems[i].Product_Quantity + "'>";
            htmlText += "<input type='hidden' class='ItmShipAdd' value='" + data.PurchaseOrderItems[i].Shipping_Address + "'>";

            htmlText += "</tr>";
        }
    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='6'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    //$('#tblPurchaseOrderItems').find("tr:gt(0)").remove();
    $('#tblPurchaseOrderItems').html(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    Reset_Purchase_Order();
    
    //if (data.Products.length > 0) {
    //    $('#hdfCurrentPage').val(data.Pager.CurrentPage);
    //    if (data.Pager.PageHtmlString != null || data.Pager.PageHtmlString != "") {
    //        $('.pagination').html(data.Pager.PageHtmlString);
    //    }
    //}
    //else {
    //    $('.pagination').html("");
    //}

    //$("#divSearchGridOverlay").hide();

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
                    Shipping_Address: $('#txtShipping_Address').val(),
                    Shipping_Date: $('#txtShippingDate').val()
                }
        }
    return pViewModel;
}

function Save_Purchase_Order_Items()
{
    var pViewModel = Set_Purchase_Order_Items();

    //if ($("#hdnPurchase_Order_Id").val() == 0)
    //{
    CallAjax("/purchaseorder/insert-update-purchase-order/", "json", JSON.stringify(pViewModel), "POST", "application/json", false, Bind_Purchase_Order_Items, "", null);
    //}
    //else {
    //    CallAjax("/purchaseorder/update-purchase-order/", "json", JSON.stringify(pViewModel), "POST", "application/json", false, Bind_Purchase_Order_Items, "", null);
    //}
}

function Edit_Purchase_Order_Item(id)
{
    $("#txtProductPrice").val($("#PItem" + id).find(".ItmProductPrice").val());
    $("#txtProductQuantity").val($("#PItem" + id).find(".ItmProductQty").val());
    $("#txtShipping_Address").val($("#PItem" + id).find(".ItmShipAdd").val());
    $("#txtShippingDate").val($("#PItem" + id).find(".ItmShipDate").val());
    
    $("#txtProductName").val($("#PItem" + id).find(".ItmProductId").val());
    //$("#hdnProductId").val($("#PItem" + id).find(".ItmProductId").val());
     
    if ($('#txtProductName').val() != 0)
        $("#divProduct").find(".autocomplete-text").trigger("focusout");

    $("#hdnPurchase_Order_Id").val($("#PItem" + id).find(".ItmPOrderId").val());
    $("#hdnPurchase_Order_Item_Id").val($("#PItem" + id).find(".ItmId").val());
}

function Delete_Purchase_Order_Item(id)
{
    if (confirm("Are you sure you want to delete this item ?") == true) {
        $("#hdnPurchase_Order_Id").val($("#PItem" + id).find(".ItmPOrderId").val());
        var Purchase_Order_Id = $("#hdnPurchase_Order_Id").val();

        $.ajax({
            url: '/purchaseorder/delete-purchase-order',
            data: { Purchase_Order_Item_Id: id, Purchase_Order_Id: Purchase_Order_Id },
            method: 'GET',
            async: false,
            success: function (data) {

                Bind_Purchase_Order_Items(data);
                //Friendly_Message(data);

            }
        });
    }
    else
    {
        $('#frmPurchaseOrderMaster').validate().cancelSubmit = true;
        //$("#frmPurchaseOrderMaster").submit(function (e) {
        //   return false;
        //});        
    }
}

function Reset_Purchase_Order()
{
    $("#txtProductPrice").val("0");
    $("#txtProductQuantity").val("0");
    $("#txtShipping_Address").val("0");
    $("#txtShippingDate").val("");
    $("#hdnProductId").val("0");
    $("#divProduct").find(".autocomplete-text").trigger("focusout");
}


