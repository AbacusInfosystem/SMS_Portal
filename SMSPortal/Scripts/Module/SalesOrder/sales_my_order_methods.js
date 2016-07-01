function Search_Sales_Order() {
    var sViewModel =
        {
            Filter:
                {
                    Order_Id: $("#hdnOrderId").val(),
                    Status: $('#drpOrderStatus').val(),
                    Date_Range: $('#reservation').val(),
                },

            Pager: {
                CurrentPage: $('#hdfCurrentPage').val(),
            },

            Cookies: {
                Entity_Id: $('#hdnDealer_Id').val(),
            },
        }

    $("#divSearchGridOverlay").show();

    CallAjax("/salesorde/get-orders", "json", JSON.stringify(sViewModel), "POST", "application/json", false, Bind_Sales_Order_Grid, "", null);

}

function Bind_Sales_Order_Grid(data) {

    var htmlText = "";

    if (data.Sales_Orders.length > 0) {
        for (i = 0; i < data.Sales_Orders.length; i++) {

            var showOrderDate = new Date(parseInt(data.Sales_Orders[i].Order_Date.replace('/Date(', '')));
            showOrderDate = (showOrderDate.getMonth() + 1).toString() + "/" + (showOrderDate.getDate().toString() + "/" + showOrderDate.getFullYear());

            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Sales_Orders[i].Order_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Sales_Orders[i].Order_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += showOrderDate;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Sales_Orders[i].Gross_Amount;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Sales_Orders[i].Status;

            htmlText += "</td>";

            htmlText += "</tr>";
        }
    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='5'>";

        htmlText += "No record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $("#tblMyOrders").find("tr:gt(0)").remove();

    $('#tblMyOrders tr:first').after(htmlText);

    //$('.iradio-list').iCheck({
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});


    if (data.Sales_Orders.length > 0) {
        $('#hdfCurrentPage').val(data.Pager.CurrentPage);

        if (data.Pager.PageHtmlString != null || data.Pager.PageHtmlString != "") {

            $('.pagination').html(data.Pager.PageHtmlString);
        }
    }
    else {
        $('.pagination').html("");
    }

    $("#divSearchGridOverlay").hide();

    $('[name="r1"]').on('change', function (event) {
        if ($(this).prop('checked')) {
            $("#hdnOrder_Id").val(this.id.replace("r1_", ""));
            $("#btnDetails").show();
        }
    });

    $("#btnDetails").hide();

}

function PageMore(Id) {

    $("#btnEdit").hide();

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    $(".selectAll").prop("checked", false);

    Search_Sales_Order();

}
