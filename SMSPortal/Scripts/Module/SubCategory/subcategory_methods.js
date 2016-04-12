

function Get_Screen_Groups(module_Id)
{
    $("#tdModule_" + module_Id).load("/user-management/get-role-screen-group-by-module/" + module_Id + "/" + $("#hdfRole_Id").val() + "/", null, function () { callback(module_Id) });
}

function Reset_Screen_Groups(module_Id)
{
    $("#tdModule_" + module_Id).html("");
}

function callback(module_Id)
{
    $("#tdModule_" + module_Id).find(".valid-screen-group").rules("add", { is_valid_screen_group: true });
}

function Save_Role()
{
    var html = "";

    html += "<input type='hidden' name = 'Role.Role_Id' value='" + $("#hdfRole_Id").val() + "'>";

    html += "<input type='hidden' name = 'Role.Role_Name' value='" + $("#txtRole_Name").val() + "'>";

    var count = 0;

    var count2 = 0;

    $(".access").each(function () {

       
        if ($(this).val() == "true") {


            html += "<input type='hidden' name = 'Role.Role_Module_List[" + count + "].Role_Module_Id' value='" + $(this).parents(".tr-role-module").find(".role-module").val() + "'>";

            html += "<input type='hidden' name = 'Role.Role_Module_List[" + count + "].Role_Id' value='" + $("#hdfRole_Id").val() + "'>";

            html += "<input type='hidden' name = 'Role.Role_Module_List[" + count + "].Module_Id' value='" + $(this).parents(".tr-role-module").find(".module").val() + "'>";

            html += "<input type='hidden' name = 'Role.Role_Module_List[" + count + "].Is_Access' value='" + $(this).parents(".tr-role-module").find(".access").val() + "'>";

            html += "<input type='hidden' name = 'Role.Role_Module_List[" + count + "].Is_Delete' value='" + $(this).parents(".tr-role-module").find(".chkdelete").val() + "'>";

            html += "<input type='hidden' name = 'Role.Role_Module_List[" + count + "].Is_Approve' value='" + $(this).parents(".tr-role-module").find(".chkapprove").val() + "'>";

            $("#tdModule_" + $(this).parents(".tr-role-module").find(".module").val()).find(".screen-access").each(function () {

                if($(this).val() == "true")
                {

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Role_Id' value='" + $("#hdfRole_Id").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Module_Id' value='" + $(this).parents(".tr-screen-group").find(".screen-module").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Screen_Group_Id' value='" + $(this).parents(".tr-screen-group").find(".screen-group").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Is_Access' value='" + $(this).parents(".tr-screen-group").find(".screen-access").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Is_Create' value='" + $(this).parents(".tr-screen-group").find(".screen-create").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Create_T_Code' value='" + $(this).parents(".tr-screen-group").find(".screen-create-code").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Is_Change' value='" + $(this).parents(".tr-screen-group").find(".screen-change").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Change_T_Code' value='" + $(this).parents(".tr-screen-group").find(".screen-change-code").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Is_View' value='" + $(this).parents(".tr-screen-group").find(".screen-view").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].View_T_Code' value='" + $(this).parents(".tr-screen-group").find(".screen-view-code").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Is_Request_View' value='" + $(this).parents(".tr-screen-group").find(".screen-request-view").val() + "'>";

                    html += "<input type='hidden' name = 'Role.Role_Screen_Group_List[" + count2 + "].Is_Request_Change' value='" + $(this).parents(".tr-screen-group").find(".screen-request-change").val() + "'>";

                    count2++;
                }
            });

            count++;
        }
    });

    $("form").append(html);
}

function Set_Checkbox_Values()
{
    $('.screen-access').each(function () {

        if ($(this).parent().prop("class").indexOf("checked") != -1) {

            $(this).val(true);
        }
        else {
            $(this).val(false);
        }

    });

    $('.screen-create').each(function () {

        if ($(this).parent().prop("class").indexOf("checked") != -1) {

            $(this).val(true);
        }
        else {
            $(this).val(false);
        }

    });

    $('.screen-change').each(function () {

        if ($(this).parent().prop("class").indexOf("checked") != -1) {

            $(this).val(true);
        }
        else {
            $(this).val(false);
        }

    });

    $('.screen-view').each(function () {

        if ($(this).parent().prop("class").indexOf("checked") != -1) {

            $(this).val(true);
        }
        else {
            $(this).val(false);
        }

    });
}