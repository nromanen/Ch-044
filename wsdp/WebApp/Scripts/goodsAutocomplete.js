$(document).ready(function() {
    $("#searchbox").autocomplete({
        source: function(request, response) {
            $.ajax({
                url: "/Home/GetExactGoods",
                type: "POST",
                dataType: "json",
                data: { name: request.term },
                success: function(data) {
                    response($.map(data, function(item) {
                        return { label: item.Name, value: item.Name };
                    }));

                }
            });
        },
        messages: {
            noResults: "",
            results: ""
        }
    });
});