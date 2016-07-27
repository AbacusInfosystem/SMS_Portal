function Bind_SubCategories(data)
{    
    $("#drpProduct_SubCategory").html("");

    var htmltext = "";

    htmltext += "<option ='0'>-Select SubCategory-</option>";

    if (data.length > 0) {
        for (var i = 0; i < data.length ; i++) {
            htmltext += "<option value='" + data[i].Subcategory_Id + "'>" + data[i].Subcategory_Name + "</option>";
        }
    }
    $("#drpProduct_SubCategory").html(htmltext);

}

function Bind_Vendor_Drpdwn(data) {
    $("#drpVendor").html("");
    var htmltext = "";
    htmltext += "<option value=''>-Select Vendor-</option>";
    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {
            htmltext += "<option value='" + data[i].Entity_Id + "'>" + data[i].Entity_Name + "</option>";
        }
    }
    $("#drpVendor").html(htmltext);
}

function GetDynamicTextBox(value) {
    return '<input name = "DynamicTextBox" class="form-control form_control_mer" type="text" value = "' + value + '" />&nbsp;' +
            '<input type="button" value="Remove" class="remove" /></br>'
}

function AddBankDetailsData() {

    var trrow = $("#tblProductQty").find('tr').size() - 1;

    var tr = "<tr id='tr" + trrow + "'>";

    tr += "<td>";
    tr += "<input type='text' id='product_Qty" + trrow + "' class='form-control form_control_mer' name='Product.ProductQuantities[" + trrow + "].Product_Quantity_Value'>";
    tr += "</td>";

    tr += "<td>";
    tr += "<button type='button' id='delete' class='btn btn-box-tool btn-tel-delete' onclick='javascript:Delete(" + trrow + ")'><i class='fa fa-times' ></i></button>";
    tr += "</td>";

    tr += "</tr>";

    $('#tblProductQty tr:last').after(tr)

}


function Delete(rowId) {

    $("#tblProductQty").find("[id='tr" + rowId + "']").remove();
    ReArrangeData();

}

function ReArrangeData() {
    $("#tblProductQty").find("tr").each(function (i, row) {
        if ($(row)[0].id != 'tblHeading') {

            $(row)[0].id = 'tr' + (i - 1);

            var newTR = "#" + $(row)[0].id + " td";

            if ($(newTR).find("[id^='product_Qty']").length > 0) {
                $(newTR).find("[id^='product_Qty']")[0].id = "product_Qty" + (i - 1);
                $(newTR).find("[id^='product_Qty']").attr("name", "Product.ProductQuantities[" + (i - 1) + "].Product_Quantity_Value");
            }

            if ($(newTR).find("[id='delete']").length > 0) {
                $(newTR).find("[id='delete']").attr("onclick", "Delete(" + (i - 1) + ")");
            }
        }
    });
}