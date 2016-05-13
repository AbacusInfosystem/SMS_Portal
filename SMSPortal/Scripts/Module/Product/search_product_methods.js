function call_back(data)
{    
    $('#div_Parent_Modal_Fade').modal('show');
    $("#div_Parent_Modal_Fade").find(".modal-title").text("Upload Product Image");
}

function excel_call_back(data) {
    $('#div_Parent_Modal_Fade').modal('show');
    $("#div_Parent_Modal_Fade").find(".modal-title").text("Upload Excel");

    $('#btnUploadExcel').click(function (event) {
        if ($('#frmProductUpload').valid()) {
            $('#frmProductUpload').attr("action", "/product/product-excel-upload/");
            $('#frmProductUpload').attr("method", "post");
            $('#frmProductUpload').submit();
        }

    })
}

function Search_Products()
{
    var productViewModel =
        {
            Filter:
                {
                    Product_Id: $('#hdnProductId').val(),
                },
            Pager:
                {
                    CurrentPage: $('#hdfCurrentPage').val(),
                },
        }

    $('#divSearchGridOverlay').show();

    CallAjax("/Product/Get_Products/", "json", JSON.stringify(productViewModel), "POST", "application/json", false, Bind_Products_Grid, "", null);
}

function Bind_Products_Grid(data)
{
    var htmlText = "";
    if (data.Products.length > 0) {
        for (i = 0; i < data.Products.length; i++) {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Products[i].Product_Id + "' class='iradio-list'/>";

            htmlText += "</td>";


            htmlText += "<td>";

            htmlText += data.Products[i].Product_Name == null ? "" : data.Products[i].Product_Name;

            htmlText += "</td>";


            htmlText += "<td>";

            htmlText += data.Products[i].Product_Name == null ? "" : $.number(data.Products[i].Product_Price, 2);

            htmlText += "</td>";


            htmlText += "<td>";

            htmlText += data.Products[i].Brand_Name == null ? "" : data.Products[i].Brand_Name;

            htmlText += "</td>";


            htmlText += "<td>";

            htmlText += data.Products[i].Category_Name == null ? "" : data.Products[i].Category_Name;

            htmlText += "</td>";


            htmlText += "<td>";

            htmlText += data.Products[i].SubCategory_Name == null ? "" : data.Products[i].SubCategory_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            if (data.Products[i].Is_Biddable.toString() == 'true')
                htmlText += 'Biddable';
            else
                htmlText += 'Non-Biddable';

            htmlText += "</td>";

            //htmlText += "<td>";

            //if (data.Products[i].Is_Active.toString() == 'true')
            //    htmlText += 'Active';
            //else
            //    htmlText += 'Inactive';

            //htmlText += "</td>";

            htmlText += "</tr>";
        }
    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='7'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    $('#tblProductMaster').find("tr:gt(0)").remove();
    $('#tblProductMaster tr:first').after(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    if (data.Products.length > 0) {
        $('#hdfCurrentPage').val(data.Pager.CurrentPage);
        if (data.Pager.PageHtmlString != null || data.Pager.PageHtmlString != "") {
            $('.pagination').html(data.Pager.PageHtmlString);
        }
    }
    else {
        $('.pagination').html("");
    }

    $("#divSearchGridOverlay").hide();

    $('[name="r1"]').on('ifChanged', function ()
    {
        if ($(this).prop('checked'))
        {             
            $("#hdnProduct_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
            $("#btnUpload").show();
            $("#btnDelete").show();

        }
    });

}

function PageMore(Id) {

    $("#btnEdit").hide();
    $("#btnUpload").hide();
    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    Search_Products();
}

function Remove_Image(data)
{
    $("#div_Parent_Modal_Fade").find(".modal-body").load("/product/Upload_Product_Image", { Product_Id: $('#hdProduct_Id').val() }, call_back);
}
