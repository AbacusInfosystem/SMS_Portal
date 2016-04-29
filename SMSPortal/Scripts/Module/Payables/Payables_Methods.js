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

    $("#tblPolicy").html("");

    var htmlText = "";

    if (data.Payables.length > 0) {

        for (i = 0; i < data.Payables.length; i++) {
            htmlText += "<tr>";

            htmlText += "<td>";

            htmlText += "<input type='radio' name='r1' id='r1_" + data.Payables[i].Invoice_Id + "' class='iradio-list'/>";

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Invoice_No == null ? "" : data.Payables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Invoice_No == null ? "" : data.Payables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Invoice_No == null ? "" : data.Payables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Receivables[i].Invoice_No == null ? "" : data.Receivables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Invoice_No == null ? "" : data.Payables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Invoice_No == null ? "" : data.Payables[i].Invoice_No;

            htmlText += "</td>";

            htmlText += "<td>";

            htmlText += data.Payables[i].Invoice_No == null ? "" : data.Payables[i].Invoice_No;

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

  

}