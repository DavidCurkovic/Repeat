// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
function onlyUnique(value, index, self) {
	return self.indexOf(value) === index;
}
//Get cities and push them into array
$(document).ready(function () {
	let cites = [];
	let events = $('.js-grid').children();
	for (let i = 0; i < events.length; i++) {
		let el = $(events[i]);
		let city = el.data('city');
		cites.push(city);
	}
	let uniqueCities = cites.filter(onlyUnique);
	for (i = 0; i < uniqueCities.length; i++) {
		$('.filter_city__js').append('<option>' + uniqueCities[i] + '</option>');
	}
});
// Display cities 
$('.filter_city__js').on('change', function () {
	let selectedCity = $(this).val();
	let events = $('.js-grid').children();
	for (let i = 0; i < events.length; i++) {
		let el = $(events[i]);
		let city = el.data('city');
		if (city == selectedCity || selectedCity == '') {
			el.show();
		} else {
			el.hide();
		}
	}
});
// Initialize and add the map
$(document).ready(function InitMap() {
	// The location of Uluru
	let clubs = document.getElementById('club_details__map');
	let lat = parseFloat(clubs.getAttribute('latitude'));
	let long = parseFloat(clubs.getAttribute('longitude'));
	const uluru = {
		lat: lat,
		lng: long
	};
	// The map, centered at Uluru
	const map = new google.maps.Map(document.getElementById('club_details__map'), {
		zoom: 12,
		center: uluru,
	});
	// The marker, positioned at Uluru
	const marker = new google.maps.Marker({
		position: uluru,
		map: map,
	});
});