$(function () {
    $(".list-inline-item:nth-child(2)").addClass("active");

    enableUrlSearch();

    $('.button2').click(function () {
        enableUrlSearch();
    });

    $('.button1').click(function () {
        enableXpathSearch();
    });
});

function enableUrlSearch() {
    $("#XPathPageIterator").attr("disabled", "disabled");

    $("#UrlMask").removeAttr("disabled");
    $("#From").removeAttr("disabled");
    $("#To").removeAttr("disabled");
};
function enableXpathSearch() {
    $("#XPathPageIterator").removeAttr("disabled");

    $("#UrlMask").attr("disabled", "disabled");
    $("#From").attr("disabled", "disabled");
    $("#To").attr("disabled", "disabled");

};

function AddField(btn) {
    var name = $(btn).closest(".form-group").find('input').attr('name');

    var input = document.createElement("input");
    input.setAttribute("class", "form-control");
    input.setAttribute("placeholder", "xpath1, xpath2...");
    input.setAttribute("style", "margin-top:10px;");
    input.setAttribute("name", name);
    input.setAttribute("required", "required");

    document.getElementById("fields").appendChild(input);

    EstablishingEvents();
}
$(document).ready(function () {
    

    var inputs = $("#fields input");

    for (var i = 0; i < inputs.length; i++)
    {
        var inp = inputs[i];
        var name = inp.getAttribute("name");
        var pos = name.indexOf("[");
        name = name.substring(0, pos);

        inp.setAttribute("name", name);
    }
});