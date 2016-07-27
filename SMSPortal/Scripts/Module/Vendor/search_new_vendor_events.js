$(function () {
    Search_Vendors();
    InitializeAutoComplete($('#txtNewVendorName'));
    $("#btnEdit").click(function () {

        $("#frmNewVendor").attr("action", "/newvendor/edit-vendor-details/");

        $("#frmNewVendor").attr("method", "post");

        $("#frmNewVendor").submit();
    });

    $("#btnSearch").click(function () {

        Search_Vendors();

    });

    $("#btnUploadLogo").click(function (event) {

        var VendorId = $('#hdnVendor_Id').val();
        $("#div_Parent_Modal_Fade").find(".modal-body").load("/Vendor/Add_Vendor_Logo", { Id: VendorId }, call_back);
    });

});
