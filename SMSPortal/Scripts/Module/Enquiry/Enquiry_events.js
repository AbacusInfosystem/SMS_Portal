$(function () {

    $(".fa-chevron-left").click(function () {

        $("#frmEnquiryMaster").attr("action", "/Enquiry/Search/");

        $("#frmEnquiryMaster").attr("method", "POST");

        $("#frmEnquiryMaster").submit();

    });

});