// Initialize charts
function initializeChart(chartId, data) {
    let chartCtx = document.getElementById(chartId).getContext('2d');
    new Chart(chartCtx, {
        type: 'bar',
        data: {
            labels: [chartId.replace('Chart', '')],
            datasets: [{
                label: 'Count',
                data: [data],
                backgroundColor: getBackgroundColor(chartId),
                borderColor: getBorderColor(chartId),
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
}

function getBackgroundColor(chartId) {
    switch (chartId) {
        case 'clientsChart':
            return 'rgba(54, 162, 235, 0.2)';
        case 'driversChart':
            return 'rgba(75, 192, 192, 0.2)';
        case 'taxisChart':
            return 'rgba(255, 206, 86, 0.2)';
        case 'reservationsCountChart':
            return 'rgba(255, 159, 64, 0.2)';
        case 'reservationsSumChart':
            return 'rgba(153, 102, 255, 0.2)';
        default:
            return '';
    }
}

function getBorderColor(chartId) {
    switch (chartId) {
        case 'clientsChart':
            return 'rgba(54, 162, 235, 1)';
        case 'driversChart':
            return 'rgba(75, 192, 192, 1)';
        case 'taxisChart':
            return 'rgba(255, 206, 86, 1)';
        case 'reservationsCountChart':
            return 'rgba(255, 159, 64, 1)';
        case 'reservationsSumChart':
            return 'rgba(153, 102, 255, 1)';
        default:
            return '';
    }
}

function toggleChart(chartId) {
    let chart = document.getElementById(chartId);
    if (chart.style.display === 'none') {
        chart.style.display = 'block';
        initializeChart(chartId, getData(chartId));
    } else {
        chart.style.display = 'none';
    }
}

function getData(chartId) {
    switch (chartId) {
        case 'clientsChart':
            return clientsData;
        case 'driversChart':
            return driversData;
        case 'taxisChart':
            return taxisData;
        case 'reservationsCountChart':
            return reservationsCountData;
        case 'reservationsSumChart':
            return reservationsSumData;
        default:
            return [];
    }
}