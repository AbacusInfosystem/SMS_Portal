// THIS FILE HAVE FUNCTIONALITY DEFINED INSIDE THE POPUP CONTROL.

$(document).ready(function () {

    $('.lookup-iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    $('[name="r1_Lookup"]').on('ifChanged', function (event) {

        if ($(this).prop('checked')) {

            $("#hdnId").val(this.id.replace("r1_Lookup_", ""));

            $("#hdnValue").val($(this).parent().parent().parent().find(".v1").val());
        }
    });

    $("#btnOK").click(function () {

        var hiddenTextValue = $("#hdnValue").val();
        var id = $("#hdnId").val();
        var Textboxname = "#txtSubcategory";
        $("#hdnSubcategoryName").val(hiddenTextValue);
        $("#hdnSubcategoryId").val(id);

        var htmlText = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + hiddenTextValue + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";

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

        // IF IT IS A CLONED HTML
        if ($("#" + $('#div_Parent_Modal_Fade').find(".modal-title").data("obj")).length) {

            // METHOD IS DEFINED IN GENERIC.POPUP.JS
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



