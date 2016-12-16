
function OpenModal() {
	$("#EmailModal").show();
}
function Follow()
{
	var good_Id = $("#btn_follow").data("good_id");
	var user_Id = $("#btn_follow").data("user_id");
	$("#btn_follow").addClass('active');

	$.ajax({
		type: "POST",
		url: "/Good/FollowGoodPrice",
		data: { "good_Id": good_Id, "user_Id": user_Id },
		success: function () {
			$("#btn_follow").text("Unfollow");
			$("#btn_follow").attr("onclick", "Unfollow()");
			$("#btn_follow").removeClass('active');
		},
		error: function () {
			console.log("error");
		}
	});
}
function Unfollow() {
	var good_Id = $("#btn_follow").data("good_id");
	var user_Id = $("#btn_follow").data("user_id");
	$("#btn_follow").addClass('active');

	$.ajax({
		type: "POST",
		url: "/Good/DeleteGoodFollow",
		data: { "good_Id": good_Id, "user_Id": user_Id },
		success: function () {
			$("#btn_follow").text("Follow");
			$("#btn_follow").attr("onclick", "Follow()");
			$("#btn_follow").removeClass('active');
		},
		error: function () {
			console.log("error");
		}
	});
}
function Register_follow()
{
	window.location.href = '/Account/SignUp';
}