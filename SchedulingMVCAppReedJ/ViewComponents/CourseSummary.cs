using Microsoft.AspNetCore.Mvc;
using SchedulingMVCAppReedJ.Data;
using SchedulingMVCAppReedJ.Models;
using SchedulingMVCAppReedJ.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.ViewComponents
{
    public class CourseSummary: ViewComponent
    {
        private ApplicationDbContext database;

        public CourseSummary(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        public IViewComponentResult Invoke()
        {
            CourseSummaryViewModel vm =
                new CourseSummaryViewModel();

            List<CourseOffering> offeringList = database.CourseOfferings.ToList<CourseOffering>();

            vm.NumberOfCoursesOffered = offeringList.Count;

            vm.NumberOfCoursesOfferedInTheMorning = offeringList.Where(co => co.StartTime.Hour <= 12).Count();

            vm.NumberOfCoursesAtCapacity =
                offeringList.Where(co => co.Enrollment >= (0.9 * co.Capacity)).Count();

            vm.NumberOfCoursesWithSpaceLeft =
                offeringList.Where(co => co.Enrollment <= (0.5 * co.Capacity)).Count(); 

            return View(vm);
        }



    }
}
