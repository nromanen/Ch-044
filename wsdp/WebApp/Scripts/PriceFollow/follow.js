
function OpenModal() {
	$("#EmailModal").show();
}
function Follow()
{
	var email = $('#email').val();
	var goodUrl = $('#goodUrl').val();

	$.ajax({
		type: "POST",
		url: "/Good/FollowGoodPrice",
		data: { "email": email, "goodUrl": goodUrl },
		success: function () {
		    alert('ok');
		},
		error: function () {
			console.log("pizdec");
		}
	});

}