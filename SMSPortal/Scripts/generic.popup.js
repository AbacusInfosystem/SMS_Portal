$(function () {

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

    $(document).on("click", ".lookup-btn", function () {

        Get_Product();
    });

    $(document).on("focusout", ".lookup-label", function (event) {

        Get_Look_Up(false, $(this), false);
    });

    // CLICK EVENT OF CLASS WHICH IDENTIFIES SEARCH LOOKUPS.
    $(document).on("click", ".lookup-btn-search", function () {

        Get_Look_Up_Search(true, $(this));
    });

    $('#div_Parent_Modal_Fade').on('hidden.bs.modal', function (e) {
        // true when we need to reset cloned html back to its main div.
        // false when we are not dealing with any cloning. Eg: Normal Look up.
        // Currently I have hardcoded it to true, but we need to set some hidden flag to know whether its a cloned popup or normal look up.
        // IF IT IS A CLONED HTML
        if ($("#" + $('#div_Parent_Modal_Fade').find(".modal-title").attr("data-obj")).length) {

            // METHOD IS DEFINED IN GENERIC.POPUP.JS
            Close_Pop_Up(true,this);
        }
        else {

            Close_Pop_Up(false, this);
        }
    });

    $('#div_Child_Modal_Fade').on('hidden.bs.modal', function (e) {

        // true when we need to reset cloned html back to its main div.
        // false when we are not dealing with any cloning. Eg: Normal Look up.
        // Currently I have hardcoded it to true, but we need to set some hidden flag to know whether its a cloned popup or normal look up.
        //$('#div_Child_Modal_Fade').find(".modal-body").html("");

        //$('#div_Child_Modal_Fade').find(".modal-title").html("");

        Close_Pop_Up(false, this);
    });

});

function Prerequisite_For_Popup() {

    var model = "";

    //var parent_Model = $('#div_Parent_Modal_Fade').find(".modal-body");

    //$(parent_Model).load("/material-master/partial-material/", { fieldValue: fieldValue, fieldName: fieldName, table_Name: tableName, columns: columns });

    if ($('#div_Parent_Modal_Fade').hasClass("in")) 
    {
        model = "div_Child_Modal_Fade";

        $('#div_Child_Modal_Fade').find(".modal-title").html("");

        $('#div_Child_Modal_Fade').modal('toggle');

    }
    else {

        model = "div_Parent_Modal_Fade";

        $('#div_Parent_Modal_Fade').find(".modal-title").html("");

        $('#div_Parent_Modal_Fade').modal('toggle');
    }

    return model;
}

