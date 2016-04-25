

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

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

   
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

    //$('[id^="r1_"]').on('ifChanged', function (event) {
    $('[name="r1"]').on('ifChanged', function (event) {
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

function GetSubcategoryList() {

    CallAjax("/SubCategory/Get_Subcategory_Autocomplete ", "json", null, "POST", "application/json", false, GetSubcategory, "", null);
}

function GetSubcategory(data) {

    $("#drpList").html("");
    var htmltext = "";
    htmltext += "<option value=''>-Select Sub Category-</option>";
    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {
            htmltext += "<option value='" + data[i].Value + "'>" + data[i].Label + "</option>";
        }
    }
    $("#drpList").html(htmltext);

}
function IconSearch() {

    $("#txtSubcategory").parents(".form-group").find(".fa-remove").trigger("click");

    $("#hdnSubcategoryName").val($("#drpList option:selected").text());
    $("#hdnSubcategoryId").val($("#drpList").val());

    hiddenTextValue = $("#hdnSubcategoryName").val();
    Textboxname = "#txtSubcategory";
    SetValueToAutocomplete(hiddenTextValue, Textboxname);


}

function call_back(data) {

    $('#div_Parent_Modal_Fade').modal('show');

    $("#div_Parent_Modal_Fade").find(".modal-title").text("Sub Category");

}