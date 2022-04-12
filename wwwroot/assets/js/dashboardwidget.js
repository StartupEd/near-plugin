
export function profileViewsChart(data) {
    if (!data) {
        return;
    }
    //console.log(data);

    var Dates = [];
    var Counts = [];
    //data.forEach(function (arrayItem) {
    //    console.log(arrayItem)
    //})
    for (var d in data) {
        Dates.push(d);
        //console.log(data[d].length)
        Counts.push(data[d].length)
    }



    var options = {
        series: [{
            name: "Profile Visited",
            data: Counts
        }],
        chart: {
            type: 'area',
            toolbar: {
                show: false
            },
            zoom: {
                enabled: false
            },
            sparkline: {
                enabled: true
            }
        },
        plotOptions: {},
        fill: {
            type: 'solid',
            opacity: 1
        },
        legend: {
            show: false
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            curve: 'smooth',
            show: true,
            width: 3,
            colors: [KTApp.getSettings()['colors']['theme']['base']['danger']]
        },
        xaxis: {
            categories: Dates,
            axisBorder: {
                show: false,
            },
            axisTicks: {
                show: false
            },
            labels: {
                show: false,
                style: {
                    colors: KTApp.getSettings()['colors']['gray']['gray-500'],
                    fontSize: '12px',
                    fontFamily: KTApp.getSettings()['font-family']
                }
            },
            crosshairs: {
                show: false,
                position: 'front',
                stroke: {
                    color: KTApp.getSettings()['colors']['gray']['gray-300'],
                    width: 1,
                    dashArray: 3
                }
            },
            tooltip: {
                enabled: true,
                formatter: undefined,
                offsetY: 0,
                style: {
                    fontSize: '12px',
                    fontFamily: KTApp.getSettings()['font-family']
                }
            }
        },
        yaxis: {
            labels: {
                show: false,
                style: {
                    colors: KTApp.getSettings()['colors']['gray']['gray-500'],
                    fontSize: '12px',
                    fontFamily: KTApp.getSettings()['font-family']
                }
            }
        },
        states: {
            normal: {
                filter: {
                    type: 'none',
                    value: 0
                }
            },
            hover: {
                filter: {
                    type: 'none',
                    value: 0
                }
            },
            active: {
                allowMultipleDataPointsSelection: false,
                filter: {
                    type: 'none',
                    value: 0
                }
            }
        },

        tooltip: {
            style: {
                fontSize: '12px',
                fontFamily: KTApp.getSettings()['font-family']
            },
            y: {
                formatter: function (val) {
                    return  val + " times"
                }
            }
        },
        colors: [KTApp.getSettings()['colors']['theme']['light']['danger']],
        markers: {
            colors: [KTApp.getSettings()['colors']['theme']['light']['danger']],
            strokeColor: [KTApp.getSettings()['colors']['theme']['base']['danger']],
            strokeWidth: 3
        }
    };

    var chart = new ApexCharts(document.querySelector("#profileViewChart"), options);
    chart.render();
}

