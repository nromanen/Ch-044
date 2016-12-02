var user_id = null;
var role_id = null;
var role_name = null;
var new_role = null;

//Init datatable
$(document).ready(function () {
	var oTable = $('#userstable').DataTable(
		{
			"processing": true,
			"serverSide": true,
			"searching": false,
			"ajax":
				{
					"url": "/Admin/LoadData",
					"type": "POST",
					"datatpye": "json"
				},
			"createdRow": function (row, data, index) {
				$(row).attr("data-user-id", data.Id);
			},
			"columns": [
				{ "data": "Id", "name": "Id", "autoWidth": true, "searchable": true },
				{ "data": "UserName", "name": "User Name", "autoWidth": true, "className": "username", "searchable": true },
				{ "data": "Password", "name": "Password", "autoWidth": true, "className": "password", "searchable": true },
				{ "data": "Email", "name": "Email", "autoWidth": true, "className": "email", "searchable": true },
				{ "data": "RoleName", "name": "Role", "autoWidth": true, "className": "role", "searchable": true },
				{
					"data": "RoleId", "name": "Action", "className": "action", "autoWidth": true, "render":
					function (data, type, full, meta) {
						return "Edit <div class='glyphicon glyphicon-edit update_user edit-user' data-role-id=" + data + "></div>";
					}
				}],
			"stateSave": true,

		});

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
	var userName = $("#userstable tr[data-user-id=" + user_id + "]").find("td").eq(1).html();
	var userPassword = $("#userstable tr[data-user-id=" + user_id + "]").find("td").eq(2).html();
	var userEmail = $("#userstable tr[data-user-id=" + user_id + "]").find("td").eq(3).html();
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
function UpdateUserAjax(id, userName, password, email, RoleId) {
	$.post('UpdateUser', { Id: id, UserName: userName, Password: password, Email: email, RoleId: RoleId }, function (data) {
		var hiddenpass = password.replace(password,"******");
		$("#userstable tr[data-user-id=" + user_id + "]").find(".username").html(userName);
		$("#userstable tr[data-user-id=" + user_id + "]").find(".password").html(hiddenpass);
		$("#userstable tr[data-user-id=" + user_id + "]").find(".email").html(email);
		$("#userstable tr[data-user-id=" + user_id + "]").find(".role").html(role_name);
	});
}