function Search_Payable()
{
    //alert(21);

    var pViewModel =

        {
            Filter:

                {

                    Purchase_Order_Id: $('#hdnPurchase_Order_Id').val(),

                    Vendor_Id: $('#hdnVendorId').val(),
                },

            Pager:

                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/Payables/Get_Payable/", "json", JSON.stringify(pViewModel), "POST", "application/json", false, Bind_Payable_Grid, "", null);
}


function Bind_Payable_Grid(data)
{

    var htmlText = "";

    if (data.Payables.length > 0)
    {

        for (i = 0; i < data.Payables.length; i++)
        {
            htmlText += "<tr>";

            htmlText += "<td>";

            //htmlText += "<input type='radio' name='r1' id='r1_" + data.Payables[i].Purchase_Order_Id + "' class='iradio-list'/>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Payables[i].Purchase_Order_Id + "_" + data.Payables[i].Purchase_Order_Amount + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Purchase_Order_No == null ? "" : data.Payables[i].Purchase_Order_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Purchase_Order_Amount == null ? "" : data.Payables[i].Purchase_Order_Amount;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Status == "" ? "Pending" : data.Payables[i].Status;

            htmlText += "</td>";

            htmlText += "</tr>";
        }
    }

    else
    {

        htmlText += "<tr>";

        htmlText += "<td colspan='3'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#tblPayableMaster').find("tr:gt(0)").remove();

    $('#tblPayableMaster tr:first').after(htmlText);

    $('.iradio-list').iCheck(
        {
            radioClass: 'iradio_square-green',


            increaseArea: '20%' // optional
        });

    if (data.Payables.length > 0)
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

    $('[name="r1"]').on('ifChanged', function (event)
    {
        if ($(this).prop('checked')) {

            //$("#hdnPurchaseOrder_Id").val(this.id.replace("r1_", ""));

            var Id = this.id.replace("r1_", "");

            $("#hdnVendor_Id").val(this.id.replace("r1_", ""));

            var String = Id.split("_");

            $("#hdnPurchaseOrder_Id").val(String[0]);

            $("#hdnPurchase_Order_Amount").val(String[1]);

            $("#btnView").show();

            $("#btnAddProductMapping").show();

            $("#btnDelete").show();

        }
    });

}

function PageMore(Id)
{

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Payable();

}