using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models.ViewModels
{
    public class SearchCourseOfferingsViewModel
    {
        [Display(Name = "Department")]
        public int? DepartmentID { get; set; }

        public int? InstructorID { get; set; }

        [Display(Name = "Choose a Start Time")]
        [DataType(DataType.Time)]
        public DateTime? SearchStartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SearchStartDate { get; set; }


        public string SortOrder { get; set; }

        // Search

        public List<CourseOffering> CourseOfferingList { get; set; }

    }
}

