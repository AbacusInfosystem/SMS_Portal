﻿$(function () {

    $(document).on("click", ".fa-remove", function (event) {

    	
        event.preventDefault();
        
        if (!$(this).parents(".form-group").find(".form-control").is(":disabled"))
        {
        	$(this).parents('.form-group').find('input[type=text]').val("");

        	$(this).parents('.form-group').find('.lookup-hidden').val("");

        	$(this).parents('.form-group').find('.lookup-hidden').trigger("change");

        	$(this).parents('.form-group').find('.todo-list').remove();
        }
    });

    $(document).on("click", ".text-muted", function () {
        Get_Autocomplete_Lookup(true,$(this), false);
    });

    $(document).on("focusout", ".autocomplete-text", function (event) {

        Get_Autocomplete_Lookup(false, $(this), false);
    });

    $('#div_Parent_Modal_Fade').on('hidden.bs.modal', function (e) {

        if ($("#" + $('#div_Parent_Modal_Fade').find(".modal-title").attr("data-obj")).length) {

            Close_Pop_Up(true,this);
        }
        else {

            Close_Pop_Up(false, this);
        }
    });

    $('#div_Child_Modal_Fade').on('hidden.bs.modal', function (e) {

        Close_Pop_Up(false, this);
    });

});


function Get_Autocomplete_Lookup(openModal,elementObj, modalExist) {

    // THIS IS THE TEXTBOX ELEMENT ON WHICH LOOKUP IS FIRED
    $("#hdnLookupLabelId").val(elementObj.parents(".auto-complete").find(".autocomplete-text").prop("id"));

    // THIS IS THE HIDDEN CONTROL ON WHICH LOOKUP SELECTED VALUE IS TO BE STORED.
    $("#hdnLookupHiddenId").val(elementObj.parents(".auto-complete").find(".auto-complete-value").prop("id"));

    $("#hdnLookupHiddenValue").val(elementObj.parents(".auto-complete").find(".auto-complete-label").prop("id"));

    // THIS IS THE SAP TABLE NAME WHICH IS USED IN FORMING A QUERY.
    var tableName = $("#" + $("#hdnLookupLabelId").val()).data("table");

    var column = $("#" + $("#hdnLookupLabelId").val()).data("col");

    var headerNames = $("#" + $("#hdnLookupLabelId").val()).data("headernames");

    var model = "div_Parent_Modal_Fade";

    if (modalExist == false) {

        page = 0;
    }
    else {

        page = $("#hdfCurrentPage").val();
    }

    if (openModal) {

        alert(1);

        $("#" + model).find(".modal-body").load("/autocomplete/autocomplete-get-lookup-data/", { table_Name: tableName, columns: column, headerNames: headerNames, page: page },
            function () {

                $("#" + model).find(".modal-title").text($("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find(".lookup-title").text() + " List");

                $('#div_Parent_Modal_Fade').modal('toggle');
            }
            );
    }
    else
    {
        alert(2);

        var enteredValue = $("#" + $("#hdnLookupLabelId").val()).val();

        if ($("#" + $("#hdnLookupLabelId").val()).val() != "") {

            $.ajax({

                url: '/lookup/Lookup-get-lookup-data_by_id',

                data: { fieldValue: enteredValue,table_Name: tableName, columns: columns },

                method: 'GET',

                async: false,

                success: function (data) {

                    if (data != null) {

                        Bind_Selected_Item(data);
                    }
                }
            });
        }
        else {

            $("#" + $("#hdnLookupHiddenId").val()).val("");

            // added by shakti. I think if no record is not found, then this ul should also get removed.
            $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.todo-list').remove();
        }
    }
   
}

function Bind_Selected_Item(data) {

    var htmltext = "";

    $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.todo-list').remove();

    if (data.Value != null) {

        $("#" + $("#hdnLookupHiddenId").val()).val($("#" + $("#hdnLookupLabelId").val()).val());

        htmltext = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + data.Value + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";
    }
    else {

        $("#" + $("#hdnLookupHiddenId").val()).val("");

        htmltext = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find(".lookup-title").text() + " does not exist</span><div class='tools'><i class='fa fa-remove'></i>";
    }

    $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').append(htmltext);

    $("#" + $("#hdnLookupHiddenId").val()).trigger("change");
}

function Close_Pop_Up(cloneObj,elementObj) {

    if (cloneObj) {

        var obj = $("#" + $(elementObj).find(".modal-title").attr("data-obj"));

        $(elementObj).find(".modal-body").find("input").each(function () {

            $(this).attr("value", $(this).val());

        });

        $(elementObj).find(".modal-body").find("input").each(function () {

            $(this).iCheck('destroy');

        });

        obj.html($(elementObj).find(".modal-body").find("#" + $('#div_Parent_Modal_Fade').find(".modal-title").attr("data-obj")).html());
    }

    $("#hdfCurrentPage").val(0);

    $(elementObj).find(".modal-title").removeAttr("data-obj");

    $(elementObj).find(".modal-body").html("");

    $(elementObj).find(".modal-title").html("");
}

function call_back(data) {

    $('#div_Parent_Modal_Fade').modal('show');

    $("#div_Parent_Modal_Fade").find(".modal-title").text("Products");

}