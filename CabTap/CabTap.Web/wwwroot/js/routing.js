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
    const routes = e.routes;
    if (routes.length > 0) {
        const routeSummary = routes[0].summary;
        logRouteSummary(routeSummary);
    }
}

function logRouteSummary(routeSummary) {
    let distance = (routeSummary.totalDistance / 1000).toFixed(2);
    let duration = (routeSummary.totalTime / 60).toFixed(2);

    const unitDistance = distance > 1 ? "Distance: " + distance + " km" : "Distance: " + distance * 1000 + " m";
    const unitDuration = duration > 60 ? "ETA: " + (duration / 60).toFixed(2) + " hours" : "ETA: " + duration + " minutes";

    console.log(unitDistance);
    console.log(unitDuration);
}

function resetDestination() {
    if (destinationSet) {
        map.removeControl(routingControl);
        destinationSet = false;
        console.log("destination reset");
    }
}