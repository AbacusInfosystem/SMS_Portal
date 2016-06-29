
function Search_Dealers()
{
    var dealerViewModel =
        {
            Filter:
                {
                    Dealer_Id: $('#hdnDealerId').val(),

                    Brand_Id: $('#hdnBrandId').val()
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
            Cookies:
                {
                    Entity_Id: $('#hdnBrandId').val()
                 }
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/dealer/get-dealers/", "json", JSON.stringify(dealerViewModel), "POST", "application/json", false, Bind_Dealers_Grid, "", null);
}

function Bind_Dealers_Grid(data)
{
    var htmlText = "";
    if (data.Dealers.length > 0)
    {
        for (i = 0; i < data.Dealers.length; i++)
        {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Dealers[i].Dealer_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Dealers[i].Dealer_Name == null ? "" : data.Dealers[i].Dealer_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Dealers[i].Brand_Name == null ? "" : data.Dealers[i].Brand_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            if (data.Dealers[i].Is_Active.toString() == 'true')

                htmlText += 'Active';

            else

                htmlText += 'InActive';

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

    $('#tblDealerMaster').find("tr:gt(0)").remove();

    $('#tblDealerMaster tr:first').after(htmlText);

    //$('.iradio-list').iCheck(
    //    {
    //    radioClass: 'iradio_square-green',

    //    increaseArea: '20%' // optional
    //});

    if (data.Dealers.length > 0)
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

    //$('[name="r1"]').on('ifChanged', function (event)
    //{
    //    if ($(this).prop('checked'))

    //    {
    //        $("#hdnDealer_Id").val(this.id.replace("r1_", ""));           

    //        $("#btnDelete").show();

    //        var check = $('#hdnIs_Brand').val();

    //        if ($('#hdnIs_Brand').val() == "True")
    //        {
    //            $("#btnView").show();
    //        }
    //        else {
    //            $("#btnEdit").show();

    //            $("#btnAddUser").show();
    //        }
    //    }
    //});

    $('[name="r1"]').on('change', function (event) {
        if ($(this).prop('checked')) {
            $("#hdnDealer_Id").val(this.id.replace("r1_", ""));

            $("#btnDelete").show();

            var check = $('#hdnIs_Brand').val();

            if ($('#hdnIs_Brand').val() == "True") {
                $("#btnView").show();
            }
            else {
                $("#btnEdit").show();

                $("#btnAddUser").show();
            }
        }
    });

}

function PageMore(Id)
{

    $("#btnEdit").hide();    

    $("#btnView").hide();

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Dealers();

}


