const provider = new GeoSearch.OpenStreetMapProvider({
    params: {
        countrycodes: 'bg',
        'accept-language': 'bg'
    }
});

let originLat, originLng, destinationLat, destinationLng;

const originInput = document.getElementById('origin');
const destinationInput = document.getElementById('destination');
const originDropdown = document.getElementById('origin-dropdown');
const destinationDropdown = document.getElementById('destination-dropdown');

originInput.addEventListener('input', async () => {
    const results = await provider.search({ query: originInput.value });
    displayResults(results, originDropdown);
});

destinationInput.addEventListener('input', async () => {
    const results = await provider.search({ query: destinationInput.value });
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