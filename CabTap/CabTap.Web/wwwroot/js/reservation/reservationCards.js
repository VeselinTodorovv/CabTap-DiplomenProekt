document.addEventListener('DOMContentLoaded', function () {
    const originInput = document.getElementById('origin');
    const destinationInput = document.getElementById('destination');
    const categoryCards = document.querySelectorAll('.category-card');
    const confirmRouteButton = document.getElementById('confirmRoute');

    categoryCards.forEach(card => {
        card.addEventListener('click', function () {
            categoryCards.forEach(card => {
                card.classList.remove('selected');
            });
            card.classList.add('selected');

            document.getElementById('categoryId').value = card.dataset.categoryId;
            document.getElementById('createButton').classList.remove('visually-hidden');
            document.getElementById('alert').classList.remove('visually-hidden');
        });
    });

    // Show category cards and hide the see prices button when it is clicked
    confirmRouteButton.addEventListener('click', function () {
        if (originInput.value === '' || destinationInput.value === '') {
            alert('Please enter origin and destination.');
            return;
        }
        
        document.getElementById('categoryCards').classList.remove('visually-hidden');
        this.style.display = 'none';
    });
});