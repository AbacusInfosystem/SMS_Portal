function AddBankDetailsData() {

    $("#frmBankDetails").validate();

    $("#frmBankDetails").find(".custom-mandatory").each(function (i) {
        Set_Mandetory_Validation($(this).attr("name"));
    });

    var rowID = $("#hdnRowIdspecific").val();
    var isEdit = $("#hdnIsEditBankDetails").val();

    var bank_Name = $("#txtBank_Name").val();
    var account_No = $("#txtAccount_No").val();
    var ifsc_Code = $("#txtIFSC_Code").val();    
    var status = $("#hdnIsActive").val();
    var statustring = "";
    
    if (status == "true") {
        statustring = "Active";
    }
    else {
        statustring = "InActive";
    }

    if ($("#frmBankDetails").valid())
    {
        if (isEdit == "false" || isEdit == false) {
            var trrow = $("#tblBankDetails").find('tr').size() - 1;

            var tr = "<tr id='tr" + trrow + "'>";

            tr += "<td>";
            tr += "<span id='trBankName" + trrow + "'>" + bank_Name + "</span>";
            tr += "<input type='hidden' id='hdnbank_Name" + trrow + "' name='Vendor.BankDetailsList[" + trrow + "].Bank_Name' value='" + bank_Name + "'>";
            tr += "<input type='hidden' id='hdnVendor_Bank_Detail_Id" + trrow + "' name='Vendor.BankDetailsList[" + trrow + "].Vendor_Bank_Detail_Id' value=''>";
            tr += "</td>";

            tr += "<td>";
            tr += "<span id='trAccount_No" + trrow + "'>" + account_No + "</span>";
            tr += "<input type='hidden' id='hdnaccount_No" + trrow + "' name='Vendor.BankDetailsList[" + trrow + "].Account_No' value='" + account_No + "'>";
            tr += "</td>";

            tr += "<td>";
            tr += "<span id='trIfsc_Code" + trrow + "'>" + ifsc_Code + "</span>";
            tr += "<input type='hidden' id='hdnifsc_Code" + trrow + "' name='Vendor.BankDetailsList[" + trrow + "].Ifsc_Code' value='" + ifsc_Code + "'>";
            tr += "</td>";

            tr += "<td>";
            tr += "<span id='trStatus" + trrow + "'>" + statustring + "</span>";
            tr += "<input type='hidden' id='hdnstatus" + trrow + "' name='Vendor.BankDetailsList[" + trrow + "].Status' value='" + status + "'>";
            tr += "</td>";

            tr += "<td class='edit'>";
            tr += "<button type='button' id='edit-bank-details' class='btn btn-box-tool btn-tel-edit' onclick='javascript:EditBankDetailsData(" + trrow + ")'><i class='fa fa-pencil' ></i></button>";
            tr += "<button type='button' id='delete-bank-details' class='btn btn-box-tool btn-tel-delete' onclick='javascript:DeleteBankDetailsData(" + trrow + ")'><i class='fa fa-times' ></i></button>";
            tr += "</td>";
            tr += "</tr>";

            $('#tblBankDetails tr:last').after(tr)
        }
        else
        {
            $("#trBankName" + rowID).text(bank_Name);
            $("#hdnbank_Name" + rowID).val(bank_Name);
            $("#trAccount_No" + rowID).text(account_No);
            $("#hdnaccount_No" + rowID).val(account_No);
            $("#trIfsc_Code" + rowID).text(ifsc_Code);
            $("#hdnifsc_Code" + rowID).val(ifsc_Code);
            $("#trStatus" + rowID).text(statustring);
            $("#hdnstatus" + rowID).val(status);
        }
    }

    $("#frmBankDetails").find(".custom-mandatory").each(function () {
        Remove_Validations($(this).attr("name"));
    });

    ResetBankDetailsData();

}

