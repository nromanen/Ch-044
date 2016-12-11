$(document).ready(function () {
    setTimeout(function myfunction() {
        UpdateExecuingInfoAjax();
    }, 1000);
});

function UpdateExecuingInfoAjax()
{
    //$.ajax({
    //    type: "GET",
    //    url: 'GetExecutingInfo',
    //    dataType: "json",
    //    success: function (data) {
    //        console.log(data);
    //        setTimeout(function myfunction() {
    //            UpdateExecuingInfoAjax();
    //        }, 1000);
    //    },
    //    error: function () {
    //        console.log('Error occured');
    //    }
    //});

    $.getJSON("GetExecutingInfo", function (data) {
        console.log(data.taskinfoes[0]);
        $("#executinginfoes").html("");
        taskinfoes = data.taskinfoes;
        for (var i = 0; i < taskinfoes.length; i++) {
            var date = eval(taskinfoes[i].Date.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
            $("#executinginfoes").append(CreateExecutingNode(taskinfoes[i].ParserTaskId, taskinfoes[i].GoodUrl, date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds()));
        }
        setTimeout(function () {
            UpdateExecuingInfoAjax();
        }, 1000);
    });
}

function CreateExecutingNode(parsertaskid, url, date)
{
    return $('<div class="row"><div class="col-xs-2"><b>' + parsertaskid + '</b></div><div class="col-xs-8"><span class="text-muted">' + url + '</span></div><div class="col-xs-2"><span class="text-muted">' + date + '</span></div></div>');
}
function ConfigureIntervals() {
    var pull = $("#pull").val();
    var push = $("#push").val();

    $.ajax({
        type:"POST",
        url: "/UniversalParser/ConfigureIntervals",
        data: { "pull": pull, "push": push },
        success: function () {
            alert("Data was changed");
        },
        error: function(){
            alert("Smth wrong");
    }
    });
}