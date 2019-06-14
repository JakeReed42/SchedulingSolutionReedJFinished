using Microsoft.EntityFrameworkCore;
using SchedulingMVCAppReedJ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models
{
    public class InstructorRepository : IInstructorRepository
    {
        private ApplicationDbContext database;

        public InstructorRepository(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public Task AddInstructor(Instructor instructor)
        {
            database.Instructors.AddAsync(instructor);
            return database.SaveChangesAsync();
        }

        public Instructor FindInstructor(int? id)
        {
            Instructor instructor = database.Instructors.Find(id);

            return instructor;
        }

        public Task EditInstructor(Instructor instructor)
        {
            database.Instructors.Update(instructor);
            return database.SaveChangesAsync();

        }

        public Task DeleteInstructor(int? id)
        {
            Instructor instructor = database.Instructors.Find(id);

            database.Instructors.Remove(instructor);
            return database.SaveChangesAsync();
            
            

        }

        public List<Department> ListAllDepartments()
        {
            List<Department> DepartmentList =
                database.Departments.ToList<Department>();

            return DepartmentList;
        }

        public List<Instructor> ListAllInstructors()
        {
            List<Instructor> InstructorList =
            database.Instructors.Include(i => i.Department).ToList<Instructor>();


       
            return InstructorList;
        }
    }
}
