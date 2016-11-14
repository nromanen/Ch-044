var user_id = null;
var role_id = null;
var role_name = null;
var new_role = null;

//Init datatable
$(document).ready(function () {
    $('#example').DataTable();

    $(".close").click(function () {
        $("#ModalUser").hide();
    });
});

//Showing ModalRole modal window and setting values of inputs
$(document).on('click', '.edit-user', function () {
    role_id = $(this).data("role-id");
    user_id = $(this).closest('tr').data("user-id");
    $("#data-role-id").val(role_id);
    $("#ModalUser").show();
    $("#role_update").val(role_id).change();
    var userName = $("#example tr[data-user-id=" + user_id + "]").find("td").eq(1).html();
    var userPassword = $("#example tr[data-user-id=" + user_id + "]").find("td").eq(2).html();
    var userEmail = $("#example tr[data-user-id=" + user_id + "]").find("td").eq(3).html();
    $("#ModalUser .user-name:first").val(userName);
    $("#ModalUser .user-pass:first").val(userPassword);
    $("#ModalUser .user-email:first").val(userEmail);
});

//Updating User
function UpdateUser() {
    if ($(".field-validation-error").length) {
        console.log("Validation error.")
    }
    else {
        var username = $("#ModalUser .user-name:first").val();
        var password = $("#ModalUser .user-pass:first").val();
        var email = $("#ModalUser .user-email:first").val();
        var role_id = $("#role_update option:selected").val();
        role_name = $("#role_update option:selected").text();
        var u_id = user_id;
        UpdateUserAjax(u_id, username, password, email, role_id);
        $(".close").click();
    }
}

//ajax query update user
function UpdateUserAjax(id,userName,password,email,RoleId) {
    $.post('UpdateUser', { Id: id, UserName: userName, Password: password, Email: email, RoleId: RoleId }, function (data) {
        $("#example tr[data-user-id=" + user_id + "]").find("#username").html(userName);
        $("#example tr[data-user-id=" + user_id + "]").find("#password").html(password);
        $("#example tr[data-user-id=" + user_id + "]").find("#email").html(email);
        $("#example tr[data-user-id=" + user_id + "]").find("#role").html(role_name);
    });
}