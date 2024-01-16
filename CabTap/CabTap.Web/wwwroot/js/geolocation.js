function initMap() {
    const map = L.map('map', {
        minZoom: MIN_ZOOM_LEVEL
    });

    map.locate({ setView: true, maxZoom: MAX_ZOOM_LEVEL });

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {}).addTo(map);

    map.on('click', (e) => onMapClick(e));
    map.on('geosearch/showlocation', (e) => destinationSelected(e));

    return map;
}

function onMapClick(e) {
    const { lat, lng } = e.latlng;
    calculateRoute(currentLat, currentLng, lat, lng);
}

function destinationSelected(e) {
    const { y, x } = e.location;
    calculateRoute(currentLat, currentLng, y, x);
}

function geolocationSuccess(position) {
    const crd = position.coords;
    currentLat = crd.latitude;
    currentLng = crd.longitude;

    console.log(`Current Location: ${currentLat} ${currentLng}`);
    addMarker(currentLat, currentLng, 'You are here');
}

function addMarker(lat, lng, popupContent) {
    const marker = L.marker([lat, lng]).addTo(map);
    marker.bindPopup(`<b>${popupContent}</b>`).openPopup();
}