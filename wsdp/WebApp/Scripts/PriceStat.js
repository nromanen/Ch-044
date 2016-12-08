$(document).ready(function () {
	$("#btn_line_chart").on('click', function () {
		var mb_one = $("#ddl_one").val();
		var currentdate = new Date();
		var getYear = currentdate.getFullYear();
 
		var jsonData = JSON.stringify({
			url_one: mb_one,
			year: getYear
		});

		$.ajax({
			type: "POST",
			url: "/Home/getLineChartData",
			data: jsonData,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: OnSuccess_,
			error: OnErrorCall_
		});

		function OnSuccess_(reponse) {
			var aData = reponse;
			var aLabels = aData[0];
			var aDatasets1 = aData[1];

			var data = {
				labels: aLabels,
				datasets: [{
					label: "My dataset",
					fillColor: "rgba(0,255,0,0.2)",
					strokeColor: "rgba(0,255,0,1)",
					pointColor: "rgba(0,255,0,1)",
					pointStrokeColor: "#fff",
					pointHighlightFill: "#fff",
					pointHighlightStroke: "rgba(0,255,0,1)",
					data: aDatasets1
				}]
			};

			var ctx = $("#priceStat").get(0).getContext('2d');
			ctx.canvas.height = 400;  // setting height of canvas
			ctx.canvas.width = 800; // setting width of canvas
			var lineChart = new Chart(ctx).Line(data, {
				bezierCurve: false
			});
		}
		function OnErrorCall_(repo) {
			console.log("error");
		}
	});

});