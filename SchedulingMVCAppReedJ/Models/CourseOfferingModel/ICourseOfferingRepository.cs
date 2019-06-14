using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models.CourseOfferingModel
{
    interface ICourseOfferingRepository
    {
        //List<CourseOffering> SearchCourseOfferings();

        List<CourseOffering> SearchResult(int? DepartmentID, DateTime? StartTime, DateTime? EndTime, int? InstructorID,
            DateTime? StartDate, DateTime? EndDate);

        List<CourseOffering> FindCourseOfferingsWithoutAssignedInstructors(int? instructorID);

        List<Instructor> AddInstructorToOffering(int? id);

        Task ConfirmAddition(int? id);

        Task AddCourseOffering(CourseOffering courseOffering);

        Task DeleteCourseOffering(int? id);

        Task EditCourseOffering(CourseOffering courseOffering);


        List<Department> ListAllDepartments();
        List<Instructor> ListAllInstructor();


    }
}
