function DrawChart (url) {
	var mb_one = url;
		var currentDate = new Date();
		var getYear = currentDate.getFullYear();
 
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
					fillColor: "rgba(24,188,156,0.2)",
					strokeColor: "rgba(24,188,156,1)",
					pointColor: "rgba(24,188,156,1)",
					pointStrokeColor: "#fff",
					pointHighlightFill: "#fff",
					pointHighlightStroke: "rgba(24,188,156,1)",
					data: aDatasets1
				}]
			};

			var ctx = $("#priceStat").get(0).getContext('2d');
			var lineChart = new Chart(ctx).Line(data, {
				bezierCurve: false
			});
		}
		function OnErrorCall_(repo) {
			console.log("error");
		}
	}
