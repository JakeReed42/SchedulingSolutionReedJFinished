using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchedulingMVCAppReedJ.Data;
using SchedulingMVCAppReedJ.Models;


namespace SchedulingMVCAppReedJ.Controllers
{
    public class ManualCourseController : Controller
    {

        private ApplicationDbContext database;

        public ManualCourseController(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        //private ICourseRepository courseRepositoryInterface;

        //public ManualCourseController(ICourseRepository repository)
        //{
        //    this.courseRepositoryInterface = repository;
        //}

        public string GetCoursesDataForChart()
        {
            var courseList = from CO in database.CourseOfferings.Include(co => co.Course)
                             .OrderBy(co => co.Course.CourseName)
                             select new { CO.Course.CourseName, CO.Capacity, CO.Enrollment };

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(courseList);
            return jsonData;
        }

        public string GetCoursesForAutoComplete(string term)
        {
            var courseList = from C in database.Courses
                             .Where (C => C.CourseName.StartsWith(term))
                             .OrderBy(c => c.CourseName)
                             select new {id = C.CourseID, label = C.CourseName};

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(courseList);
            return jsonData;
        }

        
        public IActionResult GetAllCourses()
        {

            //the "c => c.Department" is called a lambda expression. 
            List<Course> courseList = database.Courses.Include(c => c.Department).ToList<Course>();
            //^ the include statement tells the program to also allow the Department info.

            //List<Course> courseList =
            //    courseRepositoryInterface.GetAllCourses();

            return View(courseList);
        }// end of GetAllCourses Method

        [HttpGet]
        public IActionResult AutoComplete()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AutoComplete(string CourseName, int CourseID)
        {
            int courseID = CourseID;
            string courseName = CourseName;

            return RedirectToAction("AutoComplete");
        }
        public IActionResult CourseOfferingsForCourse(int? id)
        {

            int courseID = Convert.ToInt32(id);
            HttpContext.Session.SetInt32("CourseID", courseID);

            Course course = database.Courses.Find(id);

            int addedCourseID = course.CourseID;

            List<CourseOffering> OfferingList =
                database.CourseOfferings.Include(co => co.Course).Include(co => co.Instructor).Include(co => co.Course.Department).
                Where(co => co.CourseID == addedCourseID).
                ToList<CourseOffering>();

            // List<CourseOffering> OfferingList = courseRepositoryInterface.CourseOfferingsForCourse(id);

            List<Course> courseOfferingList = database.Courses.Include(c => c.Department).Include(c => c.QualifiedInstructors).
                  Where(
                 C => C.CoursesOfferings.Any(CO => CO.CourseID == addedCourseID)
                  ).ToList<Course>();

            return View(OfferingList);
    }

    // this makes it so only departmentChairs can access this page
    [Authorize(Roles = "Chair")]
        public IActionResult SearchForCoursesByDepartment()
        {
            ////Design choice: For Project, when to use hard coded list values instead of database
            ////When values are non-Changing/ smaller number of values.
            List<String> semesterList = new List<string>();
            semesterList.Add("Fall");
            semesterList.Add("Spring");
            semesterList.Add("Summer");

            ViewData["Departments"] = new SelectList(database.Courses, "DepartmentID", "DepartmentName");
            //ViewData["Departments"] = new SelectList(courseRepositoryInterface.ListAllDepartments(), "DepartmentID", "DepartmentName");

            return View("SearchForCourses");


        }

        public IActionResult FindCoursesByDepartment(int? departmentID)
        {
            List<Course> courseList
                = database.Courses.Include(c => c.Department).ToList<Course>();

            List<Course> queriedList = new List<Course>();



            ////sql in c#
            //// LINQ: Language INtergrated Query
            //// is the same as IsNull in sql
            if (departmentID != null)
            {

                courseList =
                courseList.Where(c => c.DepartmentID == departmentID).ToList<Course>();
                //^ this is saying to set courseList set to a list of courses from the database 
                // where the DepartmentIDs match


                foreach (Course course in courseList)
                {
                    if (course.DepartmentID == departmentID)
                        queriedList.Add(course);
                    // this is saying that if the IDs match to add the course to the new list

                }// end of a foreach

                return View("SearchResult", queriedList);
            }// end of the if statement that selects a certain department

            // List<Course> courseList = courseRepositoryInterface.FindCoursesByDepartment(departmentID);



            return View("SearchResult", courseList);
            // this return the name of the view you are going to need.

        }

        public IActionResult AllOfferingsForAllCourses()
        {
            List<Course> courseList = database.Courses.Include(c => c.Department).Include(c => c.CoursesOfferings).ToList<Course>();

           // List<Course> courseList = courseRepositoryInterface.AllOfferingsForAllCourses();

            return View(courseList);
        }



    }// end of class
}// end of Namespace