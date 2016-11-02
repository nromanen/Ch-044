$(document).load(function () {
    $('.combobox').combobox();
    var catid = $("#cat_id").val();
    $("#Category_Id option[value=" + catid + "]").prop('selected', true);
    $("#add_prop").click(function () {
        alert(catid);
    });
});