using SchedulingMVCAppReedJ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models.ViewModels
{
    public class ChangesSummaryViewModel
    {
        public int NumberOfAddedCourses { get; set; }
        public int NumberOfRemovedCourses { get; set; }
        public int NumberOfEditedCourses { get; set; }

        //private ApplicationDbContext database;

        //public ChangesSummaryViewModel(ApplicationDbContext dbContext)
        //{
        //    this.database = dbContext;
        //}

        //public ChangesSummaryViewModel()
        //{

        //}

        //public ChangesSummaryViewModel GetChanges()
        //{
        //    ChangesSummaryViewModel viewModel = new ChangesSummaryViewModel();

        //    List<CourseOfferingChanges> changeList = database.CoursesOfferingsChanges.ToList<> 

        //    return viewModel;
        //}
        
    }
}
