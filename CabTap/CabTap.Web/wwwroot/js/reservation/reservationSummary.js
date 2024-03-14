// Updates reservation details summary
$(document).ready(function() {
    $('#confirmRoute').click(function() {
        let origin = $('#origin').val();
        let destination = $('#destination').val();
        let passengersCount = $('#PassengersCount').val();
        let reservationType = $('input[name="reservationType"]:checked').val();
        let reservationDateTime = $('#ReservationDateTime').val();

        let reservationDetailsHtml = `
                    <p><strong>Origin:</strong> ${origin}</p>
                    <p><strong>Destination:</strong> ${destination}</p>
                    <p><strong>Passengers Count:</strong> ${passengersCount}</p>
                    <p><strong>Reservation Type:</strong> ${reservationType}</p>
                `;

        // If reservation type is scheduled, add scheduled date and time to reservation details
        if (reservationType === 'scheduled') {
            reservationDetailsHtml += `<p><strong>Scheduled Date and Time:</strong> ${reservationDateTime}</p>`;
        }

        $('#reservationDetails').html(reservationDetailsHtml);
    });
});