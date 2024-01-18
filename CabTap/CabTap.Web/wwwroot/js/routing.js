function initRoutingControl() {
    const routingControl = L.Routing.control({
        routeWhileDragging: true,
        show: false
    });

    routingControl.on('routesfound', onRouteFound);
    routingControl.on('routingerror', function (e) {
        console.error('Error calculating the route:', e.error);
    });

    return routingControl;
}

function calculateRoute(currentLat, currentLng, destinationLat, destinationLng) {
    resetDestination();

    console.log("Destination Lat:", destinationLat);
    console.log("Destination Lng:", destinationLng);

    routingControl.setWaypoints([
        L.latLng(currentLat, currentLng),
        L.latLng(destinationLat, destinationLng)
    ]).addTo(map);

    destinationSet = true;
}

function onRouteFound(e) {
    routes = e.routes;
    if (routes.length > 0) {
        toggleConfirmButton(true);
    }
}

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

function resetDestination() {
    if (destinationSet) {
        map.removeControl(routingControl);
        destinationSet = false;
        console.log("destination reset");

        toggleConfirmButton(false);
    }
}

function toggleConfirmButton(show) {
    const confirmButton = document.getElementById('confirmButton');
    confirmButton.style.display = show ? 'block' : 'none';
}