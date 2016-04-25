function call_back(data) {

    $('#div_Parent_Modal_Fade').modal('show');
    $("#div_Parent_Modal_Fade").find(".modal-title").text("Upload Brand Logo");     
    
    $('#btnUpload').click(function (event) {
        if ($('#frmUploadBrandLogo').valid()) {
            $('#frmUploadBrandLogo').attr("action", "/brand/brands-upload-logo");
            $('#frmUploadBrandLogo').attr("method", "post");
            $('#frmUploadBrandLogo').submit();
        }

    })
}

function Search_Brands()
{
    var brandViewModel =
        {
            Filter:
                {
                    Brand_Name: $('#txtBrand_Name').val(),
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/brand/get-brands/", "json", JSON.stringify(brandViewModel), "POST", "application/json", false, Bind_Brands_Grid, "", null);
}

function Bind_Brands_Grid(data)
{
    var htmlText = "";
    if (data.Brands.length > 0)
    {
        for (i = 0; i < data.Brands.length; i++)
        {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Brands[i].Brand_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Brands[i].Brand_Name == null ? "" : data.Brands[i].Brand_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Brands[i].Brand_Category_Name == null ? "" : data.Brands[i].Brand_Category_Name;;

            htmlText += "</td>";

            htmlText += "<td>";

            if (data.Brands[i].Is_Active.toString() == 'true')
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

    $('#tblBrandMaster').find("tr:gt(0)").remove();
    $('#tblBrandMaster tr:first').after(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    if (data.Brands.length > 0)
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
        if ($(this).prop('checked'))
        {            
            $("#hdnBrand_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
            $("#btnUploadLogo").show();            
            $("#btnDelete").show();

        }
    });

}

function PageMore(Id) {

    $("#btnEdit").hide();
    $("#btnUploadLogo").hide();
    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Brands();

}
