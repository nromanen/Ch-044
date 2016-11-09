$(function () {
    //Init combobox
    $('.combobox').combobox();

    //Setting values of combobox and hide input with id
    var catid = $("#cat_id").attr('type', 'hidden').val();
    $("#category_id_select").val(catid).change();
    $("#category_id_select ").val(catid).prop('disabled', true);

    var cat_update_id = $("#cat_update_id").attr('type', 'hidden').val();
    $("#category_id_update_select").val(cat_update_id).change();
    $("#category_id_update_select ").val(cat_update_id).prop('disabled', true);

    var prop_update_id = $("#prop_update_id").attr('type', 'hidden').val();
    $("#prop_id_update_select").val(prop_update_id).change();
    $("#prop_id_update_select ").val(prop_update_id).prop('disabled', true);
})