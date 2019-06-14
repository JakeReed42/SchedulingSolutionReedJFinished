using Microsoft.AspNetCore.Mvc;
using SchedulingMVCAppReedJ.Data;
using SchedulingMVCAppReedJ.Models.ViewModels;
using SchedulingMVCAppReedJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchedulingMVCAppReedJ.ViewComponents
{
    public class ChangesSummary : ViewComponent
    {
        private ApplicationDbContext database;

        public ChangesSummary(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            ChangesSummaryViewModel viewModel =
                new ChangesSummaryViewModel();

            List<CourseOfferingChanges> changesList =
                database.CoursesOfferingsChanges.ToList<CourseOfferingChanges>();

            viewModel.NumberOfAddedCourses = changesList.Where(cl => cl.ChangeType == "Add").Count();
            viewModel.NumberOfEditedCourses = changesList.Where(cl => cl.ChangeType == "Edit").Count();
            viewModel.NumberOfRemovedCourses = changesList.Where(cl => cl.ChangeType == "Delete").Count();

            return View(viewModel);
        }


    }
}
