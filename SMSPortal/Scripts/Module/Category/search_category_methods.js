
function Search_Categories()
{
    var categoryViewModel =
        {
            Filter:
                {
                    Category_Name: $('#txtCategory_Name').val(),
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/category/get-categories/", "json", JSON.stringify(categoryViewModel), "POST", "application/json", false, Bind_Category_Grid, "", null);

}

function Bind_Category_Grid(data) {
    var htmlText = "";

    if (data.Categories.length > 0) {
        for (i = 0; i < data.Categories.length; i++) {

            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Categories[i].Category_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Categories[i].Category_Name == null ? "" : data.Categories[i].Category_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            if (data.Categories[i].IsActive.toString() == 'true')
                htmlText += 'Active';
            else
                htmlText += 'InActive';

            htmlText += "</td>";

            htmlText += "</tr>";
        }
    }
    else {

        htmlText += "<tr>";

        htmlText += "<td colspan='2'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $("#tblCategory").find("tr:gt(0)").remove();
    $('#tblCategory tr:first').after(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    if (data.Categories.length > 0)
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

    $('[name="r1"]').on('ifChanged', function (event) {
        if ($(this).prop('checked')) {
            $("#hdnCategory_Id").val(this.id.replace("r1_", ""));

            $("#btnEdit").show();
            $("#btnDelete").show();

        }
    });
}

function PageMore(Id) {

    $("#btnEdit").hide();

    $("#btnDelete").hide();

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Categories();

}