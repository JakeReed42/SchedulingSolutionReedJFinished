using Microsoft.EntityFrameworkCore;
using SchedulingMVCAppReedJ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models
{
    public class CourseRepository : ICourseRepository
    {

        private ApplicationDbContext database;

        public CourseRepository(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public List<CourseOffering> CourseOfferingsForCourse(int? id)
        {
            int courseID = Convert.ToInt32(id);
            //HttpContext.Session.SetInt32("CourseID", courseID);

            Course course = database.Courses.Find(id);

            int addedCourseID = course.CourseID;

            List<CourseOffering> OfferingList =
                database.CourseOfferings.Include(co => co.Course).Include(co => co.Instructor).Include(co => co.Course.Department).
                Where(co => co.CourseID == addedCourseID).
                ToList<CourseOffering>();

            return OfferingList;
        }

        public List<Course> FindCoursesByDepartment(int? departmentID)
        {
            List<Course> courseList
                = database.Courses.Include(c => c.Department).ToList<Course>();

            //List<Course> queriedList = new List<Course>();



            //sql in c#
            // LINQ: Language INtergrated Query
            // is the same as IsNull in sql
            if (departmentID != null)
            {

                courseList =
                courseList.Where(c => c.DepartmentID == departmentID).ToList<Course>();
                //^ this is saying to set courseList set to a list of courses from the database 
                // where the DepartmentIDs match


                //    foreach (Course course in courseList)
                //{
                //    if (course.DepartmentID == departmentID)
                //    queriedList.Add(course);
                //    // this is saying that if the IDs match to add the course to the new list

                //}// end of a foreach

                //return View("SearchResult", queriedList);
            }// end of the if statement that selects a certain department





            return courseList;
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courseList = database.Courses.Include(c => c.Department).ToList<Course>();
            //^ the include statement tells the program to also allow the Department info. 


            return courseList;
        }

        public List<Department> ListAllDepartments()
        {
            List<Department> DepartmentList =
               database.Departments.ToList<Department>();

            return DepartmentList;
        }// end of list All Dept

        public List<Course> AllOfferingsForAllCourses()
        {
            List<Course> courseList =
                database.Courses.Include(c => c.Department).Include(c => c.CoursesOfferings).ToList();

            return courseList;
        }

    }// end of class    
}// end of namespace
