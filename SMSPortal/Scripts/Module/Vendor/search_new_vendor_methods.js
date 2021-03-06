﻿function Search_Vendors() {
    var vendorViewModel =
        {
            Filter:
                {
                    Vendor_ID: $('#hdnVendorId').val(),
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

        htmlText += "<td colspan='4'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#tblVendorMaster').find("tr:gt(0)").remove();
    $('#tblVendorMaster tr:first').after(htmlText);

    //$('.iradio-list').iCheck({
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

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

    $('[name="r1"]').on('change', function (event) {
        if ($(this).prop('checked')) {
            $("#hdnVendor_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
            $("#btnAddProductMapping").show();
            $("#btnDelete").show();
            $("#btnUploadLogo").show();
            //$("#btnAddUser").show();
        }
    });

}

function PageMore(Id) {

    $('#hdfCurrentPage').val((parseInt(Id) - 1));
    
    Search_Vendors();

}

function call_back(data) {

    $('#div_Parent_Modal_Fade').modal('show');
    $("#div_Parent_Modal_Fade").find(".modal-title").text("Upload Vendor Logo");

    $('#btnUpload').click(function (event) {
        //if ($('#frmUploadVendorLogo').valid()) {
        //    $('#frmUploadVendorLogo').attr("action", "/vendor/vendors-upload-logo");
        //    $('#frmUploadVendorLogo').attr("method", "post");
        //    $('#frmUploadVendorLogo').submit();
        //}

        $('#frmUploadVendorLogo').attr("action", "/vendor/vendors-upload-logo");
        $('#frmUploadVendorLogo').attr("method", "post");
        $('#frmUploadVendorLogo').submit();

    })
}