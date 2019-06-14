using Microsoft.EntityFrameworkCore;
using SchedulingMVCAppReedJ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models.CourseOfferingModel
{
    public class CourseOfferingRepository : ICourseOfferingRepository
    {
        private ApplicationDbContext database;

        public Task AddCourseOffering(CourseOffering courseOffering)
        {
            throw new NotImplementedException();
        }

        public List<Instructor> AddInstructorToOffering(int? id)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmAddition(int? id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCourseOffering(int? id)
        {
            throw new NotImplementedException();
        }

        public Task EditCourseOffering(CourseOffering courseOffering)
        {
            throw new NotImplementedException();
        }

        public List<CourseOffering> FindCourseOfferingsWithoutAssignedInstructors(int? instructorID)
        {
            //string departmentChairID = null;

            //if (User.Identity.IsAuthenticated)
            //{

            //    if (User.IsInRole("Chair"))
            //    {
            //        // this line allows you to find the who is logged in and store it
            //        departmentChairID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //    }
            //}
            //List<CourseOffering> courseOfferingsList =
            //    database.CourseOfferings.Include(co => co.Course)
            //    .Include(co => co.Course.Department).Where(co => co.InstructorID == null)
            //   .ToList<CourseOffering>();

            //if (departmentChairID != null)
            //{


            //    courseOfferingsList = courseOfferingsList.Where(co => co.Course.Department.DepartmentChairID == departmentChairID).ToList<CourseOffering>();
            //}

            //return courseOfferingsList;

            throw new NotImplementedException();
        }

        public List<Department> ListAllDepartments()
        {
            List<Department> DepartmentList =
                database.Departments.ToList<Department>();

            return DepartmentList;
        }// end of ListAllDepartments

        public List<Instructor> ListAllInstructor()
        {
            List<Instructor> InstructorList =
                database.Instructors.ToList<Instructor>();
            return InstructorList;
        }

        //public List<CourseOffering> SearchCourseOfferings()
        //{
        //    throw new NotImplementedException();
        //}

        public List<CourseOffering> SearchResult(int? DepartmentID, DateTime? StartTime, DateTime? EndTime, int? InstructorID, DateTime? StartDate, DateTime? EndDate)
        {
            List<CourseOffering> OfferingList =
               database.CourseOfferings.Include(co => co.Course).Include(co => co.Instructor).Include(co => co.Course.Department).ToList<CourseOffering>();

            if (DepartmentID != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.Course.DepartmentID == DepartmentID).ToList<CourseOffering>();

            }

            if (StartTime != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.StartTime.TimeOfDay >= StartTime.Value.TimeOfDay).ToList<CourseOffering>();
            }

            if (EndTime != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.EndTime.TimeOfDay <= EndTime.Value.TimeOfDay).ToList<CourseOffering>();

            }

            if (InstructorID != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.InstructorID == InstructorID).ToList<CourseOffering>();
            }

            if (StartDate != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.StartDate >= StartDate.Value.Date).ToList<CourseOffering>();
            }

            if (EndDate != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.EndDate <= EndDate.Value.Date).ToList<CourseOffering>();
            }


            return OfferingList;
            
        }
    }
}
