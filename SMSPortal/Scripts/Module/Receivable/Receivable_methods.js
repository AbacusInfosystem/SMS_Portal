 
function Save_Receivable_Data() {

    var rViewModel =
     {
         Receivable : {
                          
             Invoice_Id: $("#hdnInvoiceId").val(),

             Invoice_Amount: $("#txtInvoice_Amount").val(),

             Receivable_Item_Amount: $("#txtReceivable_Item_Amount").val(),

             Receivable_Date: $("#txtRecDate").val(),

             Transaction_Type: $("#drpTransaction").val(),

             Cheque_Date: $("#txtChequeDate").val(),

             Cheque_Number: $("#txtChequeNo").val(),

             Bank_Name: $("#txtBankName").val(),

             IFSC_Code: $("#txtIFSCCode").val(),

             NEFT: $("#txtNEFT").val(),

             Credit_Debit_Card: $("#txtCredit_Debit").val(),

             Receivable_Id: $("#hdnReceivable_Id").val(),

             Receivable_Item_Id: $("#hdnReceivable_Item_Id").val()
         }
     }


    CallAjax("/Receivable/Insert-Receivable", "json", JSON.stringify(rViewModel), "POST", "application/json", false, Bind_Receivable_Grid_Items, "", null);

}

function Bind_Receivable_Grid_Items(data) {

    $("#tblReceivableItems").html("");

    var htmlText = "";
    
    $("#txtInvoiceNo").val(data.Receivable.Invoice_Id),

    $("#hdnReceivable_Id").val(data.Receivable.Receivable_Id),

    $("#hdnInvoiceId").val(data.Receivable.Invoice_Id),

    $("#txtInvoice_Amount").val(data.Receivable.Invoice_Amount)

    $("#dvInvoice").find(".autocomplete-text").trigger("focusout");

    if (data.Receivables.length > 0) {

        htmlText += "<tr>";

        htmlText += "<th>Transaction Type</th>";

        htmlText += "<th>Amount</th>";

        htmlText += "<th>Bank Name</th>";

        htmlText += "<th>IFSC Code</th>";

        htmlText += "<th>Cheque No</th>";

        htmlText += "<th>Cheque Date</th>";

        htmlText += "<th>NEFT Details</th>";

        htmlText += "<th>Credit/Debit Card Details</th>";

        htmlText += "<th>Receivable Date</th>";

        htmlText += "<th>Action</th>";

        htmlText += "</tr>";

        for (i = 0; i < data.Receivables.length; i++) {

            var showReceivableDate = new Date(parseInt(data.Receivables[i].Receivable_Date.replace('/Date(', '')));
            showReceivableDate = (showReceivableDate.getMonth() + 1).toString() + "/" + (showReceivableDate.getDate().toString() + "/" + showReceivableDate.getFullYear());

            var showChequeDate = new Date(parseInt(data.Receivables[i].Cheque_Date.replace('/Date(', '')));
            showChequeDate = (showChequeDate.getMonth() + 1).toString() + "/" + (showChequeDate.getDate().toString() + "/" + showChequeDate.getFullYear());

            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Transaction_Type_Name == null ? "" : data.Receivables[i].Transaction_Type_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Receivable_Item_Amount == null ? "" : data.Receivables[i].Receivable_Item_Amount;

            htmlText += "</td>";        

            htmlText += "<td>";

            htmlText += data.Receivables[i].Bank_Name == null ? "" : data.Receivables[i].Bank_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].IFSC_Code == null ? "" : data.Receivables[i].IFSC_Code;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Cheque_Number == null ? "" : data.Receivables[i].Cheque_Number;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].showChequeDate == null ? "" : data.Receivables[i].showChequeDate;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].NEFT == null ? "" : data.Receivables[i].NEFT;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Credit_Debit_Card == null ? "" : data.Receivables[i].Credit_Debit_Card;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += showReceivableDate == null ? "" : showReceivableDate;

            htmlText += "<input type='hidden' id='hdnTransaction_Type" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].Transaction_Type + "'/>";

            htmlText += "<input type='hidden' id='hdnReceivable_Item_Amount" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].Receivable_Item_Amount + "'/>";

            htmlText += "<input type='hidden' id='hdnBank_Name" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].Bank_Name + "'/>";

            htmlText += "<input type='hidden' id='hdnIFSC_Code" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].IFSC_Code + "'/>";

            htmlText += "<input type='hidden' id='hdnCheque_Number" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].Cheque_Number + "'/>";

            htmlText += "<input type='hidden' id='hdnCheque_Date" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].showChequeDate + "'/>";

            htmlText += "<input type='hidden' id='hdnNEFT" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].NEFT + "'/>";

            htmlText += "<input type='hidden' id='hdnCredit_Debit_Card" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].Credit_Debit_Card + "'/>";

            htmlText += "<input type='hidden' id='hdnReceivable_Date" + data.Receivables[i].Receivable_Item_Id + "' value='" + showReceivableDate + "'/>";

            htmlText += "<input type='hidden' id='hdnReceivable_Item_Id" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].Receivable_Item_Id + "'/>";

            htmlText += "<input type='hidden' id='hdnReceivable_Id" + data.Receivables[i].Receivable_Item_Id + "' value='" + data.Receivables[i].Receivable_Id + "'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += "<button type='button' id='edit-receivable-details' class='btn btn-box-tool btn-tel-edit' onclick='javascript:EditReceivableData(" + data.Receivables[i].Receivable_Item_Id + ")'><i class='fa fa-pencil' ></i></button>";

            htmlText += "<button type='button' id='delete-receivable-details' class='btn btn-box-tool btn-tel-delete' onclick='javascript:DeleteReceivableData(" + data.Receivables[i].Receivable_Item_Id + ")'><i class='fa fa-times' ></i></button>";

            htmlText += "</td>";

            htmlText += "</tr>";
        }
    }
    else {
        htmlText += "<tr>";

        htmlText += "<td colspan='3'> No Record found.";

        htmlText += "</td>";

        htmlText += "</tr>";
    }

    //$('#tblReceivableItems').find("tr:gt(0)").remove();
    $('#tblReceivableItems').html(htmlText);

    //$('.iradio-list').iCheck({
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    $("#divSearchGridOverlay").hide();

    ClearReceivableData();

    //$('[name="r1"]').on('ifChanged', function (event) {
    //    if ($(this).prop('checked')) {
    //        $("#hdnVendor_Id").val(this.id.replace("r1_", ""));
    //        $("#btnEdit").show();
    //        $("#btnAddProductMapping").show();
    //        $("#btnDelete").show();

    //    }
    //});

}

