var current = 0;
var ListItems = JSON.parse($('#items').attr('data-list-items'));
var length = ListItems.length;

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
    InputSettings();
    $(".body-content").removeClass("container");
    $(".body-content").addClass("container-fluid");

    CheckButtons();
    setTimeout(function () {
        EstablishingEvents();
        console.log("EV");
    }, 10000);

});

function GetAllXpath() {
	var inputs = $(".inputfield");
	console.log(inputs);
	inputs.each(function () {
		var url = ListItems[current];
		var xpath = $(this).val();
		console.log(xpath);
		InsertaaAjax($(this), url, xpath);
	});
}
function InsertaaAjax(input, url, xpath) {
	$.ajax({
		type: "POST",
		url: '../GetPreview',
		data: ({ url: url, xpath: xpath }),
		success: function (data) {
			console.log(data);
			input.siblings("b").html(data);
		},
		error: function () {
			alert('Error occured');
		}
	});
}

function NextAjax()
{
	$.ajax({
		type: "POST",
		url: '../Next',
		data: ({ current: current}),
		success: function (data) {
			$('#iframe1').attr('src', data);
			$("#progress_bar").hide();
			CheckButtons();
			setTimeout(function () {
				EstablishingEvents();
				console.log("EV");
			}, 10000);
		},
		error: function () {
			alert('Error occured');
			$("#progress_bar").hide();
		}
	});
}

function PreviousAjax() {
	$.ajax({
		type: "POST",
		url: '../Previous',
		data: ({ current: current }),
		success: function (data) {
			$('#iframe1').attr('src', data);
			$("#progress_bar").addClass("hidden");
			CheckButtons();
			setTimeout(function () {
				EstablishingEvents();
				console.log("EV");
			}, 10000);
		},
		error: function () {
			alert('Error occured');
			$("#progress_bar").addClass("hidden");
		}
	});
}

function EstablishingEvents() {
    var iframe = document.getElementById("iframe1");
    iframe = iframe.contentWindow.document;

    $(iframe)
	.mouseout(function (event) {
		event.target.style.border = "";
	})
	.mouseover(function (event) {
		if (event.shiftKey) {
			event.target.style.border = "2px red solid";
			$("#clicked").val(getPathTo(event.target));
		}
	}
);
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
    $(".list-inline-item:nth-child(3)").addClass("active");


    $('#Next').unbind().click(function () {
        if (current != length - 1) {
            current++;
        };
		$("#progress_bar").removeClass("hidden");
		NextAjax();
		
        CheckButtons();
        setTimeout(function () {
        	EstablishingEvents();
        	$(".text-danger").html("");
            console.log("EV");
        }, 10000);

    });

    $('#Previous').unbind().click(function () {
        if (current != 0) {
            current--;
        };
		PreviousAjax();
        CheckButtons();
        setTimeout(function () {
            EstablishingEvents();
            console.log("EV");
        }, 10000);

    });

};
function CheckButtons() {
    if (current == length - 1) {
        $('#Next').attr('disabled', true);
    }
    else {
        $('#Next').attr('disabled', false);
    }
    if (current == 0) {
        $('#Previous').attr('disabled', true);
    }
    else { $('#Previous').attr('disabled', false); }
}

function AddField(btn) {
    var name = $(btn).closest(".form-group").find('input:last').attr('name');

    var input = document.createElement("input");
    input.setAttribute("class", "form-control inputfield");
    input.setAttribute("placeholder", "Click here and get Xpath");
    input.setAttribute("style", "margin-top:10px;");
    input.setAttribute("name", name);
    input.setAttribute("required", "required");

    $(btn).closest(".form-group").children(".fields").append(input);

    var br = document.createElement("br");
    var b = document.createElement("b");

    $(btn).closest(".form-group").children(".fields").append(br);
    $(btn).closest(".form-group").children(".fields").append(b);
    EstablishingEvents();
};

function InputSettings() {
    var inputs = $(".fields input:last");

    for (var i = 0; i < inputs.length; i++) {
        var inp = inputs[i];
        var name = inp.getAttribute("name");
        var pos = name.lastIndexOf("[");
        name = name.substring(0, pos);

        inp.setAttribute("name", name);
    }
};