using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models
{
  public  interface ICourseRepository
    {
        List<Course> GetAllCourses();

        List<CourseOffering> CourseOfferingsForCourse(int? id);

        List<Department> ListAllDepartments();

        List<Course> FindCoursesByDepartment(int? departmentID);

        List<Course> AllOfferingsForAllCourses();


    }
}
