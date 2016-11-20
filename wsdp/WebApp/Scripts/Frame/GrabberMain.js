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

		if (sibling === element) return getPathTo(element.parentNode) + '/' + element.tagName.toLowerCase() + '[' + (ix + 1) + ']';

		if (sibling.nodeType === 1 && sibling.tagName === element.tagName) {
			ix++;
		}
	}
}
$(document).ready(function () {
	setTimeout(function () {
		EstablishingEvents();
		console.log("EV");
	}, 10000);
});

function EstablishingEvents() {
	var iframe = document.getElementById("iframe1");
	iframe = iframe.contentWindow.document;
	$(iframe).select("a").click(function (event) {
		event.preventDefault();
	});
	$(iframe)
	.mouseover(function (event) {
		$(event.target).addClass('outline-element');
	})
	.mouseout(function (event) {
		$(event.target).removeClass('outline-element');
	})
	.click(function (event) {
		$(event.target).toggleClass('outline-element-clicked');
	})
	.click(function (event) {
		if (event === undefined) event = window.event;                     // IE hack
		var target = 'target' in event ? event.target : event.srcElement; // another IE hack

		var root = document.compatMode === 'CSS1Compat' ? document.documentElement : document.body;

		var path = getPathTo(target);
		var message = path;
		$("#clicked").val(getPathTo(event.target));
	});

	$('input').click(function (event) {
		//delete all Id`s
		$('#clicked').attr('id', null);

		//Add new id
		$(this).attr('id', 'clicked');
	});

	$('textarea').click(function (event) {
		//delete all Id`s
		$('#clicked').attr('id', null);

		//Add new id
		$(this).attr('id', 'clicked');
	});
}
