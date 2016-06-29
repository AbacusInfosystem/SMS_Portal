// THIS FILE HAVE FUNCTIONALITY DEFINED INSIDE THE POPUP CONTROL.

$(document).ready(function () {

    //$('.lookup-iradio-list').iCheck({
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    //$('[name="r1_Lookup"]').on('Changed', function (event) {

    //    alert(1);

    //    if ($(this).prop('checked')) {

    //        $("#hdnId").val(this.id.replace("r1_Lookup_", ""));

    //        $("#hdnValue").val($(this).parent().parent().parent().find(".v1").val());

    //        alert($("#hdnId").val());
    //        alert($("#hdnValue").val());
    //    }
    //});

    $("#btnOK").click(function () {

        $('.ui-sortable').each(function () {
            $('#lookupUlAuto').remove()
        });

        $('.ui-sortable').each(function () {
            $('#lookupUlLookup').remove()
        });

        var hiddenTextValue = $("#hdnValue").val();

        var id = $("#hdnId").val();

        var Textboxname = "#" + $("#hdnLookupLabelId").val();
        

        // Get ProductInfo for Purchase order items
        var vendor_id = $('#hdnVendorId').val();

        if ($("#hdnLookupLabelId").val() == "txtProductName");
        {
            $.ajax({
                url: '/purchaseorder/Get_Product/',
                data: { Product_Id: id, Vendor_Id: vendor_id },
                method: 'GET',
                async: false,
                success: function (data) {

                    if (data != null)
                    {
                        if (data.Product != null)
                            $('#txtUnitPrice').val(data.Product.Product_Price);
                    }
                }
            });
        }

         

        // Get Invoice amount for receivables
        if ($("#hdnLookupLabelId").val() == "txtInvoiceNo");
        {             
            $.ajax({
                url: '/Receivable/Get_Invoice_Amount_By_Id/',
                data: { Id: id },
                method: 'GET',
                async: false,
                success: function (data) {

                    if (data != null) {
                        if (data.Receivable != null)
                            $('#txtInvoice_Amount').val(data.Receivable.Invoice_Amount);
                    }
                }
            });
        }

        $("#" + $("#hdnLookupHiddenId").val()).val(id);

        var htmlText = "<ul id='lookupUlLookup' class='todo-list ui-sortable'><li ><span class='text'>" + hiddenTextValue + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";

        $(Textboxname).parents('.form-group').append(htmlText);

        $(Textboxname).parents('.form-group').find('.fa-remove').click(function (event) {
            event.preventDefault();
            $(this).parents('.form-group').find('input[type=text]').val("");
            $(this).parents('.form-group').find('.auto-complete-value').val("");
            $(this).parents('.form-group').find('.auto-complete-label').val("");
            $(this).parents('.form-group').find('.auto-complete-value').trigger('change');
            $(this).parents('.form-group').find('.auto-complete-label').trigger('change');
            $(this).parents('.form-group').find('.todo-list').remove();
        });

        $('#div_Parent_Modal_Fade').find(".modal-body").empty();

        $('#div_Parent_Modal_Fade').modal('toggle')

    });

    $(".close").click(function () {

        if ($("#" + $('#div_Parent_Modal_Fade').find(".modal-title").data("obj")).length) {

            Close_Pop_Up(true);
        }
        else {

            Close_Pop_Up(false);
        }
    });

    if ($("#hdnLookupLabelId").val() == "txtPurchasingOrganisation" || $("#hdnLookupLabelId").val() == "txtCompanyCode") {
        $("#btnfilter_Vendor").show();
    }

});

// PAGE MORE METHOD FOR LOOKUPS.
function PageMoreFilter(Id) {

    var textValue = $("#hdnLookupText").val();

    $('#hdfCurrentPage').val((parseInt(Id) - 1));

    if (textValue != undefined) {
        Get_Look_Up_Filter(textValue);
    }
    else {
        Get_Look_Up(true, $("#" + $("#hdnLookupLabelId").val()).closest(".form-group").find(".lookup-btn"), true);
    }

}

function RadioChanged(ele)
{
   // $('[name="r1_Lookup"]').on('Changed', function (event) {

        if ($(ele).prop('checked')) {

            $("#hdnId").val(ele.id.replace("r1_Lookup_", ""));

            $("#hdnValue").val($(ele).parent().parent().parent().find(".v1").val());

        }

   // });
}