//function Save_Receivable_Data() {

//    var rViewModel = Set_Viewmodel_Data();

//    CallAjax("/Receivable/Insert-Receivable/", "json", JSON.stringify(rViewModel), "POST", "application/json", false, Bind_Receivable_Grid_Items, "", null);
//}

//function Set_Viewmodel_Data() {

//    var rViewModel =
//     {
//         InvoiceNo: $("#txtInvoiceNo").val(),

//         ReceivableDate: $("#txtRecDate").val(),

//         TransactionType: $("#drpTransaction").val(),

//         ChequeDate: $("#txtChequeDate").val(),

//         ChequeNo: $("#txtChequeNo").val(),

//         BankName: $("#txtBankName").val(),

//         IFSC_Code: $("#txtIFSCCode").val(),

//         NEFT: $("#txtNEFT").val(),

//         Credit_Debit_Card: $("#txtCredit_Debit").val()
//     }

//    return rViewModel;
//}

//function Bind_CTC_Policy(data) {

//    $("#tblPolicy").html("");
//    var htmlText = "";

//    $("#hdfCTCPolicyId").val(data.CTCPolicy.CTCPolicyId);

//    if (data.CTCPolicies.length > 0) {
//        htmlText += "<tr>";
//        htmlText += "<th>Salary Component</th>"
//        htmlText += "<th>Operands</th>"
//        htmlText += "<th>Value</th>"
//        htmlText += "<th>OF</th>"
//        htmlText += "<th>Metro/Non-Metro</th>"

//        htmlText += " </tr>"

//        for (var i = 0; i < data.CTCPolicies.length; i++) {

//            htmlText += "<tr id='CTCPo" + data.CTCPolicies[i].CTCTemplateID + "' >";

//            htmlText += "<td>" + data.CTCPolicies[i].SalaryComponent_Name + "</td>";

//            htmlText += "<td>" + data.CTCPolicies[i].Operands + "</td>";

//            htmlText += "<td>" + data.CTCPolicies[i].Value + "</td>";

//            htmlText += "<td>" + data.CTCPolicies[i].Metros + "</td>";

