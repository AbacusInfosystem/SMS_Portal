

function Search_Purchase_Order_Items() {
    var productViewModel =
        {
            Filter:
                {
                    Product_Id: $('#hdnProductId').val(),
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/Product/Get_Products/", "json", JSON.stringify(productViewModel), "POST", "application/json", false, Bind_Purchase_Order_Items, "", null);
}

function Bind_Purchase_Order_Items(data) {
    var htmlText = "";
    if (data.PurchaseOrderItems.length > 0) {
        for (i = 0; i < data.PurchaseOrderItems.length; i++) {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.PurchaseOrderItems[i].Purchase_Order_Item_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Name == null ? "" : data.PurchaseOrderItems[i].Product_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Quantity == null ? "" : data.PurchaseOrderItems[i].Product_Quantity;

            htmlText += "</td>";


            htmlText += "<td>";

            htmlText += data.PurchaseOrderItems[i].Product_Price == null ? "" : data.PurchaseOrderItems[i].Product_Price;

            htmlText += "</td>";

            htmlText += "</tr>";
        }
    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='4'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#tblPurchaseOrderItems').find("tr:gt(0)").remove();
    $('#tblPurchaseOrderItems tr:first').after(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

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

//function PageMore(Id) {

//    $("#btnEdit").hide();
//    $("#btnUpload").hide();
//    $('#hdfCurrentPage').val((parseInt(Id) - 1));

//    Search_Products();
//}