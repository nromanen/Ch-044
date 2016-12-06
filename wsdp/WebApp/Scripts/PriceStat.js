$(document).ready(function () {
	$("#btn_line_chart").on('click', function () {
		var mb_one = $("#ddl_one").val();
		var mb_two = $("#ddl_two").val();
		var getYear = $("#ddlYear").val();
 
		var jsonData = JSON.stringify({
			url_one: mb_one,
			url_two: mb_two,
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
			var aData = reponse.d;
			var aLabels = aData[0];
			var aDatasets1 = aData[1];
			var aDatasets2 = aData[2];

			var data = {
				labels: aLabels,
				datasets: [{
					label: "My First dataset",
					fillColor: "rgba(220,220,220,0.2)",
					strokeColor: "rgba(220,220,220,1)",
					pointColor: "rgba(220,220,220,1)",
					pointStrokeColor: "#fff",
					pointHighlightFill: "#fff",
					pointHighlightStroke: "rgba(220,220,220,1)",
					data: aDatasets1
				},
				{
					label: "My Second dataset",
					fillColor: "rgba(151,187,205,0.2)",
					strokeColor: "rgba(151,187,205,1)",
					pointColor: "rgba(151,187,205,1)",
					pointStrokeColor: "#fff",
					pointHighlightFill: "#fff",
					pointHighlightStroke: "rgba(151,187,205,1)",
					data: aDatasets2
				}]
			};

			var ctx = $("#priceStat").get(0).getContext('2d');
			ctx.canvas.height = 300;  // setting height of canvas
			ctx.canvas.width = 500; // setting width of canvas
			var lineChart = new Chart(ctx).Line(data, {
				bezierCurve: false
			});
		}
		function OnErrorCall_(repo) {
			alert("Woops something went wrong, pls try later !");
		}
	});
















	//var riceData = {
	//	labels: ["January", "February", "March", "April", "May", "June"],
	//	datasets:
	//	 [
	//		{
	//			fillColor: "rgba(172,194,132,0.4)",
	//			strokeColor: "#ACC26D",
	//			pointColor: "#fff",
	//			pointStrokeColor: "#9DB86D",
	//			data: [203000, 15600, 99000, 25100, 30500, 24700]
	//		}
	//	 ]
	//}

	//var rice = document.getElementById('priceStat').getContext('2d');
	//new Chart(rice).Line(riceData);
});