function toggleChart(chartId) {
    let chart = document.getElementById(chartId);
    if (chart.style.display === 'none') {
        chart.style.display = 'block';
    } else {
        chart.style.display = 'none';
    }
}

// Initialize charts
let clientsChartCtx = document.getElementById('clientsChart').getContext('2d');
let driversChartCtx = document.getElementById('driversChart').getContext('2d');
let taxisChartCtx = document.getElementById('taxisChart').getContext('2d');
let reservationsCountChartCtx = document.getElementById('reservationsCountChart').getContext('2d');
let reservationsSumChartCtx = document.getElementById('reservationsSumChart').getContext('2d');

let clientsChart = new Chart(clientsChartCtx, {
    type: 'bar',
    data: {
        labels: ['Clients'],
        datasets: [{
            label: 'Count',
            data: [clientsData],
            backgroundColor: 'rgba(54, 162, 235, 0.2)',
            borderColor: 'rgba(54, 162, 235, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

let driversChart = new Chart(driversChartCtx, {
    type: 'bar',
    data: {
        labels: ['Drivers'],
        datasets: [{
            label: 'Count',
            data: [driversData],
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

let taxisChart = new Chart(taxisChartCtx, {
    type: 'bar',
    data: {
        labels: ['Taxis'],
        datasets: [{
            label: 'Count',
            data: [taxisData],
            backgroundColor: 'rgba(255, 206, 86, 0.2)',
            borderColor: 'rgba(255, 206, 86, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

let reservationsCountChart = new Chart(reservationsCountChartCtx, {
    type: 'bar',
    data: {
        labels: ['Reservations'],
        datasets: [{
            label: 'Count',
            data: [reservationsCountData],
            backgroundColor: 'rgba(255, 159, 64, 0.2)',
            borderColor: 'rgba(255, 159, 64, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

let reservationsSumChart = new Chart(reservationsSumChartCtx, {
    type: 'bar',
    data: {
        labels: ['Reservations'],
        datasets: [{
            label: 'Sum',
            data: [reservationsSumData],
            backgroundColor: 'rgba(153, 102, 255, 0.2)',
            borderColor: 'rgba(153, 102, 255, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        },
        plugins: {
            tooltip: {
                callbacks: {
                    label: function(context) {
                        return 'Sum: ' + context.parsed.y.toFixed(2);
                    }
                }
            }
        }
    }
});