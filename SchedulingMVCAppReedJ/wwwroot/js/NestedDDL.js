$(document).ready(function () {
    $("#department-target").on("change", function () {

        $list = $("#course-target");
        $.ajax(
            {
                url: "GetCourses",
                type: "GET",
                dataType: "json",
                data: { id: $("#department-target").val() },
                success: function (data) {
                    $list.empty();
                    $.each(data, function () {
                        $list.append('<option value=" ' + this.CourseID + ' "> ' + this.CourseName + ' </option>');

                    });
                },
                error: function () {
                    alert("Data Not Recieved");
                }


            });
    });
});