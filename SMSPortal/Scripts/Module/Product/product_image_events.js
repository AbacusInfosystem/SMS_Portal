$(function ()
{
    $("#btnAddImageAttachments").click(function ()
    {
    	
    	var div = document.createElement('div');

    	var len = $("#dvImageAttachments").find(".row").length;
    	 
    	div.className = "row";

    	div.innerHTML = document.getElementById("dumpImageAttachment").innerHTML;

    	$(div).find("input").each(function(){
    		$(this).attr("name","productimage");
    	});

    	if (len < 5)
    	{
    	    $("#dvImageAttachments").append(div);
    	}

    	$("#dvImageAttachments").find("[name='productimage']").last().iCheck({
    	    checkboxClass: 'icheckbox_square-green',
    	    radioClass: 'iradio_square-green',
    	    increaseArea: '20%' // optional
    	});
    });

    $(document).on("click", ".remove-image-attachment", function ()
    {
    	if ($(this).closest(".row").find('.address-attachment-id').length > 0)
    	{
    		var attachment_Ids = $('[name=""]').val();

    		if (attachment_Ids != "")
    		{
    			$('[name=""]').val(attachment_Ids + "," + $(this).closest(".row").find('.address-attachment-id').val());
    		}
    		else
    		{
    			$('[name=""]').val($(this).closest(".row").find('.address-attachment-id').val());
    		}
    	}

    	$(this).closest(".row").remove();

    	var len = 0;

    	$("#dvImageAttachments").find(".row").each(function ()
    	{
    		$(this).find("input").each(function ()
    		{
    			$(this).attr("name", $(this).prop("name").split("[")[0] + "[" + len + "]"+ $(this).prop("name").split("]")[1]);
    		});

    		len++;
    	});

    	var le = $("#dvImageAttachments").find(".row").length;

    	if (le ==0)
    	{
    	    $("#btnAddImageAttachments").trigger("click");

    		if ($("#hdnVendor_Type_Id").val() == 1)
    		{
    			$("").rules("add", { required: true });
    		}
    	}
    });

});

$(document).ready(function () {

    $('#dvImageAttachments').find("[name='productimage']").iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

});