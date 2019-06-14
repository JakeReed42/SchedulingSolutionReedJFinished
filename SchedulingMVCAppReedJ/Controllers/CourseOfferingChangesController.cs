using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingMVCAppReedJ.Data;
using SchedulingMVCAppReedJ.Models;
using SchedulingMVCAppReedJ.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SchedulingMVCAppReedJ.Controllers
{
    public class CourseOfferingChangesController : Controller
    {

        private ApplicationDbContext database;

        public CourseOfferingChangesController(ApplicationDbContext dBContext)
        {
            this.database = dBContext;
        }

        public IActionResult ViewAllChanges()
        {
            List<CourseOfferingChanges> changesList = database.CoursesOfferingsChanges.Include(coc => coc.Instructor)
                .Include(coc => coc.Course).Include(coc => coc.Course.Department).ToList<CourseOfferingChanges>();



            return View(changesList);
        }

        public string GetChangesDataForChart()
        {

            

            var changeList = from CC in database.CoursesOfferingsChanges.Include(CC => CC.Course).Include(CC => CC.Course.Department)
                             .OrderBy(CC => CC.ChangeType)
                             select new { CC.ChangeType,  };

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(changeList);
            return jsonData;
        }

    }
}