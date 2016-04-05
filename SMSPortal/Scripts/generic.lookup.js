// THIS FILE HAVE FUNCTIONALITY DEFINED INSIDE THE POPUP CONTROL.

$(document).ready(function () {

    //if ($('#div_Parent_Modal_Fade').hasClass("in")) {
        
    //    $('#div_Child_Modal_Fade').find(".modal-title").text($("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find(".lookup-title").text() + " List");
    //}
    //else
    //{
        
    //    $('#div_Parent_Modal_Fade').find(".modal-title").text($("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find(".lookup-title").text() + " List");
    //}


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


        if ($("#hdnId").val() != "") {

            if ($("#hdnLookup").val() == "TPART") {

                $("#hdnTPART_Name").val($("#hdnValue").val());

            }


            if ($("#hdnLookup").val() == "LFA1") {

                $("#hdnLFA1_Name").val($("#hdnValue").val());
            }


            $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.todo-list').remove();

            $("#" + $("#hdnLookupHiddenId").val()).val($("#hdnId").val());

            $("#" + $("#hdnLookupLabelId").val()).val("");


            var htmltext = "";

            htmltext = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + $("#hdnValue").val() + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";

            $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').append(htmltext);

            $("#" + $("#hdnLookupLabelId").val()).val($("#hdnId").val());

            $("#" + $("#hdnLookupHiddenId").val()).trigger("change");

            if ($("#hdnLookupLabelId").val() == "txtVendorId") {

                $.ajax({

                    url: '/vendor/get-initial-screen-data/',

                    data: { LIFNR: $("#txtVendorId").val() },

                    method: 'GET',

                    async: false,

                    success: function (data) {

                        if (data != null) {

                            if (data.vendor.Control_LIFNR != null) {

                                $("#txtAccountGroup").val(data.vendor.Initial_KTOKK);

                                $("#txtAccountGroup").parent().find(".lookup-btn-add").trigger("click");

                                $("#txtDisplayNotes").text(data.vendor.DisplayNotes);

                                if ($("#txtAccountGroup").val() != '' && $("#txtAccountGroup").val() != null) {
                                    //$("#btnCreate").trigger("click");
                                }
                            }
                        }
                    }
                });
            }
        }


        if ($('#div_Child_Modal_Fade').hasClass("in")) {

            $('#div_Child_Modal_Fade').find(".modal-body").empty();

            $('#div_Child_Modal_Fade').modal('toggle');
        }
        else
        {
            $('#div_Parent_Modal_Fade').find(".modal-body").empty();

            $('#div_Parent_Modal_Fade').modal('toggle');
        }
        
    });

    //$(".close").click(function () {

    //    // IF IT IS A CLONED HTML
    //    if ($("#" + $('#div_Parent_Modal_Fade').find(".modal-title").data("obj")).length) {

    //        // METHOD IS DEFINED IN GENERIC.POPUP.JS
    //        Close_Pop_Up(true);
    //    }
    //    else {

    //        Close_Pop_Up(false);
    //    }
    //});

    if ($("#hdnLookupLabelId").val() == "txtPurchasingOrganisation" || $("#hdnLookupLabelId").val() == "txtCompanyCode") {
        $("#btnfilter_Vendor").show();
    }

    $("#btnfilter_Vendor").click(function () {
        if ($("#hdnLookupLabelId").val() == "txtPurchasingOrganisation") {
            if ($("#txtVendorId").val() != "" && $("#hdnVendor_Id_Action").val() == 2) {
                $.ajax({
                    url: '/vendor/get-comapny-purchase-org/',
                    data: { vendor_Id: $("#txtVendorId").val() },
                    method: 'GET',
                    async: false,
                    success: function (data) {
                        $("[name='r1']").parents("tr").hide();
                        if (data.companyPurchaseOrg.Purchase_Org.length > 0) {
                            for (i = 0; i < data.companyPurchaseOrg.Purchase_Org.length; i++) {
                                $("#r1_" + data.companyPurchaseOrg.Purchase_Org[i].Id).parents("tr").show();
                            }
                        }
                    }
                });
            }
        }

        if ($("#hdnLookupLabelId").val() == "txtCompanyCode") {
            if ($("#txtVendorId").val() != "" && $("#hdnVendor_Id_Action").val() == 2) {
                $.ajax({
                    url: '/vendor/get-comapny-purchase-org/',
                    data: { vendor_Id: $("#txtVendorId").val() },
                    method: 'GET',
                    async: false,
                    success: function (data) {
                        $("[name='r1']").parents("tr").hide();
                        if (data.companyPurchaseOrg.Company_Codes.length > 0) {
                            for (i = 0; i < data.companyPurchaseOrg.Company_Codes.length; i++) {
                                $("#r1_" + data.companyPurchaseOrg.Company_Codes[i].Id).parents("tr").show();
                            }
                        }
                    }
                });
            }
        }

    });


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



