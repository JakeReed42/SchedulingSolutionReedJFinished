using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchedulingMVCAppReedJ.Data;
using SchedulingMVCAppReedJ.Models;
using SchedulingMVCAppReedJ.Models.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace SchedulingMVCAppReedJ.Controllers
{
    public class CourseOfferingsController : Controller
    {
        private ApplicationDbContext database;

        //dependency Inversion (DI)
        // Add, Delete, Edit, CourseOffering

        public CourseOfferingsController(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        [HttpGet]
        public IActionResult SearchCourseOfferingsCombined()
        {
            // 1. By Department (Name, ID)
            ViewData["Departments"] = new SelectList(database.Departments, "DepartmentID", "DepartmentName");


            // 2. By Instructor (Name, ID)
            ViewData["Instructors"] = new SelectList
                (database.Instructors, "InstructorID", "InstructorLastName");

            SearchCourseOfferingsViewModel viewModel =
               new SearchCourseOfferingsViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchCourseOfferingsCombined(SearchCourseOfferingsViewModel model)
        {
            // 1. By Department (Name, ID)
            ViewData["Departments"] = new SelectList(database.Departments, "DepartmentID", "DepartmentName");


            // 2. By Instructor (Name, ID)
            ViewData["Instructors"] = new SelectList
                (database.Instructors, "InstructorID", "InstructorLastName");

            List<CourseOffering> OfferingList =
                database.CourseOfferings.Include(co => co.Course).Include(co => co.Instructor).Include(co => co.Course.Department).ToList<CourseOffering>();

            if (model.DepartmentID != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.Course.DepartmentID == model.DepartmentID).ToList<CourseOffering>();

            }

            if (model.SearchStartTime != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.StartTime.TimeOfDay >= model.SearchStartTime.Value.TimeOfDay).ToList<CourseOffering>();
            }

            if(model.SortOrder != null)
            {
                if(model.SortOrder == "CourseName")
                {
                    OfferingList = 
                        OfferingList.OrderBy(co => co.Course.CourseName).ToList();
                }

                if (model.SortOrder == "StartTime")
                {
                    OfferingList =
                        OfferingList.OrderBy(co => co.StartTime).ToList();
                }


            }

            SearchCourseOfferingsViewModel viewModel =
               new SearchCourseOfferingsViewModel();

            viewModel.CourseOfferingList = OfferingList;

            return View(viewModel);
        }

        public IActionResult SearchCourseOfferings()
        {

            // 1. By Department (Name, ID)
         ViewData["Departments"]  = new SelectList(database.Departments, "DepartmentID", "DepartmentName");

            var InstructorWithDepartment
                = from I in database.Instructors.Include(i => i.Department)
                  select new { I.InstructorID, InstructorWithDepartment = I.InstructorFullName + " in " + I.Department.DepartmentName };


            // 2. By Instructor (Name, ID)
            ViewData["Instructors"] = new SelectList
                (InstructorWithDepartment, "InstructorID", "InstructorWithDepartment");


            return View();
        }

        [HttpPost]
        public IActionResult SearchResult(int? DepartmentID, DateTime? StartTime, DateTime? EndTime, int? InstructorID,
            DateTime? StartDate, DateTime? EndDate)
        {
            List<CourseOffering> OfferingList =
                database.CourseOfferings.Include(co => co.Course).Include(co => co.Instructor).Include(co => co.Course.Department).ToList<CourseOffering>();

           if(DepartmentID != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.Course.DepartmentID == DepartmentID).ToList<CourseOffering>();

            }

           if(StartTime != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.StartTime.TimeOfDay >= StartTime.Value.TimeOfDay).ToList<CourseOffering>();
            }

           if(EndTime != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.EndTime.TimeOfDay <= EndTime.Value.TimeOfDay).ToList<CourseOffering>();

            }

           if(InstructorID != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.InstructorID == InstructorID).ToList<CourseOffering>();
            }

           if(StartDate != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.StartDate >= StartDate.Value.Date).ToList<CourseOffering>();
            }

            if (EndDate != null)
            {
                OfferingList =
                    OfferingList.Where(co => co.EndDate <= EndDate.Value.Date).ToList<CourseOffering>();
            }


            return View(OfferingList);
        }

        public IActionResult NavigationTest()
        {
           List<CourseOffering> navList = database.CourseOfferings.Include(co => co.Course)
                //ThenInclude moves the table up so it now references course instead of offering 
                .ThenInclude(c => c.Department)
                .ThenInclude(d => d.DepartmentID)
                //resets back to courseOffering when you use include again
                .Include(co => co.CourseOfferingID).ToList<CourseOffering>();

           Course course = database.Courses.Find(1);
           List<CourseOffering> offeringList = course.CoursesOfferings;
            return View(navList);
        }

        public IActionResult FindCourseOfferingsWithoutAssignedInstructors(int? instructorID)
        {

            string departmentChairID = null;
             
            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole("Chair"))
                {
                    // this line allows you to find the who is logged in and store it
                     departmentChairID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                }
            }
            List<CourseOffering> courseOfferingsList =
                database.CourseOfferings.Include(co => co.Course)
                .Include(co => co.Course.Department).Where(co => co.InstructorID == null) 
               .ToList<CourseOffering>();
             
            if(departmentChairID != null)
            {


                courseOfferingsList = courseOfferingsList.Where(co => co.Course.Department.DepartmentChairID == departmentChairID).ToList<CourseOffering>();
            }
            
                return View(courseOfferingsList);
        }


        [Authorize(Roles = "Chair")]
        public IActionResult AddInstructorToOffering(int? id)
        {
            int courseOfferingID = Convert.ToInt32(id);

            HttpContext.Session.SetInt32("CourseOfferingID", courseOfferingID);
            //ISession session = ;
           
                CourseOffering courseOffering = database.CourseOfferings.Find(id);
                //  SessionExtensions.SetInt32(session ,"courseOffering", courseOffering.CourseOfferingID);
                

           int AddingCourseID = courseOffering.CourseID;

                //Session["CourseID"] = AddingCourseID;

                List<Instructor> instructorList =
                database.Instructors.Include(i => i.Department).
                    Where
                    (
                    i => i.QualifiedCourses.Any(QC => QC.CourseID == AddingCourseID)
                    ).ToList<Instructor>();

            

            // nandas approach is a bit cleaner and simpler kept my first two lines tho so whos the real winner
            //       CourseOffering courseOffering = database.CourseOfferings.Find(id);

            //      int AddingCourseID = database.CourseOfferings.Find(id).CourseID;

            //       List<TeachingQualification> viableInstructors =
            //database.TeachingQualifications.Include(ico => ico.Instructor).Include(ico => ico.Course)
            //.Where(ico => ico.CourseID == AddingCourseID).ToList<TeachingQualification>();

            //try
            //{


            //    database.CourseOfferings.Update(instructorList);
            //    database.SaveChanges();
            //}
            //catch (Exception e)
            //{

            //}


            return View(instructorList);
        }

       

        public IActionResult ConfirmAddition(int? id)
        {

            
                

                int? addedInstructor = id;
                int courseOfferingID = HttpContext.Session.GetInt32("CourseOfferingID").Value;

            CourseOffering courseOffering = database.CourseOfferings.Find(courseOfferingID);

            courseOffering.InstructorID = addedInstructor;

                database.CourseOfferings.Update(courseOffering);
                database.SaveChanges();
            
             
            return RedirectToAction("FindCourseOfferingsWithoutAssignedInstructors");
        }

        [HttpGet]
        [Authorize(Roles = "Chair, Coordinator")]
        public IActionResult AddCourseOffering()
        {

            PopulateDropDownList();

            return View();
        }

        private void PopulateDropDownList()
        {
            List<DaysOfWeekViewModel> daysOfWeekViewModelList =
                        new List<DaysOfWeekViewModel>();

            DaysOfWeekViewModel daysOfWeekViewModel =
                new DaysOfWeekViewModel
                {
                    DaysOfWeek = "M, W, F",


                };
            daysOfWeekViewModelList.Add(daysOfWeekViewModel);


            daysOfWeekViewModel =
            new DaysOfWeekViewModel
            {
                DaysOfWeek = "T, TR",


            };
            daysOfWeekViewModelList.Add(daysOfWeekViewModel);

            daysOfWeekViewModel =
            new DaysOfWeekViewModel
            {
                DaysOfWeek = "M, T, W, TR, F",


            };
            daysOfWeekViewModelList.Add(daysOfWeekViewModel);

            ViewData["DaysOfWeekValues"] =
              new SelectList(daysOfWeekViewModelList, "DaysOfWeek", "DaysOfWeek");

            ViewData["Instructors"] =
             new SelectList(database.Instructors, "InstructorID", "InstructorFullName");

            ViewData["Courses"] =
                new SelectList(database.Courses, "CourseID", "CourseName");
        }
        private DateTime PopulateGetDateViewModel()
        {
            List<GetDateViewModel> getDateViewModelsList = new List<GetDateViewModel>();

            DateTime currentDate = DateTime.Now;

            return currentDate;
                     

        }

       


        [HttpPost]
        public IActionResult AddCourseOffering(
            [Bind("CourseOfferingID, CRN, Days, StartTime, " +
            "EndTime, StartDate, EndDate, InstructorID, CourseID")] CourseOffering courseOffering
            )
        {

            database.CourseOfferings.Add(courseOffering);
            database.SaveChanges();
            
            
            AddCourseOfferingChanges( courseOffering, "Add");

            return RedirectToAction("SearchCourseOfferings");
        }

        [Authorize(Roles = "Chair, Coordinator")]
        public IActionResult DeleteCourseOffering(int? id)
        {
            CourseOffering courseOffering = database.CourseOfferings.Find(id);

            

            database.CourseOfferings.Remove(courseOffering);
            database.SaveChanges();

            
            AddCourseOfferingChanges(courseOffering, "Delete");

            return RedirectToAction("SearchCourseOfferings");
        }

        [HttpGet]
        [Authorize(Roles = "Chair, Coordinator")]
        public IActionResult EditCourseOffering(int? id)
        {
            CourseOffering courseOffering = database.CourseOfferings.Find(id);

            PopulateDropDownList();

            return View(courseOffering);
        }

        [HttpPost]
        public IActionResult EditCourseOffering([Bind("CourseOfferingID, CRN, Days, StartTIme, EndTime, StartDate, EndDate, InstructorID, CourseID")]CourseOffering courseOffering)
        {
            database.CourseOfferings.Update(courseOffering);
            database.SaveChanges();
            

            AddCourseOfferingChanges(courseOffering, "Edit");

            return RedirectToAction("SearchCourseOfferings");

        }

        private void AddCourseOfferingChanges(CourseOffering courseOffering, string changeType)
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser appUser = database.Users.Find(userID);


            CourseOfferingChanges offeringChanges = new CourseOfferingChanges();

            if (User.IsInRole("Chair"))
            { offeringChanges.ChangerRole = "Chair"; }

            else
            { offeringChanges.ChangerRole = "Coordinator"; }


            offeringChanges.ChangeType = changeType;
            offeringChanges.CourseID = courseOffering.CourseID;
            offeringChanges.CRN = courseOffering.CRN;
            offeringChanges.DateAndTimeOfChange = PopulateGetDateViewModel();
            offeringChanges.Days = courseOffering.Days;
            offeringChanges.EndDate = courseOffering.EndDate;
            offeringChanges.EndTime = courseOffering.EndTime;
            offeringChanges.InstructorID = courseOffering.InstructorID;
            offeringChanges.ChangerName = appUser.LastName;
            offeringChanges.StartDate = courseOffering.StartDate;
            offeringChanges.StartTime = courseOffering.StartTime;

            database.CoursesOfferingsChanges.Add(offeringChanges);
            database.SaveChanges();
        }

    }// end of class

}// end of namespace