//            htmlText += "<td>" + data.CTCPolicies[i].Value + "</td>";

//            htmlText += "<td>" + data.CTCPolicies[i].Metros + "</td>";

//            htmlText += "<input type='hidden' class='Amount' value='" + data.CTCPolicies[i].SalaryComponent_ID + "'>";
//            htmlText += "<input type='hidden' class='TransactionId' value='" + operand + "'>";
//            htmlText += "<input type='hidden' class='BankName' value='" + data.CTCPolicies[i].Value + "'>";
//            htmlText += "<input type='hidden' class='IFSCCode' value='" + OF + "'>";
//            htmlText += "<input type='hidden' class='CHequeNo' value='" + Metro + "'>";
//            htmlText += "<input type='hidden' class='ChequeDate' value='" + data.CTCPolicies[i].CTCTemplateID + "'>";
//            htmlText += "<input type='hidden' class='NEFTDetails' value='" + data.CTCPolicies[i].CTCTemplateID + "'>";
//            htmlText += "<input type='hidden' class='CreditDebitDetails' value='" + data.CTCPolicies[i].CTCTemplateID + "'>";

//            htmlText += "<td>" + "<button type='button' id='btnEdit' class='btn btn btn-sm btn-Edit' onclick='Edit_CTC(" + data.CTCPolicies[i].CTCTemplateID + ")'><i class='fa fa-pencil-square-o'></i></button>" + "</td>";

//            htmlText += "<td>" + " <button type='button' id='btnRemove' class='btn btn-danger btn-xs btn-Remove' onclick='DeleteCTC(" + data.CTCPolicies[i].CTCTemplateID + ")'><i class='fa fa-remove'></i></button>" + "</td>";

//            htmlText += " </tr>";
//        }
//        $('#tblPolicy').html(htmlText);

//    }

//    $("#drpSalaryComponents").val(0);
//    $("#drpOperands").val(0);
//    $("#txtValue").val("0");
//    $("#drpOF").val(0);
//    $("#drpMetros").val(0);
//    $("#hdfCTCTemplateID").val("0");

//    $("#txtTemplateName").attr('readonly', 'readonly');

//    Friendly_Message(data);
//}



function EditReceivableData(id) {

    $("#drpTransaction").val($("#hdnTransaction_Type" + id).val());
    $("#txtReceivable_Item_Amount").val($("#hdnReceivable_Item_Amount" + id).val());
    $("#txtRecDate").val($("#hdnReceivable_Date" + id).val());
    $("#txtBankName").val($("#hdnBank_Name" + id).val());
    $("#txtIFSCCode").val($("#hdnIFSC_Code" + id).val());
    $("#txtChequeNo").val($("#hdnCheque_Number" + id).val());
    $("#txtChequeDate").val($("#hdnCheque_Date" + id).val());
    $("#txtNEFT").val($("#hdnNEFT" + id).val());
    $("#txtCredit_Debit").val($("#hdnCredit_Debit_Card" + id).val());
    $("#hdnReceivable_Item_Id").val($("#hdnReceivable_Item_Id" + id).val());
    $("#hdnReceivable_Id").val($("#hdnhdnReceivable_Id" + id).val());
    $('#drpTransaction').trigger('change');

}

function ClearReceivableData() {

    $("#drpTransaction").val(0);
    $("#txtReceivable_Item_Amount").val('');
    $("#txtRecDate").val('');
    $("#txtBankName").val('');
    $("#txtIFSCCode").val('');
    $("#txtChequeNo").val('');
    $("#txtChequeDate").val('');
    $("#txtNEFT").val('');
    $("#txtCredit_Debit").val('');

}

function DeletReceivableData(id) {


    $("#hdnReceivable_Item_Id").val($("#hdnReceivable_Item_Id" + id).val());
    $("#hdnReceivable_Id").val($("#hdnReceivable_Id" + id).val());


    var Receivable_Item_Id = $("#hdnReceivable_Item_Id").val();
    var Receivable_Id = $("#hdnReceivable_Id").val();

    $.ajax({
        url: '/Receivable/Delete_Receivable_Data_By_Id',
        data: { receivable_Item_Id: Receivable_Item_Id, receivable_Id: Receivable_Id },
        method: 'GET',
        async: false,
        success: function (data) {

            Bind_Receivable_Grid_Items(data);
            Friendly_Message(data);

        }
    });
}