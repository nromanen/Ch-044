$(document).ready(function () {
	var iframe = $('#IFrame1');
	iframe.on("load", function () {
		console.log("iframe loaded");
	});
			var iframeDoc = iframe.contents();
			$(iframeDoc)
			.mouseover(function (event) {
				$(event.target).addClass('outline-element');
			})
			.mouseout(function (event) {
				$(event.target).removeClass('outline-element');
			})
			.click(function (event) {
				$(event.target).toggleClass('outline-element-clicked');
				alert(getElementXpath(event.target));
			});
			});

		function getElementXpath(element) {
			return "//" + $(element).parents().andSelf().map(function () {
				var $this = $(this);
				var tagName = this.nodeName;

				if($this.siblings(tagName).length > 0) {
					tagName += "[" + ($this.prevAll(tagName).length + 1) + "]";
				}
					return tagName;
				}).get().join("/").toLowerCase();
			}