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
                        $list.append('<input type="checkbox" name="courses" value=" ' + this.CourseID + ' "> ' + this.CourseName + ' <br>');

                    });
                },
                error: function () {
                    alert("Data Not Recieved");
                }


            });
    });
});