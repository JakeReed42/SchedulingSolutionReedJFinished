using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models
{
   public interface IInstructorRepository
    {
        // List of Actions (methods)
      List<Instructor>  ListAllInstructors();

      Task AddInstructor(Instructor instructor);

      Task EditInstructor(Instructor instructor);

        Instructor FindInstructor(int? id);  

      Task DeleteInstructor(int? id);

        List<Department> ListAllDepartments();
    }
}
