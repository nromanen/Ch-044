/*Global vars for correct removing/updating/adding*/
var addNode = null;
var updateNode = null;
var deleteNode = null;
var addPropNode = null;
var updatePropNode = null;

/*Inizializtion sortable list*/
var group = null;
$(document).ready(function () {

    $(".properties_list").toggleClass("treemenu");
    $(".property_item").toggleClass("tree-empty");
    $(".property_item > .toggler").remove();
    $(".properties").hide();


    $(".adder").click(function (event) {
        addPropNode = $(this).parent();
        event.preventDefault();
        var catid = addPropNode.attr('data-id');
        console.log(catid);
        var url = 'AddProperty/?catid='+catid;
        window.location.href = url;
    });

    $(".togglebutt").unbind().click(function (event) {
        $(this).parent().children(".properties").toggle('slow');
        console.log("222");
    });


    $(".update_prop").click(function (event) {
        event.preventDefault();
        var catid = $(".update_prop").parent().parent().parent().parent().parent().attr('data-id');
        var propid = $(".update_prop").parent().parent().attr('property-id');
        var url = 'UpdateProperty?catid=' + catid+'&propid='+propid;
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
        pullPlaceholder: true,
        placeholder: '<li class="placeholdertrue"></li>',
        handle: 'button.handler'
    });
});

//Deleting Property
function DeleteProp() {

    var id = $('.property_item').attr('property-id');
    console.log(id);
    $("#ModalPropertyDelete #delete_prop_close").click();
    $(".property_item[property-id='" + id + "']").remove();
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
    buttonConfigElements.each(function () {
        $(this).click(function () {
            var idUpdatedCategory = $(this).parent().attr("data-id");
            var NameUpdatedCategory = $(this).parent().attr("data-name");
            $(this).parent().addClass("selected");
            updateNode = $(this).parent();
            deleteNode = $(this).parent();
            UpdateCategory(idUpdatedCategory, NameUpdatedCategory);
            RemoveCategory(idUpdatedCategory, NameUpdatedCategory);
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

    $(".adder").click(function (event) {
        addPropNode = $(this).parent();
        event.preventDefault();
        var catid = addPropNode.attr('data-id');
        console.log(catid);
        var url = 'AddProperty/?catid=' + catid;
        window.location.href = url;
    });

    $(".togglebutt").unbind().click(function (event) {
        $(this).parent().children(".properties").toggle('slow');
        console.log("222");
    });


}

//Delete Property button function
function DeleteProperty() 
{
    $("#ModalPropertyDelete").show();
};

function UpdateProperty(categoryid , propertyid) {
    var url = 'UpdateProperty/?catid=' + catid;
    window.location.href = url;
}

//correct filling a modal form for adding
function AddCategory(id, name) {
    $("#parentcategoryhidden").val(id);
    $("#ParentCategoryNameForm").html(name);
}

//little helper to main button Add Category+ .Just sets parent category of making category to main
function SetMainNode() {
    addNode = $(".serialization").parent();
}

//inserting new category: to db, to user interface
function InsertNode() {
    var node = addNode;
    var parentid = $("#parentcategoryhidden").val();
    var name = $("#namecategory").val();

    var id = -1;
    //puting in UI new list item
    $(node).children("ul").append("<li data-id=" + id + " data-name=" + name + "  class=\"tree-closed\">"
    + "<span class=\"toggler\"></span>"
    + "<b class=\"namecategory\">" + name + "</b>"
    + "<button class=\"btn handler transperent btn-sm glyphicon glyphicon-move text-muted\"></button>"
    + "<button class='btn adder transperent btn-sm glyphicon hidden glyphicon-plus-sign text-muted'></button>"
    + "<button class=\"btn transperent hidden btn-config \" data-toggle=\"modal\" data-target=\"#ModalUpdate\" ><small class=\"glyphicon glyphicon-cog\"></small></button>"
    + "<button class=\"btn transperent hidden btn-add \"      data-toggle=\"modal\" data-target=\"#ModalAdd\"    ><small class=\"glyphicon glyphicon-plus\"></small></button>"
    + "<button class=\"btn transperent hidden btn-remove \" data-toggle=\"modal\" data-target=\"#ModalDelete\" ><small class=\"glyphicon glyphicon-minus\" ></small></button>"
    + "<ul class=\"treemenu\"></ul></li>");
    addNode = $(node).children("ul").children("li:last");


    InsertAjax(parentid, name, id, node);


    $("#ModalAdd .close").click();
    //establishing events (open tree, close tree, hovers)
    InitializeEvents();
    DeleteTreeEvents();
    MakeHovers();
    $(".tree").treemenu({ delay: 300 }).openActive();

    var mainList = $(".serialization li");
    mainList.each(function () {
        if ($(this).hasClass("tree-opened")) {
            $(this).removeClass("tree-closed");
            $(this).children("ul").attr("style", "");
        }

    });

}


//concrete inserting to db using ajax
function InsertAjax(parentid, name, id, node) {
    $.ajax({
        type: "POST",
        url: 'AddCategory',
        data: ({ namecategory: name, parentcategory: parentid }),
        success: function (data) {
            console.log(data);
            addNode.attr("data-id", data);
        },
        error: function () {
            alert('Error occured');
        }
    });

}


function DeleteTreeEvents() {
    var mainList = $(".serialization li");
    mainList.each(function () {
        //$(this).removeClass("tree-opened");
        $(this).removeClass("tree-closed");

        $(this).children("span.toggler").each(function () {
            $(this).remove();
        });
        $(this).children("ul").removeClass("treemenu");
    });
}
//concrete updating category
function UpdateNode() {
    var node = updateNode;
    var name = $("#nameupdatedcategory").val();

    console.log(name);
    node.attr("data-name", name);
    node.children(".namecategory").html(name);
    $("#ModalUpdate .close").click();
    UpdateAjax(node.attr("data-id"), name);
}


//ajax query update db 
function UpdateAjax(id, name) {
    $.post('UpdateCategory', { namecategory: name, id: id }, function (data) {
    });
}

//concrete deleting category
function DeleteNode() {
    var id = deleteNode.attr("data-id");

    var childNodes = deleteNode.children("ul").children("li");
    //mobing children category of deleted category to next parent
    childNodes.each(function () {
        var node = $(this);
        deleteNode.parent().append(node.clone());
        $(this).remove();
    });



    deleteNode.remove();
    $("#ModalDelete .close").click();
    $("#ModalUpdate .close").click();

    //establishing events (open tree, close tree, hovers)
    InitializeEvents();
    DeleteTreeEvents();
    MakeHovers();
    $(".tree").treemenu({ delay: 300 }).openActive();

    var mainList = $(".serialization li");
    mainList.each(function () {
        if ($(this).hasClass("tree-opened")) {
            $(this).removeClass("tree-closed");
            $(this).children("ul").attr("style", "");
        }

    });
    //ajax query - removing from db
    $.post('RemoveCategory', { id: id }, function (data) {
    });
}

//puts in hidden field parent category id. invokes when items drop
function PutParentCategory() {
    var id = $("#addbutton").parent().parent().attr("data-id");
    $("#parentcategoryhidden").val(id);
}

$("#delete_prop_close").click(function () {

    $("#ModalPropertyDelete").hide();
});

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
function ChangeParent(id_item, id_parent) {
    $.post('ChangeParent', { categoryid: id_item, parentid: id_parent }, function (data) {
    });
}


//making hovers-buttons on categories
function MakeHovers() {
    var liElements = $(".serialization li");
    liElements.each(function () {
        var buttons = $(this).children("button.transperent");

        //using mouseenter&mouse lived due to lags on hover
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

