function Bind_Lookup(data) {
    var htmltext = "";
    if (data.Key != null) {
        
        $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.todo-list').remove();
        $("#" + $("#hdnLookupHiddenId").val()).val($("#" + $("#hdnLookupLabelId").val()).val());
        htmltext = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + data.Key + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";
        $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').append(htmltext);

        $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.fa-remove').click(function (event) {
            event.preventDefault();
            $(this).parents('.form-group').find('input[type=text]').val("");
            $("#" + $("#hdnLookupHiddenId").val()).val("");
            $(this).parents('.form-group').find('.todo-list').remove();
        });
    }
    else {
        $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.todo-list').remove();
        htmltext = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find(".lookup-title").text() + " does not exist</span><div class='tools'><i class='fa fa-remove'></i>";
        $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').append(htmltext);

        $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.fa-remove').click(function (event) {
            event.preventDefault();
            $(this).parents('.form-group').find('input[type=text]').val("");
            $(this).parents('.form-group').find('.todo-list').remove();
        });
    }

    if ($("#hdnLookupLabelId").val() == "txtVendorId") {
        $.ajax({
            url: '/vendor/get-initial-screen-data/',
            data: { LIFNR: $("#txtVendorId").val() },
            method: 'GET',
            async: false,
            success: function (data) {
                if (data.vendor.Control_LIFNR != null) {
                    $("#txtAccountGroup").val(data.vendor.Initial_KTOKK);
                    //$("#txtPurchasingOrganisation").val(data.vendor.Initial_EKORG);
                    // $("#txtCompanyCode").val(data.vendor.Initial_BUKRS);
                    $("#txtAccountGroup").parent().find(".lookup-btn-add").trigger("click");
                    //$("#txtPurchasingOrganisation").parent().find(".lookup-btn-add").trigger("click");
                    // $("#txtCompanyCode").parent().find(".lookup-btn-add").trigger("click");
                    //alert(data.vendor.DisplayNotes);
                    $("#txtDisplayNotes").text(data.vendor.DisplayNotes);
                    if ($("#txtAccountGroup").val() != '' && $("#txtAccountGroup").val() != null) {
                        //  $("#btnCreate").trigger("click");
                    }
                }
            }
        });
    }
}

function Bind_Userlist(data) {

   // $("#WorkFlow_User_Id").html("");

    var htmltext = "";
    htmltext += "<select multiple='multiple' class='form-control user-drplist' >";
    htmltext += "<option>-Select User-</option>";

    if (data.length > 0) {
        for (var i = 0; i < data.length ; i++) {

            htmltext += "<option value='" + data[i].User_Id + "' >" + data[i].Username + "</option>";

        }
    }
    htmltext += "</select>";

    // $("#WorkFlow_User_Id").html(htmltext);
    return htmltext;
}