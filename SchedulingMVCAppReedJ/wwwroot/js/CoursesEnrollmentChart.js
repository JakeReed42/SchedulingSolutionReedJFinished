$(document).ready(function () {
    $.ajax(
        {
            url: "ManualCourse/GetCoursesDataForChart",
            type: "GET",
            dataType: "json",
            data: {},
            success: function (data) {
                CreateLineChart(data);
            },
            error: function () {
                alert("Error reading data")
            }
        });
});

function CreateLineChart(inputData) {
    var procChart = new Morris.Line({
        element: 'chart',
        data: inputData,
        xkey: ['CourseName'],
        ykeys: ['Capacity', 'Enrollment'],
        labels: ['Capacity', 'Enrollment'],
        hideHover: 'auto',
        resize: true,
        parseTime: false
    })
}
