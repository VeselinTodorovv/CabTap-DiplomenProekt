const durationInput = document.getElementById('duration');
const distanceInput = document.getElementById('distance');

const originPointInput = document.getElementById('originPoint');
const destinationPointInput = document.getElementById('destinationPoint');

const confirmButton = document.getElementById('confirmRoute');
confirmButton.addEventListener('click', calculateRoute);

function calculateRoute() {
    if (!originLat || !originLng || !destinationLat || !destinationLng) {
        return;
    }

    const map = L.map(document.createElement('div'));
    map.setView([originLat, originLng], 13);
    L.Routing.control({
        waypoints: [
            L.latLng(originLat, originLng),
            L.latLng(destinationLat, destinationLng)
        ],
        createMarker: function () { return null; },
    }).on('routesfound', function (e) {
        const routes = e.routes;
        if (routes && routes.length > 0) {
            const route = routes[0];
            const summary = route.summary;

            distanceInput.value = (summary.totalDistance / 1000).toFixed(2);
            durationInput.value = (summary.totalTime / 60).toFixed(2);

            const routeOrigin = route.waypoints[0].latLng;
            const routeDestination = route.waypoints[route.waypoints.length - 1].latLng;
            
            originPointInput.value = `${routeOrigin.lat},${routeOrigin.lng}`;
            destinationPointInput.value = `${routeDestination.lat},${routeDestination.lng}`;
            
            showCategoryCards();
        }
    }).addTo(map);
}