function Search_Users()
{
    var uViewModel =
        {
            Filter:
                {
                    User_Id: $('#hdnUserId').val(),
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/User/Get_Users/", "json", JSON.stringify(uViewModel), "POST", "application/json", false, Bind_User_Grid, "", null);

}

function Bind_User_Grid(data) {
    var htmlText = "";

    if (data.Users.length > 0) {
        for (i = 0; i < data.Users.length; i++) {

            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Users[i].User_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Users[i].User_Name == null ? "" : data.Users[i].User_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Users[i].Status == null ? "" : data.Users[i].Status;

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

    $("#tblUser").find("tr:gt(0)").remove();
    $('#tblUser tr:first').after(htmlText);

    //$('.iradio-list').iCheck({
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    if (data.Users.length > 0) {
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
            $("#hdnUser_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
            $("#btnDelete").show();
        }
    });
}
function PageMore(Id) {

    $("#btnEdit").hide();

    $("#btnDelete").hide();

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Users();

}