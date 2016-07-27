function Search_Subcategory() {
    var sViewModel =
        {
            Filter:
                {
                    SubCategory_Id: $("#hdnSubcategoryId").val(),

                },

            Pager: {
                CurrentPage: $('#hdfCurrentPage').val(),
            },
        }

    $("#divSearchGridOverlay").show();

    CallAjax("/subcategory/get-subcategories", "json", JSON.stringify(sViewModel), "POST", "application/json", false, Bind_Subcategory_Grid, "", null);

}

function Bind_Subcategory_Grid(data)
{
  
    var htmlText = "";

    if (data.SubCategories.length > 0) {
        for (i = 0; i < data.SubCategories.length; i++) {

        htmlText += "<tr>";

        htmlText += "<td>";

        htmlText += "<input type='radio' name='r1' id='r1_" + data.SubCategories[i].Subcategory_Id + "' class='iradio-list'/>";

        htmlText += "</td>";

        htmlText += "<td>";

        htmlText += data.SubCategories[i].Subcategory_Name;

        htmlText += "</td>";

        htmlText += "<td>";

        htmlText += data.SubCategories[i].Category_Name;

        htmlText += "</td>";

        htmlText += "<td>";

        htmlText += data.SubCategories[i].Status;

        htmlText += "</td>";

        htmlText += "</tr>";
        }
    }
    else
    {
        htmlText += "<tr>";

        htmlText += "<td colspan='5'>";

        htmlText += "No record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }
    $("#tblSubCategory").find("tr:gt(0)").remove();

    $('#tblSubCategory tr:first').after(htmlText);

    //$('.iradio-list').iCheck({
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

   
    if (data.SubCategories.length > 0) {
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

    $('[name="r1"]').on('change', function (event) {
        if ($(this).prop('checked')) {
            $("#hdnSubcategory_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
        }
    });

    $("#btnEdit").hide();

}

function PageMore(Id) {
     
    $("#btnEdit").hide();

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    $(".selectAll").prop("checked", false);

    Search_Subcategory();

}