function ResetBankDetailsData() {

    $("#hdnRowIdspecific").val(0);

    $("#txtBank_Name").val('');

    $("#txtAccount_No").val('');

    $("#txtIFSC_Code").val('');

    $("#chkStatus").iCheck('uncheck');

    $("#hdnIsActive").val(false);

    $("#hdnIsEditBankDetails").val(false);

}

function EditBankDetailsData(rowId) {

    var strBankDetails = $("#hdnbank_Name" + rowId).val();
    $("#txtBank_Name").val(strBankDetails);

    var strAccountNo = $("#hdnaccount_No" + rowId).val();
    $("#txtAccount_No").val(strAccountNo);

    var strIfscCode = $("#hdnifsc_Code" + rowId).val();
    $("#txtIFSC_Code").val(strIfscCode);

    var strStatus = $("#hdnstatus" + rowId).val();

    if (strStatus == "true") {
        $("#chkStatus").prop("checked", true);
        $("#hdnIsActive").val(true);
    }
    else
    {
        $("#chkStatus").prop("checked", false);
        $("#hdnIsActive").val(false);
    }

    $("#hdnRowIdspecific").val(rowId);
    $("#hdnIsEditBankDetails").val(true);

}

function DeleteBankDetailsData(rowId) {

    $("#tblBankDetails").find("[id='tr" + rowId + "']").remove();
    ReArrangeBankDetailsData();

}

function ReArrangeBankDetailsData() {
    $("#tblBankDetails").find("tr").each(function (i, row) {
        if ($(row)[0].id != 'tblHeading') {

            $(row)[0].id = 'tr' + (i-1);

            var newTR = "#" + $(row)[0].id + " td";

            if ($(newTR).find("[id^='hdnbank_Name']").length > 0) {
                $(newTR).find("[id^='hdnbank_Name']")[0].id = "hdnbank_Name" + (i-1);
                $(newTR).find("[id^='trBankName']")[0].id = "trBankName" + (i-1);
                $(newTR).find("[id^='hdnbank_Name']").attr("name", "Vendor.BankDetailsList[" + (i - 1) + "].Bank_Name");
                $(newTR).find("[id^='hdnVendor_Bank_Detail_Id']").attr("name", "Vendor.BankDetailsList[" + (i - 1) + "].Vendor_Bank_Detail_Id");
            }

            if ($(newTR).find("[id^='hdnaccount_No']").length > 0) {
                $(newTR).find("[id^='hdnaccount_No']")[0].id = "hdnaccount_No" + (i-1);
                $(newTR).find("[id^='trAccount_No']")[0].id = "trAccount_No" + (i-1);
                $(newTR).find("[id^='hdnaccount_No']").attr("name", "Vendor.BankDetailsList[" + (i-1) + "].Account_No");
            }

            if ($(newTR).find("[id^='hdnifsc_Code']").length > 0) {
                $(newTR).find("[id^='hdnifsc_Code']")[0].id = "hdnifsc_Code" + (i-1);
                $(newTR).find("[id^='trIfsc_Code']")[0].id = "trIfsc_Code" + (i-1);
                $(newTR).find("[id^='hdnifsc_Code']").attr("name", "Vendor.BankDetailsList[" + (i-1) + "].Ifsc_Code");
            }

            if ($(newTR).find("[id^='hdnstatus']").length > 0) {
                $(newTR).find("[id^='hdnstatus']")[0].id = "hdnstatus" + (i-1);
                $(newTR).find("[id^='trStatus']")[0].id = "trStatus" + (i-1);
                $(newTR).find("[id^='hdnstatus']").attr("name", "Vendor.BankDetailsList[" + (i-1) + "].Status");
            }

            if ($(newTR).find("[id='edit-bank-details']").length > 0) {
                $(newTR).find("[id='edit-bank-details']").attr("onclick", "EditBankDetailsData(" + (i-1) + ")");
            }

            if ($(newTR).find("[id='delete-bank-details']").length > 0) {
                $(newTR).find("[id='delete-bank-details']").attr("onclick", "DeleteBankDetailsData(" + (i-1) + ")");
            }
        }
    });
}
