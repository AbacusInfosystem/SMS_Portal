function Save_Payable_Data() {
    alert(1);
    var pViewModel =
     {
         Payable:
             {

                 Invoice_Id: $("#hdnInvoiceId").val(),

                 Invoice_Amount: $("#txtInvoice_Amount").val(),

                 Payable_Item_Amount: $("#txtPayable_Item_Amount").val(),

                 Payable_Date: $("#txtPayDate").val(),

                 Transaction_Type: $("#drpTransaction").val(),

                 Cheque_Date: $("#txtChequeDate").val(),

                 Cheque_Number: $("#txtChequeNo").val(),

                 Bank_Name: $("#txtBankName").val(),

                 IFSC_Code: $("#txtIFSCCode").val(),

                 NEFT: $("#txtNEFT").val(),

                 Credit_Debit_Card: $("#txtCredit_Debit").val(),

                 Payable_Id: $("#hdnPayable_Id").val(),

                 Payable_Item_Id: $("#hdnPayable_Item_Id").val()
             }
     }

    CallAjax("/Payables/Insert_Payable/", "json", JSON.stringify(pViewModel), "POST", "application/json", false, Bind_Payable_Grid_Items, "", null);

}

function Bind_Payable_Grid_Items(data) {
    alert(20);
    $("#tblPayableItems").html("");

    var htmlText = "";

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

    $("#txtInvoiceNo").val(data.Payable.Invoice_Id),

    $("#hdnInvoiceId").val(data.Payable.Invoice_Id),

    $("#txtInvoice_Amount").val(data.Payable.Invoice_Amount)

    $("#dvInvoice").find(".autocomplete-text").trigger("focusout");

    if (data.Payables.length > 0) {

        for (i = 0; i < data.Payables.length; i++) {

            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='hidden' id='hdnTransaction_Type" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].Transaction_Type + "'/>";

            htmlText += "<input type='hidden' id='hdnPayable_Item_Amount" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].Payable_Item_Amount + "'/>";

            htmlText += "<input type='hidden' id='hdnBank_Name" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].Bank_Name + "'/>";

            htmlText += "<input type='hidden' id='hdnIFSC_Code" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].IFSC_Code + "'/>";

            htmlText += "<input type='hidden' id='hdnCheque_Number" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].Cheque_Number + "'/>";

            htmlText += "<input type='hidden' id='hdnCheque_Date" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].Cheque_Date + "'/>";

            htmlText += "<input type='hidden' id='hdnNEFT" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].NEFT + "'/>";

            htmlText += "<input type='hidden' id='hdnCredit_Debit_Card" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].Credit_Debit_Card + "'/>";

            htmlText += "<input type='hidden' id='hdnReceivable_Date" + data.Payables[i].Payable_Item_Id + "' value='" + data.Payables[i].Payable_Date + "'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Receivable_Item_Amount == null ? "" : data.Payables[i].Payable_Item_Amount;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Transaction_Type == null ? "" : data.Payables[i].Transaction_Type;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Bank_Name == null ? "" : data.Payables[i].Bank_Name;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].IFSC_Code == null ? "" : data.Payables[i].IFSC_Code;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Cheque_Number == null ? "" : data.Payables[i].Cheque_Number;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Cheque_Date == null ? "" : data.Payables[i].Cheque_Date;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].NEFT == null ? "" : data.Payables[i].NEFT;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Credit_Debit_Card == null ? "" : data.Payables[i].Credit_Debit_Card;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Receivable_Date == null ? "" : data.Payables[i].Receivable_Date;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += "<button type='button' id='edit-Payable-details' class='btn btn-box-tool btn-tel-edit' onclick='javascript:EditPayableData(" + data.Payables[i].Payable_Item_Id + ")'><i class='fa fa-pencil' ></i></button>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += "<button type='button' id='delete-Payable-details' class='btn btn-box-tool btn-tel-delete' onclick='javascript:DeletePayableData(" + data.Payables[i].Payable_Item_Id + ")'><i class='fa fa-times' ></i></button>";

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
    $('#tblPayableItems').html(htmlText);

    //$('.iradio-list').iCheck({
    //    radioClass: 'iradio_square-green',
    //    increaseArea: '20%' // optional
    //});

    $("#divSearchGridOverlay").hide();

    ClearPayableData();

    //$('[name="r1"]').on('ifChanged', function (event) {
    //    if ($(this).prop('checked')) {
    //        $("#hdnVendor_Id").val(this.id.replace("r1_", ""));
    //        $("#btnEdit").show();
    //        $("#btnAddProductMapping").show();
    //        $("#btnDelete").show();

    //    }
    //});

}

function EditPayableData(id) {

    $("#drpTransaction").val($("#hdnTransaction_Type" + id).val());
    $("#txtPayable_Item_Amount").val($("#hdnPayable_Item_Amount" + id).val());
    $("#txtPayDate").val($("#hdnPayable_Date" + id).val());
    $("#txtBankName").val($("#hdnBank_Name" + id).val());
    $("#txtIFSCCode").val($("#hdnIFSC_Code" + id).val());
    $("#txtChequeNo").val($("#hdnCheque_Number" + id).val());
    $("#txtChequeDate").val($("#hdnCheque_Date" + id).val());
    $("#txtNEFT").val($("#hdnNEFT" + id).val());
    $("#txtCredit_Debit").val($("#hdnCredit_Debit_Card" + id).val());
    $("#hdnPayable_Item_Id").val($("#hdnPayable_Item_Id" + id).val());
    $("#hdnPayable_Id").val($("#hdnPayable_Id" + id).val());
    $('#drpTransaction').trigger('change');

}

function ClearPayableData() {

    $("#drpTransaction").val(0);
    $("#txtPayable_Item_Amount").val('');
    $("#txtPayDate").val('');
    $("#txtBankName").val('');
    $("#txtIFSCCode").val('');
    $("#txtChequeNo").val('');
    $("#txtChequeDate").val('');
    $("#txtNEFT").val('');
    $("#txtCredit_Debit").val('');

}

function DeletePayableData(id) {


    $("#hdnPayable_Item_Id").val($("#hdnPayable_Item_Id" + id).val());
    $("#hdnPayable_Id").val($("#hdnhdnPayable_Id" + id).val());


    var Payable_Item_Id = $("#hdnPayable_Item_Id").val();

    $.ajax({
        url: '/Payable/Delete_Policy',
        data: { Payable_Item_Id: Payable_Item_Id, Payable_Id: Payable_Id },
        method: 'GET',
        async: false,
        success: function (data) {

            Bind_Payable_Grid_Items(data);
            Friendly_Message(data);

        }
    });
}