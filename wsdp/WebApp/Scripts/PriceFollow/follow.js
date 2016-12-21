$(document).ready(function () {
	$(".loader").hide();
});
//Function for following good;
function Follow()
{
	var good_Id = $("#btn_follow").data("good_id");
	var user_Id = $("#btn_follow").data("user_id");
	var price = $("#current-price").text();
	$.ajax({
		type: "POST",
		url: "/Good/FollowGoodPrice",
		data: { "good_Id": good_Id, "user_Id": user_Id,"price":price},
		beforeSend: function () {
			$(".glyphicon-refresh").removeClass("hidden");
		},
		complete: function () {
			$(".glyphicon-refresh").addClass("hidden");
		},
		success: function () {
			$("#txt_butt").text("Unfollow");
			$("#btn_follow").attr("onclick", "Unfollow()");
			$("#btn_follow").removeClass("btn-success");
			$("#btn_follow").addClass("btn-danger");

		},
		error: function () {
			console.log("error");
		},
	});
}
//function for Unfollowing good;
function Unfollow() {
	var good_Id = $("#btn_follow").data("good_id");
	var user_Id = $("#btn_follow").data("user_id");

	$.ajax({
		type: "POST",
		url: "/Good/DeleteGoodFollow",
		data: { "good_Id": good_Id, "user_Id": user_Id},
		beforeSend: function() {
			$(".glyphicon-refresh").removeClass("hidden");
		},
		complete: function() {
			$(".glyphicon-refresh").addClass("hidden");
		},
		success: function () {
			$("#txt_butt").text("Follow");
			$("#btn_follow").attr("onclick", "Follow()");
			$("#btn_follow").removeClass("btn-danger");
			$("#btn_follow").addClass("btn-success");
		},
		error: function () {
			console.log("error");
		},
	});
}
//Redirect to login if user not authificated
function Register_follow()
{
	window.location.href = '/Account/Login';
}