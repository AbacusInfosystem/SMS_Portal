function SetValueToAutocomplete(hiddenTextValue, Textboxname)
{
    var htmlText = "<ul class='todo-list ui-sortable'><li ><span class='text'>" + hiddenTextValue + "</span><div class='tools'><i class='fa fa-remove'></i></div></li></ul>";

    $(Textboxname).parents('.form-group').append(htmlText);

    $(Textboxname).parents('.form-group').find('.fa-remove').click(function (event) {
        event.preventDefault();
        $(this).parents('.form-group').find('input[type=text]').val("");
        $(this).parents('.form-group').find('.auto-complete-value').val("");
        $(this).parents('.form-group').find('.auto-complete-label').val("");
        $(this).parents('.form-group').find('.auto-complete-value').trigger('change');
        $(this).parents('.form-group').find('.auto-complete-label').trigger('change');
        $(this).parents('.form-group').find('.todo-list').remove();
    });

}