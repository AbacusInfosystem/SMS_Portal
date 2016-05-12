$(function () {


});

function Set_Mandetory_Validation(name) {
    $("[name='" + name + "']").rules("add", {
        required: true
    });
}

function Remove_Validations(name) {
    $("[name='" + name + "']").rules("remove");
}