// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
            logCoordinates(result.x, result.y);
        });
        dropdown.appendChild(item);
    });
}

function setInputValue(value, dropdown) {
    dropdown.parentElement.querySelector('input').value = value;
    dropdown.innerHTML = '';
}

function logCoordinates(x, y) {
    console.log('Selected location coordinates:', { x, y });
}