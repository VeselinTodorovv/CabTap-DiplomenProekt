document.addEventListener('DOMContentLoaded', function () {
    const categoryCards = document.querySelectorAll('.category-card');

    categoryCards.forEach(card => {
        card.addEventListener('click', function () {
            categoryCards.forEach(card => {
                card.classList.remove('selected');
            });
            card.classList.add('selected');

            document.getElementById('categoryId').value = card.dataset.categoryId;
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