$(document).ready(function() {
    $('input[name="reservationType"]').change(function() {
        if ($(this).val() === "scheduled") {
            $('#scheduledReservationFields').show();
        } else {
            $('#scheduledReservationFields').hide();
        }
    });
});