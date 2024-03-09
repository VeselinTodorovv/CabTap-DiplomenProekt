document.addEventListener('DOMContentLoaded', function () {
    const originInput = document.getElementById('origin');
    const destinationInput = document.getElementById('destination');
    const categoryCards = document.querySelectorAll('.category-card');
    const categoryIdInput = document.getElementById('categoryId');
    const createButton = document.getElementById('createButton');
    const confirmRouteButton = document.getElementById('confirmRoute');

    categoryCards.forEach(card => {
        card.addEventListener('click', function () {
            categoryCards.forEach(card => {
                card.classList.remove('selected');
            });
            card.classList.add('selected');

            categoryIdInput.value = card.dataset.categoryId;
            createButton.style.display = 'block';
        });
    });

    // Show category cards and hide the see prices button when it is clicked
    confirmRouteButton.addEventListener('click', function () {
        if (originInput.value === '' || destinationInput.value === '') {
            alert('Please enter origin and destination.');
            return;
        }
        
        document.getElementById('categoryCards').className = 'col-md-12 mb-4';
        this.style.display = 'none';
    });
});