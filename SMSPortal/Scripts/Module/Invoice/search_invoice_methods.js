
function Search_Invoices()
{
    var invoiceViewModel =
        {
            Filter:
                {
                    Invoice_Id: $('#hdnInvoiceId').val(),
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/invoice/get-invoices/", "json", JSON.stringify(invoiceViewModel), "POST", "application/json", false, Bind_Invoices_Grid, "", null);
}

function Bind_Invoices_Grid(data)
{
    var htmlText = "";
    if (data.Invoices.length > 0)
    {
        for (i = 0; i < data.Invoices.length; i++)
        {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Invoices[i].Invoice_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Invoices[i].Invoice_No == null ? "" : data.Invoices[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Invoices[i].Order_No == null ? "" : data.Invoices[i].Order_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Invoices[i].Invoice_Date == null ? "" : Get_Date(data.Invoices[i].Invoice_Date);

            htmlText += "</td>";


            htmlText += "</tr>";
        }
    }
    else
    {
        htmlText += "<tr>";

        htmlText += "<td colspan='4'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#tblInvoices').find("tr:gt(0)").remove();
    $('#tblInvoices tr:first').after(htmlText);

    //$('.iradio-list').iCheck({
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    if (data.Invoices.length > 0)
    {
        $('#hdfCurrentPage').val(data.Pager.CurrentPage);
        if (data.Pager.PageHtmlString != null || data.Pager.PageHtmlString != "")
        {
            $('.pagination').html(data.Pager.PageHtmlString);
        }
    }
    else
    {
        $('.pagination').html("");
    }

    $("#divSearchGridOverlay").hide();


    $('[name="r1"]').on('change', function (event) {
        if ($(this).prop('checked')) {
            $("#hdnInvoice_Id").val(this.id.replace("r1_", ""));
            $("#btnDetails").show();
        }
    });

}

function PageMore(Id) {

    $("#btnDetails").hide();
    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Invoices();

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
