$(function ()
{

    $("#btnSave").click(function () {

        if ($('#frmTax').valid())
        {
            if ($("#hdnTax_Id").val() == 0)
            {
                $("#frmTax").attr("action", "/tax/insert-tax");
            }
            else
            {
                $("#frmTax").attr("action", "/tax/edit-tax/");
            }

            $('#frmTax').attr("method", "POST");

            $('#frmTax').submit();
        }
    });
});
