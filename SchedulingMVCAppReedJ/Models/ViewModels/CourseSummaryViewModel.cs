using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models.ViewModels
{
    public class CourseSummaryViewModel
    {
        public int NumberOfCoursesOffered { get; set; }
        public int NumberOfCoursesOfferedInTheMorning { get; set; }

        public int NumberOfCoursesAtCapacity { get; set; }
        public int NumberOfCoursesWithSpaceLeft { get; set; }
    }
}
