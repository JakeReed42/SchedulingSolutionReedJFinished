$(document).ready(function () {
    $.ajax(
        {
            url: "CourseOfferingChanges/GetChangesDataForChart",
            type: "GET",
            dataType: "json",
            data: {},
            success: function (data) {
                CreateBarChart(data);
            },
            error: function () {
                alert("Error reading data")
            }
        });
});

function CreateBarChart(inputData) {
    var procChart = new Morris.Bar({
        element: 'barChart',
        data: inputData,
        xkey: ['ChangeType'],
        ykeys: ['ChangerRole'],
        labels: ['ChangerRole'],
        hideHover: 'auto',
        resize: true,
        parseTime: false
    })
}
