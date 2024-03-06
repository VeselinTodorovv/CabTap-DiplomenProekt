document.addEventListener('DOMContentLoaded', function () {
    const provider = new GeoSearch.OpenStreetMapProvider({
        params: {
            countrycodes: 'bg'
        }
    });

    const originInput = document.getElementById('origin');
    const destinationInput = document.getElementById('destination');

    let originDropdown = document.getElementById('origin-dropdown');
    let destinationDropdown = document.getElementById('destination-dropdown');

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
                logCoordinates(result.x, result.y, dropdown.id === 'origin-dropdown');
            });
            dropdown.appendChild(item);
        });
    }

    function setInputValue(value, dropdown) {
        dropdown.parentElement.querySelector('input').value = value;
        dropdown.innerHTML = '';
    }

    function logCoordinates(x, y, isOrigin) {
        const originLatInput = document.getElementById('origin-lat');
        const originLngInput = document.getElementById('origin-lng');
        const destinationLatInput = document.getElementById('destination-lat');
        const destinationLngInput = document.getElementById('destination-lng');

        // Assign the values to the corresponding input fields based on the isOrigin parameter
        if (isOrigin) {
            originLatInput.value = x;
            originLngInput.value = y;
        } else {
            destinationLatInput.value = x;
            destinationLngInput.value = y;
        }

        console.log(originLatInput.value);
        console.log(originLngInput.value);
        console.log(destinationLatInput.value);
        console.log(destinationLngInput.value);
    }
});