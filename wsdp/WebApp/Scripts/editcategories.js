/*Global vars for correct removing/updating etc*/
var addNode = null;
var updateNode = null;
var deleteNode = null;

/*Inizializtion sortable list*/
var group = null;
$(document).ready(function () {
    $("#ModalPropertyUpdate .close")
        .click(function () {
            $("#ModalPropertyUpdate").hide();
            $("#ModalUpdate").show();
        });
    $("#remove_prop")
        .click(function() {
            $("#ModalUpdate").hide();
        });
    $("#remove_prop_dial")
        .click(function() {
            $("#ModalUpdate").show();
        });

    $("#add_prop").click(function (event) {
        event.preventDefault();
        var url = 'AddProperty';
        window.location.href = url;
    });
    $("#update_prop").click(function (event) {
        event.preventDefault();
        var url = 'UpdateProperty';
        window.location.href = url;
    });


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
        placeholder: '<li class="placeholdertrue"></li>',
        handle: 'small.handler'
    });
});

function DeleteProp() {

    var id = $('#id_prop_rm').val();
    $("#ModalPropertyUpdate .close").click();
    $("#ModalUpdate").show();

    $.post('RemoveProperty', { id: id }, function (data) {
    });
}

/*inizializtion of buttons*/
    $(document).ready(function () {
        InitializeEvents();
        MakeHovers();
    });

/*puting events for right-side buttons*/
function InitializeEvents() {
    //updating buttons
    var buttonConfigElements = $(".serialization li > button.btn-config");
    buttonConfigElements.each(function ()
    {
        $(this).click(function () {
            var idUpdatedCategory = $(this).parent().attr("data-id");
            var NameUpdatedCategory = $(this).parent().attr("data-name");
            $(this).parent().addClass("selected");
            updateNode = $(this).parent();
            UpdateCategory(idUpdatedCategory, NameUpdatedCategory);
        }
        );
    });

    //adding buttons
    var buttonAddElements = $(".serialization li > button.btn-add");
    buttonAddElements.each(function () {
        $(this).click(function () {
            var idParentCategory = $(this).parent().attr("data-id");
            var nameParentCategory = $(this).parent().attr("data-name");
            $(this).parent().addClass("selected");
            addNode = $(this).parent();
            AddCategory(idParentCategory, nameParentCategory);
        }
        );
    });

    //deleting buttons
    var buttonDeleteElements = $(".serialization li > button.btn-remove");
    buttonDeleteElements.each(function () {
        $(this).click(function () {
            var idDeleteCategory = $(this).parent().attr("data-id");
            var nameDeleteCategory = $(this).parent().attr("data-name");
            deleteNode = $(this).parent();
            $(this).parent().addClass("selected");
            RemoveCategory(idDeleteCategory, nameDeleteCategory);
        });
    });
}

//correct filling a modal form for adding
function AddCategory(id, name) {
    $("#parentcategoryhidden").val(id);
    $("#ParentCategoryNameForm").html("to " + name);
}

//little helper to main button Add Category+ .Just sets parent category of making category to main
function SetMainNode()
{
    addNode = $(".serialization").parent();
}

//inserting new category: to db, to user interface
function InsertNode()
{
    var node = addNode;
    var parentid = $("#parentcategoryhidden").val();
    var name = $("#namecategory").val();

    var id = -1;
    InsertAjax(parentid, name, id, node);
            
}


//concrete inserting to db using ajax
function InsertAjax(parentid, name, id, node)
{
    $.ajax({
        type: "POST",
        url: 'AddCategory',
        data: ({ namecategory: name, parentcategory: parentid }),
        success: function (data) {
            console.log(data);
            id = data;
            //will be fixed
            $(node).children("ul").append("<li data-id=" + id + " data-name=" + name + "  class=\"tree-closed\">"
            + "<span class=\"toggler\"></span>"
            +"<b class=\"namecategory\">" + name + "</b><button class=\"btn transperent hidden btn-config \" data-toggle=\"modal\" data-target=\"#ModalUpdate\" ><small class=\"glyphicon glyphicon-cog\"></small></button>"
            + "<button class=\"btn transperent hidden btn-add \"      data-toggle=\"modal\" data-target=\"#ModalAdd\"    ><small class=\"glyphicon glyphicon-plus\"></small></button>"
            + "<button class=\"btn transperent hidden btn-remove \" data-toggle=\"modal\" data-target=\"#ModalDelete\" ><small class=\"glyphicon glyphicon-minus\" ></small></button>"
            + "<ul class=\"treemenu\"></ul></li>");


            $(node).children("ul").children("li:last-child span.toggler").click(function () {

            });
            $("#ModalAdd .close").click();

            InitializeEvents();
            DeleteTreeEvents();
            MakeHovers();
            $(".tree").treemenu({ delay: 300 }).openActive();
        },
        async:false,
        error: function () {
            alert('Error occured');
        }
    });

}


function DeleteTreeEvents()
{
    var mainList = $(".serialization li");
    mainList.each(function () {
        $(this).removeClass("tree-opened");
        $(this).removeClass("tree-closed");

        $(this).children("span.toggler").each(function () {
            $(this).remove();
        });
        $(this).children("ul").removeClass("treemenu");
    });
}
//concrete updating category
function UpdateNode()
{
    var node = updateNode;
    var name = $("#nameupdatedcategory").val();

    console.log(name);
    node.attr("data-name", name);
    node.children(".namecategory").html(name);
    $("#ModalUpdate .close").click();
    UpdateAjax(node.attr("data-id"), name);
}


//ajax query update db 
function UpdateAjax(id, name)
{
    $.post('UpdateCategory', { namecategory: name, id: id }, function (data) {
    });
}

//concrete deleting category
function DeleteNode()
{
    var id = deleteNode.attr("data-id");
    deleteNode.remove();
    $("#ModalDelete .close").click();
    $.post('RemoveCategory', { id: id }, function (data) {
    });
}

//puts in hidden field parent category id. invokes when items drop
function PutParentCategory() {
    var id = $("#addbutton").parent().parent().attr("data-id");
    $("#parentcategoryhidden").val(id);
}

//correct filling update modal form 
function UpdateCategory(id, name) {    
    $("#CategoryNameForUpdate").html(name);
    $("#updatedidhidden").val(id);
    $("#removedidhidden").val(id);
}

//correct filling delete modal form 
function RemoveCategory(id, name) {
    $("#deletecategoryname").html(name);
    $("#deletecategoryidmodal").val(id);
}

//ajax query change parent. invokes, when items drop
function ChangeParent(id_item, id_parent)
{
    $.post('ChangeParent',{categoryid:id_item, parentid:id_parent }, function(data) {
    });
}


//making hovers-buttons on categories
function MakeHovers()
{
    var liElements = $(".serialization li");
    liElements.each(function () {
        var buttons = $(this).children("button.transperent");

        //using mouseenter&mouse lived due to lags hover
        $(this).mouseenter(function () {
            buttons.each(function () {
                $(this).removeClass("hidden");
            });

            var parentButtons = $(this).parent().parent().children("button.transperent");
            parentButtons.each(function () {
                $(this).addClass("hidden");
            });

        });
        
        $(this).mouseleave(function () {
            buttons.each(function () {
                $(this).addClass("hidden");
            });

            var parentButtons = $(this).parent().parent().children("button.transperent");
            parentButtons.each(function () {
                $(this).removeClass("hidden");
            });
        });

    });
}
 
