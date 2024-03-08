document.addEventListener('DOMContentLoaded', function () {
    const categoryCards = document.querySelectorAll('.category-card');

    categoryCards.forEach(card => {
        card.addEventListener('click', function () {
            // Remove the 'selected' class from all category cards
            categoryCards.forEach(card => {
                card.classList.remove('selected');
            });

            // Add the 'selected' class to the clicked category card
            card.classList.add('selected');

            // Update the hidden input field with the selected category's ID
            document.getElementById('categoryId').value = card.dataset.categoryId;

            // Show the create button
            document.getElementById('createButton').style.display = 'block';
        });
    });

    // Show category cards and hide the see prices button when it is clicked
    document.getElementById('confirmRoute').addEventListener('click', function () {
        if (document.getElementById('origin').value === '' || document.getElementById('destination').value === '') {
            alert('Please enter origin and destination.');
            return;
        }
        
        document.getElementById('categoryCards').style.display = 'block';
        this.style.display = 'none';
    });
});