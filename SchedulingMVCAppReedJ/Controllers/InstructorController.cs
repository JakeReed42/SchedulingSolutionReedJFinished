using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchedulingMVCAppReedJ.Data;
using SchedulingMVCAppReedJ.Models;

namespace SchedulingMVCAppReedJ.Controllers
{
    public class InstructorController : Controller
    {
        //Attribute. different from a variable. it can be used across methods
        private IInstructorRepository instructorRepositoryInterface;

        public InstructorController(IInstructorRepository repository)
        {
            this.instructorRepositoryInterface = repository;
        }

        public IActionResult ListAllInstructors()
        {
            //List<Instructor> InstructorList =
            //instructorRepository.Instructors.Include(i => i.Department).ToList<Instructor>();

            List<Instructor> InstructorList =
                instructorRepositoryInterface.ListAllInstructors();

            return View(InstructorList);
        }// end of ListAllInstructors


        //Add and Edit: Two Methods for each (Get and Post)
        [HttpGet]
        public IActionResult AddInstructor()
        {
            ViewData["Departments"] =
                new SelectList(instructorRepositoryInterface.ListAllDepartments(), "DepartmentID", "DepartmentName");

            return View();
        }// end of AddInstructor HttpGet

        [HttpPost]
        public async Task<IActionResult> AddInstructor([Bind("InstructorID, InstructorFirstName, InstructorLastName, InstructorEmail, DepartmentID")]Instructor instructor)
        {
            //AddAsync adds the info to the database without freezing up the screen
            //database.Instructors.AddAsync(instructor);
            //database.SaveChangesAsync();

           await instructorRepositoryInterface.AddInstructor(instructor);

            return RedirectToAction("ListAllInstructors");
        }

        [HttpGet]
        public IActionResult EditInstructor(int? id)
        {
            //List<Instructor> instructorList = instructorRepositoryInterface.ListAllInstructors();

            Instructor instructor = instructorRepositoryInterface.FindInstructor(id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditInstructor([Bind("InstructorID, InstructorFirstName, InstructorLastName, InstructorEmail, DepartmentID")]Instructor instructor)
        {

            await instructorRepositoryInterface.EditInstructor(instructor);

            return RedirectToAction("ListAllInstructors");
        }

        public async Task<IActionResult> DeleteInstructor(int id)
        {
            //Instructor instructor = database.Instructors.Find(id);
            //try
            //{
            //    database.Instructors.Remove(instructor);
            //    database.SaveChangesAsync();
            //}
            //catch (Exception e)
            //{

            //}

            await instructorRepositoryInterface.DeleteInstructor(id);

            return RedirectToAction("ListAllInstructors");
        }

    }// end of class
}//end of namespace