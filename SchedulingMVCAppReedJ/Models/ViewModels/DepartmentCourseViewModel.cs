using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models.ViewModels
{
    public class DepartmentCourseViewModel
    {

        [Required(ErrorMessage = "Need To Select A Department")]
        [Display(Name = "Departments")]
        public int DepartmentID { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }

        public int CourseID { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

    }
}
