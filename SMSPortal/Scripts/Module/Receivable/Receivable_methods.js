//function AddReceivableDetailsData() {
//    $("#frmBankDetails").validate();
//    $("#frmBankDetails").find(".custom-mandatory").rules("add", { required: true, messages: { required: "Input is required." } });

//    var rowID = $("#hdnRowIdspecific").val();
//    var isEdit = $("#hdnIsEditReceivableDetails").val();

//    var bank_Name = $("#txtBank_Name").val();
//    var account_No = $("#txtAccount_No").val();
//    var ifsc_Code = $("#txtIFSC_Code").val();
//    var status = $("#hdnIsActive").val();
//    var statustring = "";
//}

$("#btnSave").click(function () {

    var ReceivableViewModel =
       {
           InvoiceNo : $("#txtInvoiceNo").val(),

           ReceivableDate: $("#txtRecDate").val(),

           TransactionType: $("#drpTransaction").val(),

           ChequeDate: $("#txtChequeDate").val(),

           ChequeNo: $("#txtChequeNo").val(),

           BankName: $("#txtBankName").val(),

           IFSC_Code: $("#txtIFSCCode").val(),

           NEFT: $("#txtNEFT").val(),

           Credit_Debit_Card: $("#txtCredit_Debit").val() 
        }
    

    CallAjax("/Receivable/Insert-Receivable/", "json", JSON.stringify(ReceivableViewModel), "POST", "application/json", false);

}); 
 