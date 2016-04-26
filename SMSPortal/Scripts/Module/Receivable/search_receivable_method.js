﻿function Search_Receivable() {
    var VendorViewModel =
        {
            Filter:
                {
                    Invoice_No: $('#hdnInvoice_No').val(),
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    CallAjax("/Receivable/Get-Recievable/", "json", JSON.stringify(vendorViewModel), "POST", "application/json", false, Bind_Receivable_Grid, "", null);
}


function Bind_Receivable_Grid(data) {
    var htmlText = "";
    if (data.Receivables.length > 0) {
        for (i = 0; i < data.Receivables.length; i++) {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Receivables[i].Invoice_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

            htmlText += "</td>";

            //htmlText += "<td>";

            //htmlText += data.Receivables[i].Amount == null ? "" : data.Receivables[i].Amount;

            //htmlText += "</td>";

            //htmlText += "<td>";

            //if (data.Receivables[i].Status.toString() == 'true')
            //    htmlText += 'Active';
            //else
            //    htmlText += 'InActive';

            //htmlText += "</td>";

            htmlText += "</tr>";
        }
    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='3'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#tblReceivableMaster').find("tr:gt(0)").remove();
    $('#tblReceivableMaster tr:first').after(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    if (data.Receivables.length > 0) {
        $('#hdfCurrentPage').val(data.Pager.CurrentPage);
        if (data.Pager.PageHtmlString != null || data.Pager.PageHtmlString != "") {
            $('.pagination').html(data.Pager.PageHtmlString);
        }
    }
    else {
        $('.pagination').html("");
    }

    $("#divSearchGridOverlay").hide();

    $('[name="r1"]').on('ifChanged', function (event) {
        if ($(this).prop('checked')) {
            $("#hdnVendor_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
            $("#btnAddProductMapping").show();
            $("#btnDelete").show();

        }
    });

}

function PageMore(Id) {

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Receivable();

}