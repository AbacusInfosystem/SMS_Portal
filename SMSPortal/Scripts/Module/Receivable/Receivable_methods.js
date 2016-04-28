 
function Save_Receivable_Data() {

    var rViewModel =
     {
         InvoiceNo: $("#txtInvoiceNo").val(),

         ReceivableDate: $("#txtRecDate").val(),

         TransactionType: $("#drpTransaction").val(),

         ChequeDate: $("#txtChequeDate").val(),

         ChequeNo: $("#txtChequeNo").val(),

         BankName: $("#txtBankName").val(),

         IFSC_Code: $("#txtIFSCCode").val(),

         NEFT: $("#txtNEFT").val(),

         Credit_Debit_Card: $("#txtCredit_Debit").val()
     }

    CallAjax("/Receivable/Insert-Receivable/", "json", JSON.stringify(rViewModel), "POST", "application/json", false, Bind_Receivable_Grid_Items, "", null);

}

function Bind_Receivable_Grid_Items(data) {

    $("#tblPolicy").html("");

    var htmlText = "";

    if (data.Receivables.length > 0) {

        for (i = 0; i < data.Receivables.length; i++) {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Receivables[i].Invoice_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

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

    $('#tblReceivableItems').find("tr:gt(0)").remove();
    $('#tblReceivableItems tr:first').after(htmlText);

    $('.iradio-list').iCheck({
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    $("#divSearchGridOverlay").hide();

    $('[name="r1"]').on('ifChanged', function (event) {
        if ($(this).prop('checked')) {
            $("#hdnVendor_Id").val(this.id.replace("r1_", ""));
            $("#btnEdit").show();
            $("#btnAddProductMapping").show();
            $("#btnDelete").show();

        }
    });

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

//function Edit_CTC(id) {
//    $("#drpSalaryComponents").val($("#CTCPo" + id).find(".Components").val());
//    $("#drpOperands").val($("#CTCPo" + id).find(".Operation").val());
//    $("#txtValue").val($("#CTCPo" + id).find(".Value").val());
//    $("#drpOF").val($("#CTCPo" + id).find(".OF").val());
//    $("#drpMetros").val($("#CTCPo" + id).find(".Metros").val());
//    $("#hdfCTCTemplateID").val($("#CTCPo" + id).find(".ID").val());
//    $("#hdfCTCPolicyId").val($("#CTCPo" + id).find(".TemplateID").val());

//}

//function DeleteCTC(id) {


//    $("#hdfCTCTemplateID").val($("#CTCPo" + id).find(".ID").val());

//    $("#hdfCTCPolicyId").val($("#CTCPo" + id).find(".TemplateID").val());


//    var TemplateId = $("#hdfCTCPolicyId").val();

//    //  alert(TemplateId);

//    $.ajax({
//        url: '/HRMS/Delete_Policy',
//        data: { CTCID: id, TemplateId: TemplateId },
//        method: 'GET',
//        async: false,
//        success: function (data) {

//            Bind_CTC_Policy(data);
//            Friendly_Message(data);
//            // $("#hdfCTCPolicyId").val(data.CTCPolicies.CTCPolicyId);
//        }
//    });
//}