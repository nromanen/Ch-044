function getPathTo(element) {
	if (element.id !== '') {
		if (element.id == undefined) return "";
		return "//" + element.tagName.toLowerCase() + "[@id='" + element.id + "']";
	}

	if (element === document.body)
		return element.tagName.toLowerCase();

	var ix = 0;
	var siblings = element.parentNode.childNodes;
	for (var i = 0; i < siblings.length; i++) {
		var sibling = siblings[i];

		if (sibling === element) return getPathTo(element.parentNode) + '/' + element.tagName.toLowerCase();

		if (sibling.nodeType === 1 && sibling.tagName === element.tagName) {
			ix++;
		}
	}
}
$(document).ready(function () {

	$(".body-content").removeClass("container");
	$(".body-content").addClass("container-fluid");

	setTimeout(function () {
		EstablishingEvents();
		console.log("EV");
	}, 10000);
});

function EstablishingEvents() {
	var iframe = document.getElementById("iframe1");

	iframe = iframe.contentWindow.document;

	$(iframe)
   .mouseout(function (event) {
   	event.target.style.border = "";
   })
	.mouseover(function (event) {
		if (event.shiftKey) {
			if ($("#clicked").hasClass("urlmask")) {
				event.target.style.border = "2px red solid";
				$("#clicked").val(event.target.getAttribute("href"));

			}
			else {
				event.target.style.border = "2px red solid";
				$("#clicked").val(getPathTo(event.target));
			}
		}
	});
	$('input').click(function (event) {
		//delete all Id`s
		$('#clicked').attr('id', null);

		//Add new id
		$(this).attr('id', 'clicked');
	});
}