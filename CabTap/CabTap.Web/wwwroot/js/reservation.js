document.addEventListener('DOMContentLoaded', function () {
    const provider = new GeoSearch.OpenStreetMapProvider({
        params: {
            countrycodes: 'bg'
        }
    });

    const originInput = document.getElementById('origin');
    const destinationInput = document.getElementById('destination');

    let durationInput = document.getElementById('duration');
    let distanceInput = document.getElementById('distance');

    let originLat, originLng, destinationLat, destinationLng;

    let originDropdown = document.getElementById('origin-dropdown');
    let destinationDropdown = document.getElementById('destination-dropdown');

    const confirmButton = document.getElementById('confirmRoute');
    confirmButton.addEventListener('click', calculateRoute);

    originInput.addEventListener('input', async () => {
        const results = await provider.search({query: originInput.value });
        console.log(results);
        displayResults(results, originDropdown);
    });

    destinationInput.addEventListener('input', async () => {
        const results = await provider.search({ query: destinationInput.value });
        console.log(results);
        displayResults(results, destinationDropdown);
    });

    function displayResults(results, dropdown) {
        dropdown.innerHTML = '';
        results.forEach(result => {
            const item = document.createElement('div');
            item.classList.add('autocomplete-dropdown-item');
            item.textContent = result.label;
            item.addEventListener('click', () => {
                setInputValue(result.label, dropdown);
                setCoordinates(result.x, result.y, dropdown.id === 'origin-dropdown');
            });
            dropdown.appendChild(item);
        });
    }

    function setInputValue(value, dropdown) {
        dropdown.parentElement.querySelector('input').value = value;
        dropdown.innerHTML = '';
    }

    function setCoordinates(x, y, isOrigin) {
        if (isOrigin) {
            originLat = y;
            originLng = x;
        } else {
            destinationLat = y;
            destinationLng = x;
        }
    }

    function calculateRoute() {
        if (!originLat || !originLng || !destinationLat || !destinationLng) {
            return;
        }
        
        const map = L.map(document.createElement('div'));

        L.Routing.control({
            waypoints: [
                L.latLng(originLat, originLng),
                L.latLng(destinationLat, destinationLng)
            ],
            routeWhileDragging: true,
            createMarker: function () { return null; },
        }).on('routesfound', function (e) {
            const routes = e.routes;
            if (routes && routes.length > 0) {
                const route = routes[0];
                const summary = route.summary;

                distanceInput.value = (summary.totalDistance / 1000).toFixed(2)
                durationInput.value = (summary.totalTime / 60).toFixed(2);
            }
        }).addTo(map);
    }
});