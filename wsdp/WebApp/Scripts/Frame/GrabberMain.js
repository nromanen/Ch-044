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

    $(".body-content").removeClass("container");
    $(".body-content").addClass("container-fluid");

    CheckButtons();
    setTimeout(function () {
        EstablishingEvents();
        console.log("EV");
    }, 10000);

});

function EstablishingEvents() {
    var iframe = document.getElementById("iframe1");
    iframe = iframe.contentWindow.document;

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
	.mouseover(function (event) {
	    if (event.shiftKey) {
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
        $('#iframe1').attr('src', ListItems[current]);
        CheckButtons();
        setTimeout(function () {
            EstablishingEvents();
            console.log("EV");
        }, 10000);

    });

    $('#Previous').unbind().click(function () {
        if (current != 0) {
            current--;
        };
        $('#iframe1').attr('src', ListItems[current]);
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
    input.setAttribute("class", "form-control");
    input.setAttribute("placeholder", "Click here and get Xpath");
    input.setAttribute("style", "margin-top:10px;");
    input.setAttribute("name", name);

    $(btn).closest(".form-group").children(".fields").append(input);

    EstablishingEvents();
};