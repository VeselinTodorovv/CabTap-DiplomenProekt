// Updates reservation details summary
$(document).ready(function() {
    $('#confirmRoute').click(function() {
        let origin = $('#origin').val();
        let destination = $('#destination').val();
        let passengersCount = $('#PassengersCount').val();

        let reservationDetailsHtml = `
                    <p><strong>Origin:</strong> ${origin}</p>
                    <p><strong>Destination:</strong> ${destination}</p>
                    <p><strong>Passengers Count:</strong> ${passengersCount}</p>
                `;

        $('#reservationDetails').html(reservationDetailsHtml);
    });
});