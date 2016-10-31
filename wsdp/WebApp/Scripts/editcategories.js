var addNode = null;
var updateNode = null;
var deleteNode = null;

var group = null;
$(document).ready(function () {
    $("ul.serialization ").sortable({
        group: 'serialization',
        onDrop: function ($item, container, _super) {
            $item.removeClass(container.group.options.draggedClass).removeAttr("style");
            $("body").removeClass(container.group.options.bodyClass);
            var itemid = $item.attr("data-id");
            var itemparent = $item.parent().parent().attr("data-id");
            ChangeParent(itemid, itemparent);
        },
        pullPlaceholder:true,
        placeholder:'<li class="placeholdertrue"></li>'
    });
});




    $(document).ready(function () {
        InitializeEvents();
        MakeHovers();
    });
function InitializeEvents() {

    var buttonConfigElements = $(".serialization li > button.btn-config");
    buttonConfigElements.each(function ()
    {
        $(this).click(function () {
            var idUpdatedCategory = $(this).parent().attr("data-id");
            var NameUpdatedCategory = $(this).parent().attr("data-name");
            RemoveSelectionsFromAllElements();
            $(this).parent().addClass("selected");
            updateNode = $(this).parent();
            UpdateCategory(idUpdatedCategory, NameUpdatedCategory);
        }
        );
    });


    var buttonAddElements = $(".serialization li > button.btn-add");
    buttonAddElements.each(function () {
        $(this).click(function () {
            var idParentCategory = $(this).parent().attr("data-id");
            var nameParentCategory = $(this).parent().attr("data-name");
            RemoveSelectionsFromAllElements();
            $(this).parent().addClass("selected");
            addNode = $(this).parent();
            AddCategory(idParentCategory, nameParentCategory);
        }
        );
    });

    var buttonDeleteElements = $(".serialization li > button.btn-remove");
    buttonDeleteElements.each(function () {
        $(this).click(function () {
            var idDeleteCategory = $(this).parent().attr("data-id");
            var nameDeleteCategory = $(this).parent().attr("data-name");
            RemoveSelectionsFromAllElements();
            deleteNode = $(this).parent();
            $(this).parent().addClass("selected");
            RemoveCategory(idDeleteCategory, nameDeleteCategory);
        });
    });

    var liElements = $(".serialization li");
    liElements.each(function () {
        $(this).mouseup(function () {
            var idCategory = $(this).attr("data-id");
        });
    });
}
function AddCategory(id, name) {
    /*if (!$("#updatecategory").hasClass("hidden")) {
        $("#updatecategory").toggleClass("hidden");
    }

    if (id == $("#parentcategoryhidden").val() &&  !$("#addcategory").hasClass("hidden"))
    {
        $("#addcategory").addClass("hidden");
        RemoveSelectionsFromAllElements();
    }

    else
        $("#addcategory").removeClass("hidden");*/

        

    $("#parentcategoryhidden").val(id);
    $("#ParentCategoryNameForm").html("to " + name);
}

function InsertNode(node, name, id)
{
    $(node).append("<li data-id=" + id + " data-name=" + name + "  >" + name + "<button class=\"btn transperent hidden btn-config btn-sm\"   data-toggle=\"modal\" data-target=\"#ModalUpdate\" ><small class=\"glyphicon glyphicon-cog\"></small></button>"
            + "<button class=\"btn transperent hidden btn-add btn-sm\"      data-toggle=\"modal\" data-target=\"#ModalAdd\"    ><small class=\"glyphicon glyphicon-plus\"></small></button>"
            + "<button class=\"btn transperent hidden btn-remove btn-sm\" data-toggle=\"modal\" data-target=\"#ModalDelete\" ><small class=\"glyphicon glyphicon-minus\" ></small></button>" + "</li>");
    InitializeEvents();
    MakeHovers();
}

function UpdateNode(node, name)
{
    $(node).attr("data-name").val(name);
    $(node).html(name + "<button class=\"btn transperent hidden btn-config btn-sm\"   data-toggle=\"modal\" data-target=\"#ModalUpdate\" ><small class=\"glyphicon glyphicon-cog\"></small></button>"
            + "<button class=\"btn transperent hidden btn-add btn-sm\"      data-toggle=\"modal\" data-target=\"#ModalAdd\"    ><small class=\"glyphicon glyphicon-plus\"></small></button>"
            + "<button class=\"btn transperent hidden btn-remove btn-sm\" data-toggle=\"modal\" data-target=\"#ModalDelete\" ><small class=\"glyphicon glyphicon-minus\" ></small></button>");
    InitializeEvents();
    MakeHovers();
}

function DeleteNode(node)
{
    $(node).remove();
}
function PutParentCategory() {
    var id = $("#addbutton").parent().parent().attr("data-id");
    $("#parentcategoryhidden").val(id);
}
function UpdateCategory(id, name) {
    /*if (!$("#addcategory").hasClass("hidden")) {
        $("#addcategory").toggleClass("hidden");
    }

    if (id == $("#updatedidhidden").val() && !$("#updatecategory").hasClass("hidden")) {
        $("#updatecategory").addClass("hidden");
        RemoveSelectionsFromAllElements();
    }

    else
        $("#updatecategory").removeClass("hidden");*/

        
        
    $("#CategoryNameForUpdate").html(name);
    $("#updatedidhidden").val(id);
    $("#removedidhidden").val(id);
}


function RemoveCategory(id, name) {
    $("#deletecategoryname").html(name);
    $("#deletecategoryidmodal").val(id);
}

function RemoveSelectionsFromAllElements()
{
    var liElements = $(".serialization li");
    liElements.each(function () {
        $(this).removeClass("selected");
    });
}

function ChangeParent(id_item, id_parent)
{
    $.post('ChangeParent',{categoryid:id_item, parentid:id_parent }, function(data) {
    });
}

function MakeFolders()
{
    var buttons = $(".serialization li button.arrow");

    buttons.each(function () {
        var liMain = $(this).parent();
        if (liMain.children("ul").html() == "")
        {
            $(this).addClass("hidden");
        }
        else
        {
            liMain.children("ul").addClass("hidden");
            $(this).click(function () {
                ShowFolder(liMain);
            });
        }
    });
}

function ShowFolder(node)
{
    node.children("ul").toggleClass("hidden");
    node.children(".arrow").children("span").toggleClass("glyphicon-chevron-right");
    node.children(".arrow").children("span").toggleClass("glyphicon-chevron-down");
}

function MakeHovers()
{
    var liElements = $(".serialization li");
    liElements.each(function () {
        var buttons = $(this).children("button.transperent");
        $(this).mouseenter(function () {
            buttons.each(function () {
                $(this).removeClass("hidden");
            });
        });
        
        $(this).mouseleave(function () {
            buttons.each(function () {
                $(this).addClass("hidden");
            });
        });

    });
}
 
