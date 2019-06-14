using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingMVCAppReedJ.Data;
using SchedulingMVCAppReedJ.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulingMVCAppReedJ.Models.ViewModels;
using SchedulingMVCAppReedJ.Services;

namespace SchedulingMVCAppReedJ.Controllers
{
    // Below shows inhertance (Child : Parent)
    public class ManualDepartmentController : Controller
    {
        //attribute of this class
        private ApplicationDbContext database;

        //Constructor Biggest hint is that it has the same name as the class
        //
        public ManualDepartmentController(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        [Authorize]
        public IActionResult GetAllDepartments()
        {

           List<Department> departmentList = 
                database.Departments.Include(d => d.DepartmentChair).ToList<Department>();

            return View(departmentList);
        }

        //returns JSON 
        public string GetCourses(int id)
        {
            string jsonData = null;
            var courseList = from C in database.Courses
                             .Where(C => C.DepartmentID == id)
                             .OrderBy(C => C.CourseName)
                             select new {C.CourseID, C.CourseName};

            jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(courseList);

            return jsonData;
        }

        public IActionResult SaveSelections()

        {

            List<int> allSelectedCourses = HttpContext.Session.GetSelectedCourses();



            List<Course> selectedCourseList = new List<Course>();



            foreach (int eachCourseID in allSelectedCourses)

            {

                Course course = database.Courses.Find(eachCourseID);

                selectedCourseList.Add(course);



                //Do something with each slected course object

            }

            // Do something with the selected course list

            HttpContext.Session.Clear();



            ViewData["Departments"] = new SelectList(database.Departments.OrderBy(d => d.DepartmentName).ToList(), "DepartmentID", "DepartmentName");



            return View("SelectedList", selectedCourseList);



        }

        [HttpGet]
        public IActionResult NestedDDL()
        {

            var departments =
                new SelectList(database.Departments.ToList(), "DepartmentID", "DepartmentName");


            var courses =
                new SelectList(database.Courses.ToList(), "CourseID", "CourseName");

            var viewModel = new DepartmentCourseViewModel();
            viewModel.Departments = departments;
            viewModel.Courses = courses;

            return View(viewModel);
        }

        [HttpPost]
        public void NestedDDL(DepartmentCourseViewModel model)
        {
            int departmentID = model.DepartmentID;
            int courseID = model.CourseID;
        }

        [HttpGet]
        public IActionResult SelectedList()
        {
            ViewData["Departments"] =
                new SelectList(database.Departments.OrderBy(d => d.DepartmentName).ToList(), "DepartmentID", "DepartmentName");
          

            //List<Course> courseList = database.Courses.ToList<Course>();

            return View();
        }

        [HttpPost]
        public IActionResult SelectedListResult()
        {
            ViewData["Departments"] =
              new SelectList(database.Departments.ToList(), "DepartmentID", "DepartmentName");

                        List<Course> courseList = database.Courses.ToList<Course>();

            List<Course> selectedCourseList = new List<Course>();          

            List<string> selectedCourses_Strings =
                Request.Form["courses"].ToList();


            List<int> currentSelectedCourses_Ints =
                new List<int>();

            foreach (string eachString in selectedCourses_Strings)
            {
                currentSelectedCourses_Ints.Add(Convert.ToInt32(eachString));
            }

            List<int> allSelectedCourses_Ints =
                HttpContext.Session.GetSelectedCourses();

            foreach(int eachCourseID in allSelectedCourses_Ints)
            {
                if(currentSelectedCourses_Ints.Contains(eachCourseID))
                {
                    currentSelectedCourses_Ints.Remove(eachCourseID);
                }
            }

            allSelectedCourses_Ints.AddRange(currentSelectedCourses_Ints);

            HttpContext.Session.SetSelectedCourses(allSelectedCourses_Ints);

            foreach (int eachCourseID in allSelectedCourses_Ints)
            {
                Course course = database.Courses.Find(eachCourseID);
                selectedCourseList.Add(course);
            }

            
            

            return View("SelectedList", selectedCourseList);
        }


    }// end of class
}// end of method