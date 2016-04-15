function call_back(data) {

    $('#div_Parent_Modal_Fade').modal('show');

    $("#div_Parent_Modal_Fade").find(".modal-title").text("Vendor Bank Details");

}
function prod_map_call_back(data) {

    $('#div_Parent_Modal_Fade').modal('show');

    $("#div_Parent_Modal_Fade").find(".modal-title").text("Vendor Product Mapping");

}
function Search_Vendors() {
    var vendorViewModel =
        {
            Filter:
                {
                    Vendor_Name: $('#txtVendor_Name').val(),
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/Vendor/Get_Vendors/", "json", JSON.stringify(vendorViewModel), "POST", "application/json", false, Bind_Vendors_Grid, "", null);
}

function Bind_Vendors_Grid(data) {
    var htmlText = "";
    if (data.Vendors.length > 0) {
        for (i = 0; i < data.Vendors.length; i++) {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Vendors[i].Vendor_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Vendors[i].Vendor_Name == null ? "" : data.Vendors[i].Vendor_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Vendors[i].Address == null ? "" : data.Vendors[i].Address;

            htmlText += "</td>";

            htmlText += "<td>";

            if (data.Vendors[i].Is_Active.toString() == 'true')
                htmlText += 'Active';
            else
                htmlText += 'InActive';

            htmlText += "</td>";

            htmlText += "</tr>";
        }
    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='3'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#tblVendorMaster').find("tr:gt(0)").remove();
    $('#tblVendorMaster tr:first').after(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    if (data.Vendors.length > 0) {
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
            $("#hdnvendor_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
            $("#btnDelete").show();

        }
    });

}

function PageMore(Id) {

    $("#btnEdit").hide();
    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Vendors();

}