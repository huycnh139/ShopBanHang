//
// Bars chart
//

var BarsChart = (function() {

	//
	// Variabless
	//

	var $chart = $('#chart-bars1');


	//
	// Methods
	//

	// Init chart
	function initChart($chart) {

		// Create chart
		var ordersChart = new Chart($chart, {
			type: 'bar',
			data: {
				labels: ['Jul', 'Aug1', 'Sep', 'Oct', 'Nov', 'Dec1'],
				datasets: [{
					label: 'Sales',
					data: [25, 20, 30, 22, 17, 2]
				}]
			}
		});

		// Save to jQuery object
		$chart.data('chart', ordersChart);
	}


	// Init chart
	if ($chart.length) {
		initChart($chart);
	}

})();
