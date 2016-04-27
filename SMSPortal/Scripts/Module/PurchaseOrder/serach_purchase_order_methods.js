
function Search_Purchase_Orders() {
    var pViewModel =
        {
            Filter:
                {
                    Purchase_Order_Id: $("#hdnPurchaseOrderId").val(),
                },

            Pager: {
                CurrentPage: $('#hdfCurrentPage').val(),
            },
        }    
    $("#divSearchGridOverlay").show();

    CallAjax("/purchaseorder/get-purchase_orders", "json", JSON.stringify(pViewModel), "POST", "application/json", false, Bind_Purchase_Order_Grid, "", null);

}

function Bind_Purchase_Order_Grid(data)
{
  
    var htmlText = "";
    if (data.PurchaseOrders.length > 0) {
        for (i = 0; i < data.PurchaseOrders.length; i++) {

        htmlText += "<tr>";

        htmlText += "<td>";

        htmlText += "<input type='radio' name='r1' id='r1_" + data.PurchaseOrders[i].Purchase_Order_Id + "' class='iradio-list'/>";

        htmlText += "</td>";

        htmlText += "<td>";

        htmlText += data.PurchaseOrders[i].Purchase_Order_No;

        htmlText += "</td>";         

        htmlText += "</tr>";
        }
    }
    else
    {
        htmlText += "<tr>";

        htmlText += "<td colspan='2'>";

        htmlText += "No record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }
    $("#tblPurchaseOrder").find("tr:gt(0)").remove();

    $('#tblPurchaseOrder tr:first').after(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

   
    if (data.PurchaseOrders.length > 0) {
    $('#hdfCurrentPage').val(data.Pager.CurrentPage);

    if (data.Pager.PageHtmlString != null || data.Pager.PageHtmlString != "") {

        $('.pagination').html(data.Pager.PageHtmlString);
    }
    }
    else
    {
        $('.pagination').html("");
    }

    $("#divSearchGridOverlay").hide();
     
    $('[name="r1"]').on('ifChanged', function (event) {
        if ($(this).prop('checked')) {
            $("#hdn_PurchaseOrderId").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
        }
    });

    $("#btnEdit").hide();

}

function PageMore(Id) {
     
    $("#btnEdit").hide();

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    $(".selectAll").prop("checked", false);

    Search_Purchase_Orders();

}
