
var InitializeAutoComplete = function (elementObject) {


    $(elementObject).autocomplete({
        source: function (request, response) {

            var urlString = ''

            if ($(elementObject).attr("id") == 'txtSubcategory') {
                urlString = "/subcategory/subcategory-autocomplete/" + $('#txtSubcategory').val();
            }
            if ($(elementObject).attr("id") == 'txtVendorName') {
                urlString = "/vendor/vendor-autocomplete/" + $('#txtVendorName').val();
            }
            
            if ($(elementObject).attr("id") == 'txtBrand_Name') {
                urlString = "/brand/brands-autocomplete/" + $('#txtBrand_Name').val();
            }

            if ($(elementObject).attr("id") == 'txtDealer_Name') {
                urlString = "/dealer/dealer-autocomplete/" + $('#txtDealer_Name').val();
            }

            if ($(elementObject).attr("id") == 'txtProduct_Name') {
                urlString = "/product/product-autocomplete/" + $('#txtProduct_Name').val();
            }

            if ($(elementObject).attr("id") == 'txtCategory_Name') {
                urlString = "/category/category_autocomplete/" + $('#txtCategory_Name').val();
            }

            if ($(elementObject).attr("id") == 'txtInvoice_Number') {
                urlString = "/invoice/get-invoice-autocomplete/" + $('#txtInvoice_Number').val();
                 
            }

            if ($(elementObject).attr("id") == 'txtInvoice_No') {
                urlString = "/Receivable/autocomplete-Invoice-No/" + $('#txtInvoice_No').val();
            }

            if ($(elementObject).attr("id") == 'txtUser_Name') {
                urlString = "/user/user-autocomplete/" + $('#txtUser_Name').val();
            }

            if ($(elementObject).attr("id") == 'txtPurchase_Order_No') {
                urlString = "/purchaseorder/purchase_order_autocomplete-autocomplete/" + $('#txtPurchase_Order_No').val();
            }

            if ($(elementObject).attr("id") == 'txtInvoiceNo') {
                urlString = "/receivable/receivable-autocomplete/" + $('txtInvoiceNo').val();
            }

            if ($(elementObject).attr("id") == 'txtOrderNo') {
                urlString = "/receivable/receivable-autocomplete/" + $('txtOrderNo').val();
            }

            if ($(elementObject).attr("id") == 'txtPurchase_Order_Number') {
                urlString = "/Payable/Payable-autocomplete/" + $('txtPurchase_Order_Number').val();
            }

            $.ajax({

                url: urlString,
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Label,
                            value: item.Value
                        }
                    }));
                }
            });
        },

        minLength: 1,
        focus: function (event, ui) {
            $(this).val(ui.item.label);
            return false;
        },
        select: function (event, ui) {

            $(this).val(ui.item.label);         

            $(this).parents('.form-group').find('input[type=text]').val("");
            $(this).parents('.form-group').find('.auto-complete-value').val(ui.item.value);
            $(this).parents('.form-group').find('.auto-complete-label').val(ui.item.label);

            $(this).parents('.form-group').find('.auto-complete-value').trigger('change');
            $(this).parents('.form-group').find('.auto-complete-label').trigger('change');

            if ($(this).parents('.form-group').find(".todo-list")[0]) {
                $(this).parents('.form-group').find('.todo-list').remove();
            }

            var htmlText = "<ul id='lookupUl' class='todo-list ui-sortable'><li ><span class='text'>" + ui.item.label + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";

            if ($(this).parents('.form-group').find(".ui-menu")[0]) {

                $(this).parents('.form-group').find('.text').html(ui.item.label);
            } else {

                $(this).parents('.form-group').append(htmlText);
            }

            $(this).parents('.form-group').find('.fa-remove').click(function (event) {
                event.preventDefault();
                $(this).parents('.form-group').find('input[type=text]').val("");
                $(this).parents('.form-group').find('.auto-complete-value').val("");
                $(this).parents('.form-group').find('.auto-complete-label').val("");
                $(this).parents('.form-group').find('.auto-complete-value').trigger('change');
                $(this).parents('.form-group').find('.auto-complete-label').trigger('change');
                $(this).parents('.form-group').find('.todo-list').remove();
            });

            $('.ui-autocomplete').html("");
            return false;
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-sortable");
        },
        close: function (event, ui) {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");

        }
    });

    $(elementObject).each(function () {
           
        if ($(this).parents('.form-group').find('.auto-complete-value').val() != 0) {

          
            var htmlText = "<ul id='lookupUl' class='todo-list ui-sortable'><li ><span class='text'>" + $(this).parents('.form-group').find('.auto-complete-label').val() + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";

            if ($(this).parents('.form-group').find(".ui-menu")[0]) {

                $(this).parents('.form-group').find('.text').html(ui.item.label);
            } else {

                $(this).parents('.form-group').append(htmlText);
            }

            $(this).parents('.form-group').find('input[type=text]').val("");

            $(this).parents('.form-group').find('.fa-remove').click(function (event) {
                event.preventDefault();
                $(this).parents('.form-group').find('input[type=text]').val("");
                $(this).parents('.form-group').find('.auto-complete-value').val("");
                $(this).parents('.form-group').find('.auto-complete-label').val("");
                $(this).parents('.form-group').find('.auto-complete-value').trigger('change');
                $(this).parents('.form-group').find('.auto-complete-label').trigger('change');
                $(this).parents('.form-group').find('.todo-list').remove();

            });
        }
        else {
            $(this).parents('.form-group').find('.todo-list').remove();
        }
    });
}