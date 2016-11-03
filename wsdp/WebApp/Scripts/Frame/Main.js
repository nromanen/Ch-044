$(document).ready(function () {
	var iframe = $('#IFrame1');

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
		$('#clicked').val(getElementXpath(event.target));
	});

	$('input').click(function (event) {
		//delete all Id`s
		$('#clicked').attr('id', null);

		//Add new id
		$(this).attr('id', 'clicked');
	});
});

function getElementXpath(element) {
	return "//" + $(element).parents().andSelf().map(function () {
		var $this = $(this);
		var tagName = this.nodeName;

		if ($this.siblings(tagName).length > 0) {
			tagName += "[" + ($this.prevAll(tagName).length + 1) + "]";
		}
		return tagName;
	}).get().join("/").toLowerCase();
}