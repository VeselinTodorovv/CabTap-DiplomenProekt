function showCategoryCards() {
    const distance = parseFloat(distanceInput.value);
    const categoryIds = Array.from(document.querySelectorAll('.category-card')).map(card => card.dataset.categoryId);

    categoryIds.forEach(categoryId => {
        updatePrices(categoryId, distance);
    });
}

function updatePrices(categoryId, distance) {
    $.get(getTotalPriceUrl, { categoryId: categoryId, distance: distance })
        .done(function(data) {
            $('#totalPrice-' + categoryId).text('$' + data.toFixed(2));
            document.getElementById('price').value = data.toFixed(2);
        })
        .fail(function(xhr, status, error) {
            console.error('Failed to get total price:', error);
        });
}