function Get_Look_Up(openModal, elementObj, modalExist) {

    // THIS IS THE TEXTBOX ELEMENT ON WHICH LOOKUP IS FIRED
    $("#hdnLookupLabelId").val(elementObj.parents(".lookup-parent").find(".lookup-label").prop("id"));

    // THIS IS THE HIDDEN CONTROL ON WHICH LOOKUP SELECTED VALUE IS TO BE STORED.
    $("#hdnLookupHiddenId").val(elementObj.parents(".lookup-parent").find(".lookup-hidden").prop("id"));

    // THIS IS ELEMENT ID NAME FROM WHERE VALUE SHOULD BE READ.
    var paramArray = $("#" + $("#hdnLookupLabelId").val()).data("param").split(',');

    // THIS IS SAP FIELD NAME WHICH IS USED IN WHERE CLAUSE WHILE FORMING A QUERY.
    var fieldsArray = $("#" + $("#hdnLookupLabelId").val()).data("field").split(',');

    // THIS IS THE SAP TABLE NAME WHICH IS USED IN FORMING A QUERY.
    var tableName = $("#" + $("#hdnLookupLabelId").val()).data("table");

    // THESE ARE SAP FIELD NAMES WHICH WOULD BE USED IN SELECT STATEMENT OF QUERY.
    var columns = $("#" + $("#hdnLookupLabelId").val()).data("col");

    var headerNames = $("#" + $("#hdnLookupLabelId").val()).data("headernames");

    var fieldValue = "";

    var fieldName = "";

    for (i = 0; i < paramArray.length; i++) {

        fieldValue += $("#" + paramArray[i]).val() + ",";

        fieldName += fieldsArray[i] + ",";
    }


    // TRUE, WHEN POPUP HAVE TO BE OPENED.
    // FALSE, WHEN ONFOCUSOUT EVENT IS TRIGGERED.

    if (openModal) {

        fieldValue = fieldValue.substring(0, fieldValue.length - 1);

        fieldName = fieldName.substring(0, fieldName.length - 1);

        var model = "";

        if (modalExist == false) {

            model = Prerequisite_For_Popup();

            $("#hdnModal").val(model);

            page = 0;
        }
        else {
            model = $("#hdnModal").val();

            page = $("#hdfCurrentPage").val();
        }        

        $("#" + model).find(".modal-body").load("/vendor-master/partial-vendor/", { fieldValue: fieldValue, fieldName: fieldName, table_Name: tableName, columns: columns, filterName: null, filterValue: "", filterDesc: null, headerNames: headerNames, page: page },
            function ()
            {
                //alert($("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find(".lookup-title").text() + " List");

                $("#" + model).find(".modal-title").text($("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find(".lookup-title").text() + " List");
    }
            );

        
    }
    else {

        var enteredValue = $("#" + $("#hdnLookupLabelId").val()).val();

        var paramId = $("#" + $("#hdnLookupLabelId").val()).data("byid");

        fieldValue += enteredValue;

        fieldName += paramId;

        if ($("#" + $("#hdnLookupLabelId").val()).val() != "") {

            $.ajax({

                url: '/vendor-master/get-lookup',

                data: { fieldValue: fieldValue, fieldName: fieldName, table_Name: tableName, columns: columns },

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

function Get_Look_Up_Search(openModal, elementObj) {
    // THIS IS THE TEXTBOX ELEMENT ON WHICH LOOKUP IS FIRED
    $("#hdnLookupLabelId").val(elementObj.parents(".lookup-parent").find(".lookup-label").prop("id"));

    // THIS IS THE HIDDEN CONTROL ON WHICH LOOKUP SELECTED VALUE IS TO BE STORED.
    $("#hdnLookupHiddenId").val(elementObj.parents(".lookup-parent").find(".lookup-hidden").prop("id"));

    // THESE ARE FILTERS WHICH WOULD BE USED TO SEARCH THE TABLE.
    var filterName = $("#" + $("#hdnLookupLabelId").val()).data("filter");

    if ($("#" + $("#hdnLookupLabelId").val()).data("filter") != undefined) {

        var filterArray = $("#" + $("#hdnLookupLabelId").val()).data("filter").split(',');
    }

    // THESE ARE THE VALUES OF THE FILTERS WHICH WOULD BE USED EXTRACTED FROM THE TEXTBOXES TO SEARCH THE TABLE.
    var filterValue = "";

    $(".filter-txt").each(function () {

        filterValue += $(this).val() + ",";

    });

    if (filterValue != "") {

        if (filterArray.length != 1) {
            filterValue = filterValue.substring(0, filterValue.length - 1);
        }
    }

    // THESE ARE THE DESCRIPTIONS OF THE FILTERS WHICH WOULD BE USED TO IDENTIFY THEM.
    var filterDesc = $("#" + $("#hdnLookupLabelId").val()).data("filterdesc");

    // TRUE, WHEN POPUP HAVE TO BE OPENED.
    // FALSE, WHEN ONFOCUSOUT EVENT IS TRIGGERED.

    if (openModal) {

        var model = "";

        model = Prerequisite_For_Popup();

        $("#hdnModal").val(model);

        $("#" + model).find(".modal-body").load("/vendor/partial-lookup-filter/", { filterName: filterName, filterValue: filterValue, filterDesc: filterDesc },
            function () {

                $("#" + model).find(".modal-title").text($("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find(".lookup-title").text() + " List");

                $("#hdnLookupText").val($("#hdnLookupLabelId").val());

                // VALIDATION OF FILTERS.
                $("#frmLookupSearch").validate();

                $('#btnLookupSearch').click(function () {

                    $("#hdfCurrentPage").val(0);

                    var param = $("#frmLookupSearch").find("div").prop("id");

                    var fields = $("#" + param).find(".filter-txt").length;

                    for (var i = 0; i < fields; i++) {

                        $("[name='Filter_" + i + "']").rules("add", { require_from_group: [1, ".filter-txt"] });
                    }

                    var textValue = $("#hdnLookupText").val();

                    Get_Look_Up_Filter(textValue);
                });

            }
        );

    }
    else {

        var enteredValue = $("#" + $("#hdnLookupLabelId").val()).val();

        var paramId = $("#" + $("#hdnLookupLabelId").val()).data("byid");

        fieldValue += enteredValue;

        fieldName += paramId;

        if ($("#" + $("#hdnLookupLabelId").val()).val() != "") {

            $.ajax({

                url: '/vendor-master/get-lookup',

                data: { fieldValue: fieldValue, fieldName: fieldName, table_Name: tableName, columns: columns },

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

            $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.todo-list').remove();
        }
    }
}


function Get_Look_Up_Filter(textValue) {

    // THIS IS ELEMENT ID NAME FROM WHERE VALUE SHOULD BE READ.        
    var paramArray = $("#" + textValue).data("param").split(',');

    // THIS IS SAP FIELD NAME WHICH IS USED IN WHERE CLAUSE WHILE FORMING A QUERY.
    var fieldsArray = $("#" + textValue).data("field").split(',');

    // THIS IS THE SAP TABLE NAME WHICH IS USED IN FORMING A QUERY.
    var tableName = $("#" + textValue).data("table");

    // THESE ARE SAP FIELD NAMES WHICH WOULD BE USED IN SELECT STATEMENT OF QUERY.
    var columns = $("#" + textValue).data("col");

    var fieldValue = "";

    var fieldName = "";

    for (i = 0; i < paramArray.length; i++) {

        fieldValue += $("#" + paramArray[i]).val() + ",";

        fieldName += fieldsArray[i] + ",";
    }

    // THESE ARE FILTERS WHICH WOULD BE USED TO SEARCH THE TABLE.
    var filterName = $("#" + textValue).data("filter");

    if ($("#" + textValue).data("filter") != undefined) {

        var filterArray = $("#" + textValue).data("filter").split(',');
    }

    var filterValue = "";

    $(".filter-txt").each(function () {

        filterValue += $(this).val() + ",";

    });

    if (filterValue != "") {

        if (filterArray.length != 1) {
            filterValue = filterValue.substring(0, filterValue.length - 1);
        }
    }

    // THESE ARE THE DESCRIPTIONS OF THE FILTERS WHICH WOULD BE USED TO IDENTIFY THEM.
    var filterDesc = $("#" + textValue).data("filterdesc");

    var headerNames = $("#" + $("#hdnLookupLabelId").val()).data("headernames");

    fieldValue = fieldValue.substring(0, fieldValue.length - 1);

    fieldName = fieldName.substring(0, fieldName.length - 1);

    // PAGINATION
    var page = $("#hdfCurrentPage").val();

    modal = $("#hdnModal").val();

    // LOADS SEARCH RESULTS IN DIV ELEMENT IF FORM IS VALID.
    if ($("#frmLookupSearch").valid()) {

        $("#" + modal).find(".modal-body").find("#dvLookupFilter").load("/vendor-master/partial-vendor/", { fieldValue: fieldValue, fieldName: fieldName, table_Name: tableName, columns: columns, filterName: filterName, filterValue: filterValue, filterDesc: filterDesc, headerNames: headerNames, page: page });
    }
}

function Bind_Selected_Item(data) {

    var htmltext = "";

    $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.todo-list').remove();

    if (data.Key != null) {

        $("#" + $("#hdnLookupHiddenId").val()).val($("#" + $("#hdnLookupLabelId").val()).val());

        htmltext = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + data.Key + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";
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
        
        // Set User input in value attriibute of the Control
        $(elementObj).find(".modal-body").find("input").each(function () {

            $(this).attr("value", $(this).val());

        });

        // To distroy the i-check animation
        $(elementObj).find(".modal-body").find("input").each(function () {

            $(this).iCheck('destroy');

        });

        obj.html($(elementObj).find(".modal-body").find("#" + $('#div_Parent_Modal_Fade').find(".modal-title").attr("data-obj")).html());
    }

    // RESET PAGE VALUE ON CLOSE OF MODAL.
    $("#hdfCurrentPage").val(0);

    $(elementObj).find(".modal-title").removeAttr("data-obj");

    $(elementObj).find(".modal-body").html("");

    $(elementObj).find(".modal-title").html("");
}

function Get_Product()
{
    $("#div_Parent_Modal_Fade").find(".modal-body").load("/Product/Get_Products", {}, call_back);
}

function call_back(data) {

    $('#div_Parent_Modal_Fade').modal('show');

    $("#div_Parent_Modal_Fade").find(".modal-title").text("Products");

}