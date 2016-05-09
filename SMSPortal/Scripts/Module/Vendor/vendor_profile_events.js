
$(function () {

    $("#btnEdit").click(function () {

        $("#frmVendorProfileMaster").attr("action", "/vendor/edit-vendor-profile-details/");

        $("#frmVendorProfileMaster").attr("method", "post");

        $("#frmVendorProfileMaster").submit();
    });

    $("#btnAddBankDetails").click(function (event) {

        $("#div_Parent_Modal_Fade").find(".modal-body").load("/vendor/Add_Bank_Details", { vendor_Id: $("#hdnVendor_Id").val() }, call_back);
    });

});