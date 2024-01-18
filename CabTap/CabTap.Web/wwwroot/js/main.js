let currentLat, currentLng, destinationSet = false;
let routes, unitDistance, unitDuration;

const map = initMap();
const routingControl = initRoutingControl();

if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(geolocationSuccess, function (e) {
        console.error("Geolocation error:", e.message);
    }, GEOLOCATION_OPTIONS);
}

const provider = new GeoSearch.OpenStreetMapProvider(PROVIDER_OPTIONS);
const search = new GeoSearch.GeoSearchControl({
    provider: provider,
    style: 'bar',
    autoComplete: true,
    autoCompleteDelay: 250,
    showMarker: false
});

map.addControl(search);

const confirmButton = document.getElementById('confirmRoute');
confirmButton.addEventListener('click', sendRouteData);

// Will only log the data in the console for now
function sendRouteData() {
    // TODO: Implement remaining services, so you can send this data to the controller
    const routeSummary = routes[0].summary;  
    let distance = (routeSummary.totalDistance / 1000).toFixed(2);
    let duration = (routeSummary.totalTime / 60).toFixed(2);

    unitDistance = distance > 1 ? "Distance: " + distance + " km" : "Distance: " + distance * 1000 + " m";
    unitDuration = duration > 60 ? "ETA: " + (duration / 60).toFixed(2) + " hours" : "ETA: " + duration + " minutes";

    console.log(unitDistance);
    console.log(unitDuration);
}
