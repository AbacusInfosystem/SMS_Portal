$(function () {

    //$('input:not(.non-iCheck input:checkbox)').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    $("#txtContactNo1").mask("(99) 99999-99999");
    $("#txtContactNo2").mask("(999) 999-9999");

    $(".fa-chevron-left").click(function () {

        $("#frmDealerMaster").validate().cancelSubmit = true;
        $("#frmDealerMaster").attr("action", "/Dealer/Search/");
        $("#frmDealerMaster").attr("method", "POST");
        $("#frmDealerMaster").submit();

    });


    $(".chkstatus").on("change", function () {

        if (!$(this).is(':checked')) {
            $("#hdnIs_Active").val(false);
        }
        else {
            $("#hdnIs_Active").val(true);
        }
    });

    $("#btnSave").click(function () {

        if ($('#frmDealerMaster').valid()) {
            if ($("#hdf_DealerId").val() == 0) {
                $("#frmDealerMaster").attr("action", "/dealer/insert-dealer/");
            }
            else {
                $("#frmDealerMaster").attr("action", "/dealer/update-dealer/");
            }
            $('#frmDealerMaster').attr("method", "POST");
            $('#frmDealerMaster').submit();
        }
    });

    //    $('#txtDealerPercentage').on('change', function () {

    //    if ($('#txtDealerPercentage').val() != "")
    //    {
    //        alert('dfdg');
    //    }

    //});
    

});

function Calc_Percentage(obj) {   
    var txtName = obj.id;
    
    if (txtName == 'txtDealerPercentage')
    {
        if (obj.value != "") {
            if (parseFloat(obj.value) < 100) {
                $('#err_dealer').text("");
                var dealer_percetage = parseFloat(obj.value);
                var brand_val = 0;
                if ($('#txtBrandPercentage').val() != "") {
                    brand_val = 0;
                }
                else {
                    brand_val = parseFloat($('#txtBrandPercentage').val());
                }
                brand_val = 100 - dealer_percetage;
                $('#txtBrandPercentage').val(brand_val);
            }
            else {
                $('#err_dealer').text('Percentage must be less than 100');
            }
        }
    }
    if (txtName == 'txtBrandPercentage')
    {
        if (obj.value != "") {
            if (parseFloat(obj.value) < 100) {
                $('#err_brand').text("");
                var brand_percetage = parseFloat(obj.value);
                var dealer_val = 0;
                if ($('#txtDealerPercentage').val() != "") {
                    dealer_val = 0;
                }
                else {
                    dealer_val = parseFloat($('#txtDealerPercentage').val());
                }
                dealer_val = 100 - brand_percetage;
                $('#txtDealerPercentage').val(dealer_val);
            }
            else {
                $('#err_brand').text('Percentage must be less than 100');
            }
        }
    }

}
