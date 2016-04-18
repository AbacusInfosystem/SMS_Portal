$(function () {


    $('input:not(.non-iCheck input:checkbox)').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional
    });

    function bindul() {
        var ID = "";
        var Value = "";
        $("[name='r1']").each(function () {
            if ($(this).parents('.icheckbox_square-green').prop('class').indexOf('checked') != -1) {

                var IID = $(this).prop('id').replace("r1_", "") + ",";
                var Iid = IID.split(".");
                ID = Iid[0] + "," + ID;

                var res = IID.split(".");
                //var idvalue = res[1] + ",";
                Value = res[1] + Value;

            }
        });

        // $("#hdnLookupLabelId").val(ID)
        $("#hdnId").val(ID)
        $("#hdnValue").val(Value)
        //$("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.todo-list').remove();
        $("#" + $("#hdnLookupHiddenId").val()).val($("#hdnId").val());
        //$("#" + $("#hdnLookupLabelId").val()).val("");
        var htmltext = "";
        var array = Value.split(',');
        var array1 = ID.split(',');
        var len = array.length;
        for (var i = 0; i < len - 1; i++) {
            htmltext = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + array[i] + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";
            $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').append(htmltext);
            $("#" + $("#hdnLookupLabelId").val()).val();
        }

        $("#" + $("#hdnLookupLabelId").val()).parents('.form-group').find('.fa-remove').click(function (event) {
            event.preventDefault();
            $(this).parents('.form-group').find('input[type=text]').val("");
            $("#" + $("#hdnLookupHiddenId").val()).val("");
            //$(this).parents('.form-group').find('.todo-list').remove();
            $(this).parents('li').remove();
        });

    }


    $("#User_Role_Id").change(function () {

        $.ajax({
            url: '/admin/get-userlist-basedon-rileid',
            data: { roleid: $("#User_Role_Id").val() },
            method: 'GET',
            async: false,
            success: function (data) {
                if (data != null) {
                    Bind_Userlist(data);
                }
            }
        });
    });

    $(".fa-remove").click(function (event) {
        event.preventDefault();
        $(this).parents('li').remove();
    });

});


