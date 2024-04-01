$(document).ready(function() {
    $('input[name="ReservationType"]').change(function() {
        if ($(this).val() === "Scheduled") {
            $('#scheduledReservationFields').show();
        } else {
            $('#scheduledReservationFields').hide();
        }
    });
});