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
        showConfirmButton();
    }
}

function resetDestination() {
    if (destinationSet) {
        map.removeControl(routingControl);
        destinationSet = false;
        console.log("destination reset");

        hideConfirmButton();
    }
}

function showConfirmButton() {
    const confirmButton = document.getElementById('confirmButton');
    confirmButton.style.display = 'block';
}

function hideConfirmButton() {
    const confirmButton = document.getElementById('confirmButton');
    confirmButton.style.display = 'none';
}