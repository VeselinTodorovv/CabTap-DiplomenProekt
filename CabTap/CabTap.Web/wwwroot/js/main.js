let currentLat, currentLng, destinationSet = false;

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