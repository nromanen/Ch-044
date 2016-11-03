$(function () {
    $('.combobox').combobox();
    var catid = $("#cat_id").attr('type', 'hidden').val();
    $("#category_id_select").val(catid).change();
    $("#category_id_select ").val(catid).prop('disabled', true);

    var catid = $("#cat_update_id").attr('type', 'hidden').val();
    $("#category_id_update_select").val(catid).change();
    $("#category_id_update_select ").val(catid).prop('disabled', true);

    var catid = $("#prop_update_id").attr('type', 'hidden').val();
    $("#prop_id_update_select").val(catid).change();
    $("#prop_id_update_select ").val(catid).prop('disabled', true);


})